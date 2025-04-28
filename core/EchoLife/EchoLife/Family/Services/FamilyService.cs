using System.Security.Claims;
using EchoLife.Account.Services;
using EchoLife.Common;
using EchoLife.Common.Exceptions;
using EchoLife.Family.Data;
using EchoLife.Family.Dtos;
using EchoLife.Family.Models;

namespace EchoLife.Family.Services;

public class FamilyService(
    IFamilyTreeRepository _familyTreeRepository,
    IFamilyMemberRepository _familyMemberRepository
) : IFamilyService
{
    public async Task<FamilyTreeResponse> CreateFamilyTreeAsync(
        ClaimsPrincipal user,
        FamilyTreeRequest familyTreeRequest
    )
    {
        var userId = ClaimsManager.GetAuthorizedUserId(user);

        var result =
            await _familyTreeRepository.CreateAsync(
                new FamilyTree
                {
                    Id = IdGenerator.GenerateUlid(),
                    Name = familyTreeRequest.Name,
                    CreatedUserId = userId,
                }
            ) ?? throw new UnknowException();

        return FamilyTreeResponse.From(result);
    }

    public async Task<FamilyTreeResponse> GetFamilyTreeAsync(
        ClaimsPrincipal me,
        string familyTreeId
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);
        await EnsureUserInFamilyAsync(myId, familyTreeId);

        var result = await EnsureAndGetFamilyTreeAsync(familyTreeId);

        return FamilyTreeResponse.From(result);
    }

    public async Task<List<FamilyTreeResponse>> GetFamilyTreeAsync(
        ClaimsPrincipal me,
        int count,
        string? cursorId
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var familyMembers = await _familyMemberRepository.ReadAsync(
            x => x.UserId == myId && (cursorId == null || x.FamilyId.CompareTo(cursorId) < 0),
            count
        );
        var families = await _familyTreeRepository.ReadAsync(familyMembers.Select(m => m.FamilyId));
        return [.. families.Select(FamilyTreeResponse.From)];
    }

    public async Task<FamilyTreeResponse> UpdateFamilyTreeAsync(
        ClaimsPrincipal user,
        string familyTreeId,
        FamilyTreeRequest familyTreeRequest
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(user);
        await EnsureUserInFamilyAsync(myId, familyTreeId);

        var result = await EnsureAndGetFamilyTreeAsync(familyTreeId);

        var updated =
            await _familyTreeRepository.UpdateAsync(Update(result, familyTreeRequest))
            ?? throw new UnknowException();
        return FamilyTreeResponse.From(updated);

        static FamilyTree Update(FamilyTree familyTree, FamilyTreeRequest familyTreeRequest)
        {
            familyTree.Name = familyTreeRequest.Name;
            return familyTree;
        }
    }

    public async Task DeleteFamilyTreeAsync(ClaimsPrincipal me, string familyTreeId)
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);
        await EnsureUserInFamilyAsync(myId, familyTreeId);

        await _familyTreeRepository.DeleteAsync(familyTreeId);
    }

    private async Task<FamilyTree> EnsureAndGetFamilyTreeAsync(string familyTreeId)
    {
        return await _familyTreeRepository.ReadAsync(familyTreeId)
            ?? throw new FamilyTreeNotFoundException(familyTreeId);
    }

    private async Task EnsureUserInFamilyAsync(string userId, string familyTreeId)
    {
        var familyMember = await _familyMemberRepository.ReadAsync(
            m => m.UserId == userId && m.FamilyId == familyTreeId,
            1
        );
        if (familyMember.Count == 0)
        {
            throw new ForbiddenException(userId);
        }
    }

    #region Family Member

    public async Task CreateFamilyMemberAsync(
        ClaimsPrincipal user,
        FamilyMemberRequest familyMemberRequest
    )
    {
        var userId = ClaimsManager.GetAuthorizedUserId(user);

        await _familyMemberRepository.CreateAsync(
            new FamilyMember
            {
                Id = IdGenerator.GenerateUlid(),
                UserId = userId,
                FamilyId = familyMemberRequest.FamilyId,
                DisplayName = familyMemberRequest.DisplayName,
                Gender = familyMemberRequest.Gender,
                FatherId = familyMemberRequest.FatherId,
                MotherId = familyMemberRequest.MotherId,
                SpouseId = familyMemberRequest.SpouseId,
                Generation = familyMemberRequest.Generation,
                BirthDate = familyMemberRequest.BirthDate,
                DeathDate = familyMemberRequest.DeathDate,
                PowerLevel = familyMemberRequest.PowerLevel,
            }
        );
    }

    public async Task<List<FamilyMemberResponse>> GetFamilyMemberAsync(string familyTreeId)
    {
        return
        [
            .. (await _familyMemberRepository.ReadByFamilyIdAsync(familyTreeId)).Select(
                FamilyMemberResponse.From
            ),
        ];
    }

    public async Task<FamilyMemberResponse> UpdateFamilyMemberAsync(
        ClaimsPrincipal me,
        FamilyMemberRequest familyMemberRequest
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);
        var member = await EnsureAndGetFamilyMemberAsync(myId);

        return FamilyMemberResponse.From(
            await _familyMemberRepository.UpdateAsync(Update(member, familyMemberRequest))
                ?? throw new UnknowException()
        );
        FamilyMember Update(FamilyMember familyMember, FamilyMemberRequest familyMemberRequest)
        {
            return member;
        }
    }

    public async Task DeleteFamilyMemberAsync(ClaimsPrincipal me, string familyMemberId)
    {
        await _familyMemberRepository.DeleteAsync(familyMemberId);
    }

    public async Task<FamilyMember> EnsureAndGetFamilyMemberAsync(string memberId)
    {
        return await _familyMemberRepository.ReadAsync(memberId)
            ?? throw new FamilyMemberNotFoundException(memberId);
    }

    #endregion
}
