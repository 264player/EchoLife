using System.Security.Claims;
using EchoLife.Life.Dtos;
using EchoLife.Life.Models;

namespace EchoLife.Life.Services;

public interface ILifeHistoryService
{
    #region LifeHistory
    Task CreateLifeHistoryAsync(ClaimsPrincipal me, LifeHistoryRequest lifeHistoryRequest);
    Task<List<LifeHistory>> GetMyLifeHistoryAsync(
        ClaimsPrincipal me,
        QueryLifeHistoryRequest queryLifeHistoryRequest
    );
    Task<LifeHistory?> GetLifeHistoryAsync(ClaimsPrincipal me, string lifeHistoryId);
    Task UpdateLifeHistoryAsync(
        ClaimsPrincipal me,
        string lifeHistoryId,
        LifeHistoryRequest lifeHistoryRequest
    );
    Task DeleteLifeHistoryAsync(ClaimsPrincipal me, string lifeHistoryId);
    #endregion

    #region LifeSubSection
    Task CreateLifeSubSectionAsync(ClaimsPrincipal me, LifeSubSectionRequest lifeSubSectionRequest);
    Task<List<LifeSubSectionResponse>> GetLifeSubSectionAsync(
        ClaimsPrincipal me,
        string historyId,
        QueryLifeSubSectionRequest queryLifeSubSectionRequest
    );
    Task<LifeSubSectionResponse?> GetLifeSubSectionAsync(ClaimsPrincipal me, string sectionId);
    Task UpdateLifeSubSectionAsync(
        ClaimsPrincipal me,
        string sectionId,
        LifeSubSectionRequest lifeSubSection
    );
    Task DeleteLifeSubSectionAsync(ClaimsPrincipal me, string sectionId);
    #endregion
}
