using EchoLife.Life.Models;

namespace EchoLife.Life.Services;

public interface ILifeHistoryService
{
    #region LifeHistory
    Task CreateLifeHistoryAsync(LifeHistory lifeHistory);
    Task<LifeHistory?> GetLifeHistoryAsync(string userId, string lifeHistoryId);
    Task UpdateLifeHistoryAsync(LifeHistory lifeHistory);
    Task DeleteLifeHistoryAsync(string userId, string lifeHistoryId);
    #endregion
    #region LifeSubSection
    Task CreateLifeSubSectionAsync(LifeSubSection lifeSubSection);
    Task<LifeSubSection?> GetLifeSubSectionAsync(string userId, string sectionId);
    Task UpdateLifeSubSectionAsync(LifeSubSection lifeSubSection);
    Task DeleteLifeSubSectionAsync(string userId, string sectionId);
    #endregion
}
