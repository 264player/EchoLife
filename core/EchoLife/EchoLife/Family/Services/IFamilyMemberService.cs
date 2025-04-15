using System.Security.Claims;
using EchoLife.Family.Dtos;

namespace EchoLife.Family.Services;

public interface IFamilyMemberService
{
    Task CreateFamilyMemberAsync(ClaimsPrincipal user, FamilyMemberRequest familyMemberRequest);
    Task<FamilyMemberResponse> GetFamilyMemberAsync(ClaimsPrincipal user, string familyTreeId);
    Task<List<FamilyMemberResponse>> GetFamilyMemberAsync(string familyTreeId);
    Task UpdateFamilyMemberAsync(FamilyMemberRequest familyMemberRequest);
    Task DeleteFamilyMemberAsync(string familyMemberId);
}
