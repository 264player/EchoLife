using System.Security.Claims;
using EchoLife.Account.Services;
using EchoLife.Common;
using EchoLife.Common.Exceptions;
using EchoLife.Family.Data;
using EchoLife.Family.Dtos;
using EchoLife.Family.Models;

namespace EchoLife.Family.Services;

public class FamilyTreeService(IFamilyTreeRepository _familyTreeRepository) : IFamilyTreeService
{
    public async Task CreateFamilyTreeAsync(
        ClaimsPrincipal user,
        FamilyTreeRequest familyTreeRequest
    )
    {
        var userId = ClaimsManager.GetAuthorizedUserId(user);

        await _familyTreeRepository.CreateAsync(
            new FamilyTree
            {
                Id = IdGenerator.GenerateUlid(),
                Name = familyTreeRequest.Name,
                CreatedUserId = userId,
            }
        );
    }

    public async Task<FamilyTreeResponse> GetFamilyTreeAsync(string familyTreeId)
    {
        var result = await EnsureAndGetFamilyTreeAsync(familyTreeId);

        return FamilyTreeResponse.From(result);
    }

    public async Task UpdateFamilyTreeAsync(
        ClaimsPrincipal user,
        string familyTreeId,
        FamilyTreeRequest familyTreeRequest
    )
    {
        var userId = ClaimsManager.GetAuthorizedUserId(user);

        var result = await EnsureAndGetFamilyTreeAsync(familyTreeId);

        await _familyTreeRepository.UpdateAsync(Update(result, familyTreeRequest));

        static FamilyTree Update(FamilyTree familyTree, FamilyTreeRequest familyTreeRequest)
        {
            familyTree.Name = familyTreeRequest.Name;
            return familyTree;
        }
    }

    public async Task DeleteFamilyTreeAsync(string familyTreeId)
    {
        await _familyTreeRepository.DeleteAsync(familyTreeId);
    }

    private async Task<FamilyTree> EnsureAndGetFamilyTreeAsync(string familyTreeId)
    {
        return await _familyTreeRepository.ReadAsync(familyTreeId)
            ?? throw new ResourceNotFoundException();
    }
}
