using System.Security.Claims;
using EchoLife.Account.Services;
using EchoLife.Common;
using EchoLife.Common.Exceptions;
using EchoLife.Life.Data;
using EchoLife.Life.Dtos;
using EchoLife.Life.Models;

namespace EchoLife.Life.Services;

public class LifeHistoryService(
    ILifeHitoryRepository _lifeHitoryRepository,
    ILifeSubSectionRepository _lifeSubSectionRepository
) : ILifeHistoryService
{
    #region LifeHistory
    public async Task CreateLifeHistoryAsync(ClaimsPrincipal me, LifeHistoryRequest lifeHistory)
    {
        var myId = ClaimsManager.EnsureGetUserId(me)!;

        await _lifeHitoryRepository.CreateAsync(
            new LifeHistory
            {
                Id = IdGenerator.GenerateUlid(),
                Title = lifeHistory.Title,
                UserId = myId,
            }
        );
    }

    public async Task<List<LifeHistory>> GetMyLifeHistoryAsync(
        ClaimsPrincipal me,
        QueryLifeHistoryRequest queryLifeHistoryRequest
    )
    {
        var myId = ClaimsManager.EnsureGetUserId(me);

        var result = await _lifeHitoryRepository.ReadAsync(
            h => h.UserId == myId,
            queryLifeHistoryRequest.CursorId,
            queryLifeHistoryRequest.Count
        );

        return result;
    }

    public async Task<LifeHistory?> GetLifeHistoryAsync(ClaimsPrincipal me, string lifeHistoryId)
    {
        var myId = ClaimsManager.EnsureGetUserId(me);

        var result = await EnsureAndGetLifeHistoryAsync(lifeHistoryId);
        if (result.UserId != myId)
        {
            throw new ForbiddenException();
        }

        return result;
    }

    public async Task UpdateLifeHistoryAsync(
        ClaimsPrincipal me,
        string lifeHistoryId,
        LifeHistoryRequest lifeHistoryRequest
    )
    {
        var lifeHitsory = await EnsureAndGetLifeHistoryAsync(lifeHistoryId);

        var myId = ClaimsManager.EnsureGetUserId(me);
        if (lifeHitsory.UserId != myId)
        {
            throw new ForbiddenException();
        }

        await _lifeHitoryRepository.UpdateAsync(Update(lifeHitsory, lifeHistoryRequest));

        static LifeHistory Update(LifeHistory lifeHistory, LifeHistoryRequest lifeHistoryRequest)
        {
            lifeHistory.Title = lifeHistoryRequest.Title;
            return lifeHistory;
        }
    }

    public async Task DeleteLifeHistoryAsync(ClaimsPrincipal me, string lifeHistoryId)
    {
        var myId = ClaimsManager.EnsureGetUserId(me);

        var result = await EnsureAndGetLifeHistoryAsync(lifeHistoryId);
        if (result.UserId != myId)
        {
            throw new ForbiddenException();
        }

        await _lifeHitoryRepository.DeleteAsync(lifeHistoryId);
    }

    private async Task<LifeHistory> EnsureAndGetLifeHistoryAsync(string lifeHistoryId)
    {
        return await _lifeHitoryRepository.ReadAsync(lifeHistoryId)
            ?? throw new ResourceNotFoundException();
    }
    #endregion

    #region LifeSubSection
    public async Task CreateLifeSubSectionAsync(
        ClaimsPrincipal me,
        LifeSubSectionRequest lifeSubSectionRequest
    )
    {
        var userId = ClaimsManager.EnsureGetUserId(me);

        var history = await EnsureAndGetLifeHistoryAsync(lifeSubSectionRequest.LifeHistoryId);

        if (userId != history.UserId)
        {
            throw new ForbiddenException();
        }

        await _lifeSubSectionRepository.CreateAsync(
            new LifeSubSection
            {
                Id = IdGenerator.GenerateUlid(),
                Title = lifeSubSectionRequest.Title,
                Content = lifeSubSectionRequest.Content,
                LifeHistoryId = lifeSubSectionRequest.LifeHistoryId,
                FatherId = lifeSubSectionRequest.FatherId,
                Deep = lifeSubSectionRequest.Deep,
            }
        );
    }

    public async Task<List<LifeSubSectionResponse>> GetLifeSubSectionAsync(
        ClaimsPrincipal me,
        string historyId,
        QueryLifeSubSectionRequest queryLifeSubSectionRequest
    )
    {
        var userId = ClaimsManager.EnsureGetUserId(me);

        var history = await EnsureAndGetLifeHistoryAsync(historyId);

        var result = await _lifeSubSectionRepository.ReadAsync(
            s => s.LifeHistoryId == historyId,
            queryLifeSubSectionRequest.CursorId,
            queryLifeSubSectionRequest.Count
        );
        return [.. result.Select(LifeSubSectionResponse.From)];
    }

    public async Task<LifeSubSectionResponse?> GetLifeSubSectionAsync(
        ClaimsPrincipal me,
        string sectionId
    )
    {
        var result = await EnsureAndGetLifeSubSectionAsync(sectionId);

        var history = await EnsureAndGetLifeHistoryAsync(result.LifeHistoryId);

        if (history.UserId != ClaimsManager.EnsureGetUserId(me))
        {
            throw new ForbiddenException();
        }

        return LifeSubSectionResponse.From(result);
    }

    public async Task UpdateLifeSubSectionAsync(
        ClaimsPrincipal me,
        string sectionId,
        LifeSubSectionRequest lifeSubSectionRequest
    )
    {
        var result = await EnsureAndGetLifeSubSectionAsync(sectionId);

        var history = await EnsureAndGetLifeHistoryAsync(result.LifeHistoryId);

        if (history.UserId != ClaimsManager.EnsureGetUserId(me))
        {
            throw new ForbiddenException();
        }

        await _lifeSubSectionRepository.UpdateAsync(Update(result, lifeSubSectionRequest));

        static LifeSubSection Update(
            LifeSubSection lifeSubSection,
            LifeSubSectionRequest lifeSubSectionRequest
        )
        {
            lifeSubSection.Title = lifeSubSectionRequest.Title;
            lifeSubSection.Content = lifeSubSectionRequest.Content;
            return lifeSubSection;
        }
    }

    public async Task DeleteLifeSubSectionAsync(ClaimsPrincipal me, string sectionId)
    {
        var result = await EnsureAndGetLifeSubSectionAsync(sectionId);

        var history = await EnsureAndGetLifeHistoryAsync(result.LifeHistoryId);

        if (history.UserId != ClaimsManager.EnsureGetUserId(me))
        {
            throw new ForbiddenException();
        }

        await _lifeSubSectionRepository.DeleteAsync(sectionId);
    }

    private async Task<LifeSubSection> EnsureAndGetLifeSubSectionAsync(string sectionId)
    {
        return await _lifeSubSectionRepository.ReadAsync(sectionId)
            ?? throw new ResourceNotFoundException();
    }
    #endregion
}
