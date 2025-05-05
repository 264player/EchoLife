using System.Security.Claims;
using EchoLife.Family.Dtos;

namespace EchoLife.Family.Services;

public interface IFamilyService
{
    #region Family
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
    #endregion

    #region Family Member
    Task<FamilyMemberResponse> CreateFamilyMemberAsync(
        ClaimsPrincipal user,
        FamilyMemberRequest familyMemberRequest
    );
    Task<List<FamilyMemberResponse>> GetFamilyMembersAsync(string familyTreeId);
    Task<FamilyMemberResponse> UpdateFamilyMemberAsync(
        ClaimsPrincipal me,
        FamilyMemberRequest familyMemberRequest
    );
    Task<FamilyMemberResponse> GetFamilyMemberAsync(string memberId);
    Task DeleteFamilyMemberAsync(ClaimsPrincipal me, string familyMemberId);
    #endregion
}
