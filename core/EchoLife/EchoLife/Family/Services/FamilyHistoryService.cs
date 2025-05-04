using System.Security.Claims;
using EchoLife.Account.Services;
using EchoLife.Common;
using EchoLife.Common.Exceptions;
using EchoLife.Family.Data;
using EchoLife.Family.Dtos;
using EchoLife.Family.Models;
using EchoLife.Life.Services;

namespace EchoLife.Family.Services;

public class FamilyHistoryService(
    IFamilyHistoryRepository familyHistoryRepository,
    IFamilySubSectionRepository familySubSectionRepository
) : IFamilyHistoryService
{
    #region FamilyHistory
    public async Task CreateFamilyHistoryAsync(ClaimsPrincipal me, FamilyHistoryRequest lifeHistory)
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me)!;

        await familyHistoryRepository.CreateAsync(
            new FamilyHistory
            {
                Id = IdGenerator.GenerateUlid(),
                Title = lifeHistory.Title,
                UserId = myId,
            }
        );
    }

    public async Task<List<FamilyHistoryResponse>> GetFamilyHistoryAsync(
        ClaimsPrincipal me,
        QueryFamilyHistoryRequest queyFamilyHistoryRequest
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var result = await familyHistoryRepository.ReadAsync(
            h =>
                h.UserId == myId
                && (
                    queyFamilyHistoryRequest.CursorId == null
                    || h.Id.CompareTo(queyFamilyHistoryRequest.CursorId) < 0
                ),
            queyFamilyHistoryRequest.Count
        );

        return [.. result.Select(FamilyHistoryResponse.From)];
    }

    public async Task<FamilyHistoryResponse?> GetFamilyHistoryAsync(
        ClaimsPrincipal me,
        string familyHistoryId
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var result = await EnsureAndGetFamilyHistoryAsync(familyHistoryId);
        if (result.UserId != myId)
        {
            throw new ForbiddenException(myId);
        }

        return FamilyHistoryResponse.From(result);
    }

    public async Task UpdateFamilyHistoryAsync(
        ClaimsPrincipal me,
        string familyHistoryId,
        FamilyHistoryRequest familyHistoryRequest
    )
    {
        var familyHistory = await EnsureAndGetFamilyHistoryAsync(familyHistoryId);

        var myId = ClaimsManager.GetAuthorizedUserId(me);
        if (familyHistory.UserId != myId)
        {
            throw new ForbiddenException(myId);
        }

        await familyHistoryRepository.UpdateAsync(Update(familyHistory, familyHistoryRequest));

        static FamilyHistory Update(
            FamilyHistory lifeHistory,
            FamilyHistoryRequest lifeHistoryRequest
        )
        {
            lifeHistory.Title = lifeHistoryRequest.Title;
            return lifeHistory;
        }
    }

    public async Task DeleteFamilyHistoryAsync(ClaimsPrincipal me, string familyHistoryId)
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var result = await EnsureAndGetFamilyHistoryAsync(familyHistoryId);
        if (result.UserId != myId)
        {
            throw new ForbiddenException(myId);
        }

        await familyHistoryRepository.DeleteAsync(familyHistoryId);
    }

    private async Task<FamilyHistory> EnsureAndGetFamilyHistoryAsync(string lifeHistoryId)
    {
        return await familyHistoryRepository.ReadAsync(lifeHistoryId)
            ?? throw new FamilyHistoryNotFoundException(lifeHistoryId);
    }
    #endregion

    #region FamilySubSection
    public async Task CreateFamilySubSectionAsync(
        ClaimsPrincipal me,
        FamilySubSectionRequest familySubSectionRequest
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var history = await EnsureAndGetFamilyHistoryAsync(familySubSectionRequest.FamilyHistoryId);

        if (myId != history.UserId)
        {
            throw new ForbiddenException(myId);
        }

        await familySubSectionRepository.CreateAsync(
            new FamilySubSection
            {
                Id = IdGenerator.GenerateUlid(),
                Title = familySubSectionRequest.Title,
                Content = familySubSectionRequest.Content,
                FamilyHistoryId = familySubSectionRequest.FamilyHistoryId,
                FatherId = familySubSectionRequest.FatherId,
                Index = familySubSectionRequest.Index,
            }
        );
    }

    public async Task<List<FamilySubSectionResponse>> GetFamilySubSectionAsync(
        ClaimsPrincipal me,
        string historyId,
        QueryFamilySubSectionRequest queryFamilySubSectionRequest
    )
    {
        var userId = ClaimsManager.GetAuthorizedUserId(me);

        var history = await EnsureAndGetFamilyHistoryAsync(historyId);

        var result = await familySubSectionRepository.ReadAsync(
            s =>
                s.FamilyHistoryId == historyId
                && (
                    queryFamilySubSectionRequest.CursorId == null
                    || s.Id.CompareTo(queryFamilySubSectionRequest.CursorId) > 0
                ),
            queryFamilySubSectionRequest.Count
        );
        return [.. result.Select(FamilySubSectionResponse.From)];
    }

    public async Task<FamilySubSectionResponse?> GetFamilySubSectionAsync(
        ClaimsPrincipal me,
        string sectionId
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);
        var result = await EnsureAndGetLifeSubSectionAsync(sectionId);

        var history = await EnsureAndGetFamilyHistoryAsync(result.FamilyHistoryId);

        if (history.UserId != myId)
        {
            throw new ForbiddenException(myId);
        }

        return FamilySubSectionResponse.From(result);
    }

    public async Task UpdateFamilySubSectionAsync(
        ClaimsPrincipal me,
        string sectionId,
        FamilySubSectionRequest lifeSubSectionRequest
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);
        var result = await EnsureAndGetLifeSubSectionAsync(sectionId);

        var history = await EnsureAndGetFamilyHistoryAsync(result.FamilyHistoryId);

        if (history.UserId != myId)
        {
            throw new ForbiddenException(myId);
        }

        await familySubSectionRepository.UpdateAsync(Update(result, lifeSubSectionRequest));

        static FamilySubSection Update(
            FamilySubSection lifeSubSection,
            FamilySubSectionRequest lifeSubSectionRequest
        )
        {
            lifeSubSection.Title = lifeSubSectionRequest.Title;
            lifeSubSection.Content = lifeSubSectionRequest.Content;
            lifeSubSection.FatherId = lifeSubSectionRequest.FatherId;
            lifeSubSection.Index = lifeSubSectionRequest.Index;
            return lifeSubSection;
        }
    }

    public async Task DeleteFamilySubSectionAsync(ClaimsPrincipal me, string sectionId)
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);
        var result = await EnsureAndGetLifeSubSectionAsync(sectionId);

        var history = await EnsureAndGetFamilyHistoryAsync(result.FamilyHistoryId);

        if (history.UserId != myId)
        {
            throw new ForbiddenException(myId);
        }

        await familySubSectionRepository.DeleteAsync(sectionId);
    }

    private async Task<FamilySubSection> EnsureAndGetLifeSubSectionAsync(string sectionId)
    {
        return await familySubSectionRepository.ReadAsync(sectionId)
            ?? throw new LifeSubSectionNotFoundException(sectionId);
    }
    #endregion
}
