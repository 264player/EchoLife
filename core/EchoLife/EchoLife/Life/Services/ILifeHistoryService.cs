using System.Security.Claims;
using EchoLife.Life.Dtos;

namespace EchoLife.Life.Services;

public interface ILifeHistoryService
{
    #region LifeHistory
    Task<LifeHistoryResponse> CreateLifeHistoryAsync(
        ClaimsPrincipal me,
        LifeHistoryRequest lifeHistoryRequest
    );
    Task<List<LifeHistoryResponse>> GetMyLifeHistoryAsync(
        ClaimsPrincipal me,
        QueryLifeHistoryRequest queryLifeHistoryRequest
    );
    Task<LifeHistoryResponse?> GetLifeHistoryAsync(ClaimsPrincipal me, string lifeHistoryId);
    Task UpdateLifeHistoryAsync(
        ClaimsPrincipal me,
        string lifeHistoryId,
        LifeHistoryRequest lifeHistoryRequest
    );
    Task DeleteLifeHistoryAsync(ClaimsPrincipal me, string lifeHistoryId);
    #endregion

    #region LifeSubSection
    Task<LifeSubSectionResponse> CreateLifeSubSectionAsync(
        ClaimsPrincipal me,
        LifeSubSectionRequest lifeSubSectionRequest
    );
    Task<List<LifeSubSectionResponse>> GetAllLifeSubSectionAsync(
        ClaimsPrincipal me,
        string historyId
    );
    Task<LifeSubSectionResponse?> GetLifeSubSectionAsync(ClaimsPrincipal me, string sectionId);
    Task UpdateLifeSubSectionAsync(
        ClaimsPrincipal me,
        string sectionId,
        LifeSubSectionRequest lifeSubSection
    );
    Task DeleteLifeSubSectionAsync(ClaimsPrincipal me, string sectionId);
    #endregion
    Task<string> AiPolishAync(ClaimsPrincipal me, string sectionId);
}
