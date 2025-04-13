using System.Security.Claims;
using EchoLife.Family.Dtos;

namespace EchoLife.Family.Services;

public interface IFamilyHistoryService
{
    #region FamilyHistory
    Task CreateFamilyHistoryAsync(ClaimsPrincipal me, FamilyHistoryRequest familyHistoryRequest);
    Task<List<FamilyHistoryResponse>> GetFamilyHistoryAsync(
        ClaimsPrincipal me,
        QueyFamilyHistoryRequest queyFamilyHistoryRequest
    );
    Task<FamilyHistoryResponse?> GetFamilyHistoryAsync(ClaimsPrincipal me, string lifeHistoryId);
    Task UpdateFamilyHistoryAsync(
        ClaimsPrincipal me,
        string lifeHistoryId,
        FamilyHistoryRequest familyHistoryRequest
    );
    Task DeleteFamilyHistoryAsync(ClaimsPrincipal me, string lifeHistoryId);
    #endregion

    #region FamilySubSection
    Task CreateFamilySubSectionAsync(
        ClaimsPrincipal me,
        FamilySubSectionRequest familySubSectionRequest
    );
    Task<List<FamilySubSectionResponse>> GetFamilySubSectionAsync(
        ClaimsPrincipal me,
        string historyId,
        QueryFamilySubSectionRequest queryFamilySubSectionRequest
    );
    Task<FamilySubSectionResponse?> GetFamilySubSectionAsync(ClaimsPrincipal me, string sectionId);
    Task UpdateFamilySubSectionAsync(
        ClaimsPrincipal me,
        string sectionId,
        FamilySubSectionRequest familySubSectionRequest
    );
    Task DeleteFamilySubSectionAsync(ClaimsPrincipal me, string sectionId);
    #endregion
}
