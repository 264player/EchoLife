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
    public async Task<LifeHistoryResponse> CreateLifeHistoryAsync(
        ClaimsPrincipal me,
        LifeHistoryRequest lifeHistory
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me)!;

        var result =
            await _lifeHitoryRepository.CreateAsync(
                new LifeHistory
                {
                    Id = IdGenerator.GenerateUlid(),
                    Title = lifeHistory.Title,
                    UserId = myId,
                }
            ) ?? throw new UnknowException();
        return LifeHistoryResponse.From(result);
    }

    public async Task<List<LifeHistoryResponse>> GetMyLifeHistoryAsync(
        ClaimsPrincipal me,
        QueryLifeHistoryRequest queryLifeHistoryRequest
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var result = await _lifeHitoryRepository.ReadAsync(
            h =>
                h.UserId == myId
                && (
                    queryLifeHistoryRequest.CursorId == null
                    || h.Id.CompareTo(queryLifeHistoryRequest.CursorId) < 0
                ),
            queryLifeHistoryRequest.Count
        );

        return [.. result.Select(LifeHistoryResponse.From)];
    }

    public async Task<LifeHistoryResponse?> GetLifeHistoryAsync(
        ClaimsPrincipal me,
        string lifeHistoryId
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var result = await EnsureAndGetLifeHistoryAsync(lifeHistoryId);
        if (result.UserId != myId)
        {
            throw new ForbiddenException(myId);
        }

        return LifeHistoryResponse.From(result);
    }

    public async Task UpdateLifeHistoryAsync(
        ClaimsPrincipal me,
        string lifeHistoryId,
        LifeHistoryRequest lifeHistoryRequest
    )
    {
        var lifeHitsory = await EnsureAndGetLifeHistoryAsync(lifeHistoryId);

        var myId = ClaimsManager.GetAuthorizedUserId(me);
        if (lifeHitsory.UserId != myId)
        {
            throw new ForbiddenException(myId);
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
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var result = await EnsureAndGetLifeHistoryAsync(lifeHistoryId);
        if (result.UserId != myId)
        {
            throw new ForbiddenException(myId);
        }

        await _lifeHitoryRepository.DeleteAsync(lifeHistoryId);
    }

    private async Task<LifeHistory> EnsureAndGetLifeHistoryAsync(string lifeHistoryId)
    {
        return await _lifeHitoryRepository.ReadAsync(lifeHistoryId)
            ?? throw new LifeHistoryNotFoundException(lifeHistoryId);
    }
    #endregion

    #region LifeSubSection
    public async Task<LifeSubSectionResponse> CreateLifeSubSectionAsync(
        ClaimsPrincipal me,
        LifeSubSectionRequest lifeSubSectionRequest
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var history = await EnsureAndGetLifeHistoryAsync(lifeSubSectionRequest.LifeHistoryId);

        if (myId != history.UserId)
        {
            throw new ForbiddenException(myId);
        }

        var result =
            await _lifeSubSectionRepository.CreateAsync(
                new LifeSubSection
                {
                    Id = IdGenerator.GenerateUlid(),
                    Title = lifeSubSectionRequest.Title,
                    Content = lifeSubSectionRequest.Content,
                    LifeHistoryId = lifeSubSectionRequest.LifeHistoryId,
                    FatherId = lifeSubSectionRequest.FatherId,
                    Index = lifeSubSectionRequest.Index,
                }
            ) ?? throw new UnknowException();
        return LifeSubSectionResponse.From(result);
    }

    public async Task<List<LifeSubSectionResponse>> GetAllLifeSubSectionAsync(
        ClaimsPrincipal me,
        string historyId
    )
    {
        var userId = ClaimsManager.GetAuthorizedUserId(me);

        var history = await EnsureAndGetLifeHistoryAsync(historyId);

        var result = await _lifeSubSectionRepository.ReadAllSubSectionsAsync(historyId);

        return [.. SortToTree(result).Select(LifeSubSectionResponse.From)];

        List<LifeSubSection> SortToTree(List<LifeSubSection> sections)
        {
            var insertPosition = new Dictionary<string, string>();
            var result = new List<LifeSubSection>();

            foreach (var item in sections)
            {
                if (string.IsNullOrWhiteSpace(item.FatherId))
                {
                    result.Add(item);
                    insertPosition[item.Id] = item.Id;
                }
                else
                {
                    var lastChildId = insertPosition[item.FatherId];

                    var parentIndex = result.FindIndex(n => n.Id == item.FatherId);
                    if (parentIndex != -1)
                    {
                        var lastChildIndex = result.FindIndex(n => n.Id == lastChildId);
                        result.Insert(lastChildIndex + 1, item);
                    }

                    insertPosition[item.FatherId] = item.Id;
                    insertPosition[item.Id] = item.Id;
                }
            }
            return result;
        }
    }

    public async Task<LifeSubSectionResponse?> GetLifeSubSectionAsync(
        ClaimsPrincipal me,
        string sectionId
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var result = await EnsureAndGetLifeSubSectionAsync(sectionId);

        var history = await EnsureAndGetLifeHistoryAsync(result.LifeHistoryId);

        if (history.UserId != myId)
        {
            throw new ForbiddenException(myId);
        }

        return LifeSubSectionResponse.From(result);
    }

    public async Task UpdateLifeSubSectionAsync(
        ClaimsPrincipal me,
        string sectionId,
        LifeSubSectionRequest lifeSubSectionRequest
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);
        var result = await EnsureAndGetLifeSubSectionAsync(sectionId);

        var history = await EnsureAndGetLifeHistoryAsync(result.LifeHistoryId);

        if (history.UserId != myId)
        {
            throw new ForbiddenException(myId);
        }

        await _lifeSubSectionRepository.UpdateAsync(Update(result, lifeSubSectionRequest));

        static LifeSubSection Update(
            LifeSubSection lifeSubSection,
            LifeSubSectionRequest lifeSubSectionRequest
        )
        {
            lifeSubSection.Title = lifeSubSectionRequest.Title;
            lifeSubSection.Content = lifeSubSectionRequest.Content;
            lifeSubSection.Index = lifeSubSectionRequest.Index;
            lifeSubSection.FatherId = lifeSubSectionRequest.FatherId;
            return lifeSubSection;
        }
    }

    public async Task DeleteLifeSubSectionAsync(ClaimsPrincipal me, string sectionId)
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);
        var result = await EnsureAndGetLifeSubSectionAsync(sectionId);

        var history = await EnsureAndGetLifeHistoryAsync(result.LifeHistoryId);

        if (history.UserId != myId)
        {
            throw new ForbiddenException(myId);
        }

        await _lifeSubSectionRepository.DeleteAsync(sectionId);
    }

    private async Task<LifeSubSection> EnsureAndGetLifeSubSectionAsync(string sectionId)
    {
        return await _lifeSubSectionRepository.ReadAsync(sectionId)
            ?? throw new LifeSubSectionNotFoundException(sectionId);
    }
    #endregion
}
