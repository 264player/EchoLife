using System.Security.Claims;
using EchoLife.Account.Services;
using EchoLife.Common;
using EchoLife.Common.Exceptions;
using EchoLife.Family.Data;
using EchoLife.Family.Dtos;
using EchoLife.Family.Models;

namespace EchoLife.Family.Services;

public class FamilyMemberService(IFamilyMemberRepository _familyMemberRepository)
    : IFamilyMemberService
{
    public async Task CreateFamilyMemberAsync(
        ClaimsPrincipal user,
        FamilyMemberRequest familyMemberRequest
    )
    {
        var userId = ClaimsManager.EnsureGetUserId(user);

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

    public Task<FamilyMemberResponse> GetFamilyMemberAsync(
        ClaimsPrincipal user,
        string familyTreeId
    )
    {
        throw new NotImplementedException();
    }

    public Task<List<FamilyMemberResponse>> GetFamilyMemberAsync(string familyTreeId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateFamilyMemberAsync(FamilyMemberRequest familyMemberRequest)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteFamilyMemberAsync(string familyMemberId)
    {
        await _familyMemberRepository.DeleteAsync(familyMemberId);
    }

    public async Task<FamilyMember> EnsureAndGetFamilyMemberAsync(string familyId)
    {
        return await _familyMemberRepository.ReadAsync(familyId)
            ?? throw new ResourceNotFoundException();
    }
}
