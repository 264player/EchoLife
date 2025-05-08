using System.Security.Claims;
using EchoLife.Account.Models;
using EchoLife.Account.Services;
using EchoLife.Common;
using EchoLife.Common.AIAgent.TogetherAI.Text.Services;
using EchoLife.Common.Exceptions;
using EchoLife.Family.Data;
using EchoLife.Family.Dtos;
using EchoLife.Family.Models;
using EchoLife.Life.Services;
using Microsoft.Extensions.Options;

namespace EchoLife.Family.Services;

public class FamilyHistoryService(
    IFamilyHistoryRepository _familyHistoryRepository,
    IFamilySubSectionRepository _familySubSectionRepository,
    ITextToTextClient _textToTextClient,
    IOptions<TextToTextPrompts> _prompts
) : IFamilyHistoryService
{
    #region FamilyHistory
    public async Task<FamilyHistoryResponse> CreateFamilyHistoryAsync(
        ClaimsPrincipal me,
        FamilyHistoryRequest familyHistory
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me)!;

        var result =
            await _familyHistoryRepository.CreateAsync(
                new FamilyHistory
                {
                    Id = IdGenerator.GenerateUlid(),
                    Title = familyHistory.Title,
                    FamilyId = familyHistory.FamilyId,
                }
            ) ?? throw new UnknowException();
        return FamilyHistoryResponse.From(result);
    }

    public async Task<List<FamilyHistoryResponse>> GetFamilyHistoryAsync(
        ClaimsPrincipal me,
        QueryFamilyHistoryRequest queyFamilyHistoryRequest
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var result = await _familyHistoryRepository.ReadAsync(
            h =>
                h.FamilyId == queyFamilyHistoryRequest.FamilyId
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
        //if (result.FamilyId != myId)
        //{
        //    throw new ForbiddenException(myId);
        //}

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
        //if (familyHistory.FamilyId != myId)
        //{
        //    throw new ForbiddenException(myId);
        //}

        await _familyHistoryRepository.UpdateAsync(Update(familyHistory, familyHistoryRequest));

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
        //if (result.familyId != myId)
        //{
        //    throw new ForbiddenException(myId);
        //}

        await _familyHistoryRepository.DeleteAsync(familyHistoryId);
    }

    private async Task<FamilyHistory> EnsureAndGetFamilyHistoryAsync(string lifeHistoryId)
    {
        return await _familyHistoryRepository.ReadAsync(lifeHistoryId)
            ?? throw new FamilyHistoryNotFoundException(lifeHistoryId);
    }
    #endregion

    #region FamilySubSection
    public async Task<FamilySubSectionResponse> CreateFamilySubSectionAsync(
        ClaimsPrincipal me,
        FamilySubSectionRequest familySubSectionRequest
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var history = await EnsureAndGetFamilyHistoryAsync(familySubSectionRequest.FamilyHistoryId);

        //if (myId != history.FamilyId)
        //{
        //    throw new ForbiddenException(myId);
        //}

        var result =
            await _familySubSectionRepository.CreateAsync(
                new FamilySubSection
                {
                    Id = IdGenerator.GenerateUlid(),
                    Title = familySubSectionRequest.Title,
                    Content = familySubSectionRequest.Content,
                    FamilyHistoryId = familySubSectionRequest.FamilyHistoryId,
                    FatherId = familySubSectionRequest.FatherId,
                    Index = familySubSectionRequest.Index,
                }
            ) ?? throw new UnknowException();
        return FamilySubSectionResponse.From(result);
    }

    public async Task<List<FamilySubSectionResponse>> GetFamilySubSectionAsync(
        ClaimsPrincipal me,
        string historyId,
        QueryFamilySubSectionRequest queryFamilySubSectionRequest
    )
    {
        var userId = ClaimsManager.GetAuthorizedUserId(me);

        var history = await EnsureAndGetFamilyHistoryAsync(historyId);

        var result = await _familySubSectionRepository.ReadAsync(
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

    public async Task<List<FamilySubSectionResponse>> GetAllFamilySubSectionAsync(
        ClaimsPrincipal me,
        string historyId
    )
    {
        var userId = ClaimsManager.GetAuthorizedUserId(me);

        var history = await EnsureAndGetFamilyHistoryAsync(historyId);

        var result = await _familySubSectionRepository.ReadAllAsync(historyId);

        return [.. SortToTree(result).Select(FamilySubSectionResponse.From)];

        List<FamilySubSection> SortToTree(List<FamilySubSection> sections)
        {
            var insertPosition = new Dictionary<string, string>();
            var result = new List<FamilySubSection>();

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

    public async Task<FamilySubSectionResponse?> GetFamilySubSectionAsync(
        ClaimsPrincipal me,
        string sectionId
    )
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);
        var result = await EnsureAndGetLifeSubSectionAsync(sectionId);

        var history = await EnsureAndGetFamilyHistoryAsync(result.FamilyHistoryId);

        //if (history.familyId != myId)
        //{
        //    throw new ForbiddenException(myId);
        //}

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

        //if (history.familyId != myId)
        //{
        //    throw new ForbiddenException(myId);
        //}

        await _familySubSectionRepository.UpdateAsync(Update(result, lifeSubSectionRequest));

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

        //if (history.familyId != myId)
        //{
        //    throw new ForbiddenException(myId);
        //}

        await _familySubSectionRepository.DeleteAsync(sectionId);
    }

    private async Task<FamilySubSection> EnsureAndGetLifeSubSectionAsync(string sectionId)
    {
        return await _familySubSectionRepository.ReadAsync(sectionId)
            ?? throw new LifeSubSectionNotFoundException(sectionId);
    }
    #endregion

    public async Task<string> AiPolishAync(ClaimsPrincipal me, string sectionId)
    {
        ClaimsManager.EnsureRole(me, AccountRoles.User);
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var section =
            await _familySubSectionRepository.ReadAsync(sectionId)
            ?? throw new LifeSubSectionNotFoundException(sectionId);

        var comment = await _textToTextClient.TalkAsync(
            $"{section.Title}\n{section.Content}",
            _prompts.Value.FamilyHitstoryPolishPrompt
        );

        return comment;
    }
}
