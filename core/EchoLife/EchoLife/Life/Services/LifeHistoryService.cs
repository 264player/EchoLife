using EchoLife.Common.Exceptions;
using EchoLife.Life.Data;
using EchoLife.Life.Models;

namespace EchoLife.Life.Services;

public class LifeHistoryService(
    ILifeHitoryRepository _lifeHitoryRepository,
    ILifeSubSectionRepository _lifeSubSectionRepository
) : ILifeHistoryService
{
    #region LifeHistory
    public async Task CreateLifeHistoryAsync(LifeHistory lifeHistory)
    {
        await _lifeHitoryRepository.CreateAsync(lifeHistory);
    }

    public async Task<LifeHistory?> GetLifeHistoryAsync(string userId, string lifeHistoryId)
    {
        var result = await _lifeHitoryRepository.ReadAsync(lifeHistoryId);
        if (result != null && result.UserId != userId)
        {
            throw new ForbiddenException();
        }
        return result;
    }

    public async Task UpdateLifeHistoryAsync(LifeHistory lifeHistory)
    {
        await _lifeHitoryRepository.UpdateAsync(lifeHistory);
    }

    public async Task DeleteLifeHistoryAsync(string userId, string lifeHistoryId)
    {
        var result = await _lifeHitoryRepository.ReadAsync(lifeHistoryId);
        if (result != null && result.UserId != userId)
        {
            throw new ForbiddenException();
        }

        if (result != null)
        {
            await _lifeHitoryRepository.DeleteAsync(lifeHistoryId);
        }
    }
    #endregion
    #region LifeSubSection
    public async Task CreateLifeSubSectionAsync(LifeSubSection lifeSubSection)
    {
        await _lifeSubSectionRepository.CreateAsync(lifeSubSection);
    }

    public async Task<LifeSubSection?> GetLifeSubSectionAsync(string userId, string sectionId)
    {
        var result = await _lifeSubSectionRepository.ReadAsync(sectionId);

        if (result == null)
        {
            return result;
        }

        var history = await _lifeHitoryRepository.ReadAsync(result.LifeHistoryId);
        if (history != null && history.UserId != userId)
        {
            throw new ForbiddenException();
        }
        return result;
    }

    public async Task UpdateLifeSubSectionAsync(LifeSubSection lifeSubSection)
    {
        await _lifeSubSectionRepository.UpdateAsync(lifeSubSection);
    }

    public async Task DeleteLifeSubSectionAsync(string userId, string sectionId)
    {
        var result = await _lifeSubSectionRepository.ReadAsync(sectionId);

        if (result == null)
        {
            return;
        }

        var history = await _lifeHitoryRepository.ReadAsync(result.LifeHistoryId);
        if (history != null && history.UserId != userId)
        {
            throw new ForbiddenException();
        }

        await _lifeSubSectionRepository.DeleteAsync(sectionId);
    }
    #endregion
}
