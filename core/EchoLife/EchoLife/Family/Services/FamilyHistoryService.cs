using System.Security.Claims;
using EchoLife.Account.Services;
using EchoLife.Common;
using EchoLife.Common.Exceptions;
using EchoLife.Family.Data;
using EchoLife.Family.Dtos;
using EchoLife.Family.Models;

namespace EchoLife.Family.Services;

public class FamilyHistoryService(
    IFamilyHistoryRepository familyHistoryRepository,
    IFamilySubSectionRepository familySubSectionRepository
) : IFamilyHistoryService
{
    #region LifeHistory
    public async Task CreateFamilyHistoryAsync(ClaimsPrincipal me, FamilyHistoryRequest lifeHistory)
    {
        var myId = ClaimsManager.EnsureGetUserId(me)!;

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
        QueyFamilyHistoryRequest queyFamilyHistoryRequest
    )
    {
        var myId = ClaimsManager.EnsureGetUserId(me);

        var result = await familyHistoryRepository.ReadAsync(
            h => h.UserId == myId,
            queyFamilyHistoryRequest.CursorId,
            queyFamilyHistoryRequest.Count
        );

        return [.. result.Select(FamilyHistoryResponse.From)];
    }

    public async Task<FamilyHistoryResponse?> GetFamilyHistoryAsync(
        ClaimsPrincipal me,
        string familyHistoryId
    )
    {
        var myId = ClaimsManager.EnsureGetUserId(me);

        var result = await EnsureAndGetFamilyHistoryAsync(familyHistoryId);
        if (result.UserId != myId)
        {
            throw new ForbiddenException();
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

        var myId = ClaimsManager.EnsureGetUserId(me);
        if (familyHistory.UserId != myId)
        {
            throw new ForbiddenException();
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
        var myId = ClaimsManager.EnsureGetUserId(me);

        var result = await EnsureAndGetFamilyHistoryAsync(familyHistoryId);
        if (result.UserId != myId)
        {
            throw new ForbiddenException();
        }

        await familyHistoryRepository.DeleteAsync(familyHistoryId);
    }

    private async Task<FamilyHistory> EnsureAndGetFamilyHistoryAsync(string lifeHistoryId)
    {
        return await familyHistoryRepository.ReadAsync(lifeHistoryId)
            ?? throw new ResourceNotFoundException();
    }
    #endregion

    #region LifeSubSection
    public async Task CreateFamilySubSectionAsync(
        ClaimsPrincipal me,
        FamilySubSectionRequest familySubSectionRequest
    )
    {
        var userId = ClaimsManager.EnsureGetUserId(me);

        var history = await EnsureAndGetFamilyHistoryAsync(familySubSectionRequest.FamilyHistoryId);

        if (userId != history.UserId)
        {
            throw new ForbiddenException();
        }

        await familySubSectionRepository.CreateAsync(
            new FamilySubSection
            {
                Id = IdGenerator.GenerateUlid(),
                Title = familySubSectionRequest.Title,
                Content = familySubSectionRequest.Content,
                FamilyHistoryId = familySubSectionRequest.FamilyHistoryId,
                FatherId = familySubSectionRequest.FatherId,
                Deep = familySubSectionRequest.Deep,
            }
        );
    }

    public async Task<List<FamilySubSectionResponse>> GetFamilySubSectionAsync(
        ClaimsPrincipal me,
        string historyId,
        QueryFamilySubSectionRequest queryLifeSubSectionRequest
    )
    {
        var userId = ClaimsManager.EnsureGetUserId(me);

        var history = await EnsureAndGetFamilyHistoryAsync(historyId);

        var result = await familySubSectionRepository.ReadAsync(
            s => s.FamilyHistoryId == historyId,
            queryLifeSubSectionRequest.CursorId,
            queryLifeSubSectionRequest.Count
        );
        return [.. result.Select(FamilySubSectionResponse.From)];
    }

    public async Task<FamilySubSectionResponse?> GetFamilySubSectionAsync(
        ClaimsPrincipal me,
        string sectionId
    )
    {
        var result = await EnsureAndGetLifeSubSectionAsync(sectionId);

        var history = await EnsureAndGetFamilyHistoryAsync(result.FamilyHistoryId);

        if (history.UserId != ClaimsManager.EnsureGetUserId(me))
        {
            throw new ForbiddenException();
        }

        return FamilySubSectionResponse.From(result);
    }

    public async Task UpdateFamilySubSectionAsync(
        ClaimsPrincipal me,
        string sectionId,
        FamilySubSectionRequest lifeSubSectionRequest
    )
    {
        var result = await EnsureAndGetLifeSubSectionAsync(sectionId);

        var history = await EnsureAndGetFamilyHistoryAsync(result.FamilyHistoryId);

        if (history.UserId != ClaimsManager.EnsureGetUserId(me))
        {
            throw new ForbiddenException();
        }

        await familySubSectionRepository.UpdateAsync(Update(result, lifeSubSectionRequest));

        static FamilySubSection Update(
            FamilySubSection lifeSubSection,
            FamilySubSectionRequest lifeSubSectionRequest
        )
        {
            lifeSubSection.Title = lifeSubSectionRequest.Title;
            lifeSubSection.Content = lifeSubSectionRequest.Content;
            return lifeSubSection;
        }
    }

    public async Task DeleteFamilySubSectionAsync(ClaimsPrincipal me, string sectionId)
    {
        var result = await EnsureAndGetLifeSubSectionAsync(sectionId);

        var history = await EnsureAndGetFamilyHistoryAsync(result.FamilyHistoryId);

        if (history.UserId != ClaimsManager.EnsureGetUserId(me))
        {
            throw new ForbiddenException();
        }

        await familySubSectionRepository.DeleteAsync(sectionId);
    }

    private async Task<FamilySubSection> EnsureAndGetLifeSubSectionAsync(string sectionId)
    {
        return await familySubSectionRepository.ReadAsync(sectionId)
            ?? throw new ResourceNotFoundException();
    }
    #endregion
}
