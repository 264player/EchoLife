using System.Security.Claims;
using EchoLife.Family.Dtos;

namespace EchoLife.Family.Services;

public interface IFamilyService
{
    Task<FamilyTreeResponse> CreateFamilyTreeAsync(
        ClaimsPrincipal user,
        FamilyTreeRequest familyTreeRequest
    );
    Task<FamilyTreeResponse> GetFamilyTreeAsync(ClaimsPrincipal me, string familyTreeId);
    Task<List<FamilyTreeResponse>> GetFamilyTreeAsync(
        ClaimsPrincipal me,
        int count,
        string? cursorId
    );
    Task<FamilyTreeResponse> UpdateFamilyTreeAsync(
        ClaimsPrincipal user,
        string familyTreeId,
        FamilyTreeRequest familyTreeRequest
    );
    Task DeleteFamilyTreeAsync(ClaimsPrincipal me, string familyTreeId);
}
