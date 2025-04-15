using System.Security.Claims;
using EchoLife.Family.Dtos;

namespace EchoLife.Family.Services;

public interface IFamilyTreeService
{
    Task CreateFamilyTreeAsync(ClaimsPrincipal user, FamilyTreeRequest familyTreeRequest);
    Task<FamilyTreeResponse> GetFamilyTreeAsync(string familyTreeId);
    Task UpdateFamilyTreeAsync(
        ClaimsPrincipal user,
        string familyTreeId,
        FamilyTreeRequest familyTreeRequest
    );
    Task DeleteFamilyTreeAsync(string familyTreeId);
}
