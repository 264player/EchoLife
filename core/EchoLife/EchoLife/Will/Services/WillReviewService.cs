using System.Security.Claims;
using EchoLife.Account.Models;
using EchoLife.Account.Services;
using EchoLife.Common;
using EchoLife.Common.AIAgent.TogetherAI.Text.Services;
using EchoLife.Common.Exceptions;
using EchoLife.Will.Data;
using EchoLife.Will.Dtos;
using EchoLife.Will.Models;

namespace EchoLife.Will.Services;

public class WillReviewService(
    IWillReviewRepository _willReviewRepository,
    IWillService _willService,
    ITextToTextClient _textToTextClient
) : IWillReviewService
{
    public async Task<WillReviewResponse> RequestHumanReviewAsync(
        ClaimsPrincipal me,
        string willVersinoId
    )
    {
        ClaimsManager.EnsureRole(me, AccountRoles.User);

        var willVersion = await _willService.GetWillVersionAsync(willVersinoId);

        var result =
            await _willReviewRepository.CreateAsync(
                new WillReview { Id = IdGenerator.GenerateUlid(), VersionId = willVersinoId }
            ) ?? throw new UnknowException();

        return WillReviewResponse.From(result, willVersion);
    }

    public async Task<WillReviewResponse> RequestAIReviewAsync(
        ClaimsPrincipal me,
        string willVersinoId
    )
    {
        ClaimsManager.EnsureRole(me, AccountRoles.User);

        var willVersion = await _willService.GetWillVersionAsync(willVersinoId);

        var comment = await _textToTextClient.TalkAsync(willVersion.Value);

        var result =
            await _willReviewRepository.CreateAsync(
                new WillReview
                {
                    Id = IdGenerator.GenerateUlid(),
                    VersionId = willVersinoId,
                    Comments = comment,
                    ReviewedAt = DateTime.UtcNow,
                }
            ) ?? throw new UnknowException();

        return WillReviewResponse.From(result, willVersion);
    }

    public async Task<WillReviewResponse> GetReviewAsync(ClaimsPrincipal me, string reviewId)
    {
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var result =
            await _willReviewRepository.ReadAsync(reviewId)
            ?? throw new WillReviewNotFoundException(reviewId);

        var version = await _willService.GetWillVersionAsync(result.VersionId);

        if (result.ReviewerId != myId || result.UserId != myId)
        {
            throw new ForbiddenException(myId);
        }

        return WillReviewResponse.From(result, version);
    }

    public async Task<List<WillReviewResponse>> GetMyReviewRequestAsync(
        ClaimsPrincipal me,
        int count,
        string? cusorId
    )
    {
        ClaimsManager.EnsureRole(me, AccountRoles.User);
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var result = await _willReviewRepository.ReadAsync(
            r => (r.UserId == myId) && (cusorId == null || r.Id.CompareTo(cusorId) < 0),
            count
        );

        var versions = await _willService.GetWillVersionsAsync(
            [.. result.Select(r => r.VersionId)]
        );

        return
        [
            .. versions.Select(v =>
                WillReviewResponse.From(result.First(r => r.VersionId == v.Id), v)
            ),
        ];
    }

    public async Task<List<WillReviewResponse>> GetMyReviewAsync(
        ClaimsPrincipal me,
        int count,
        string? cusorId
    )
    {
        ClaimsManager.EnsureRole(me, AccountRoles.Reviewer);
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var result = await _willReviewRepository.ReadAsync(
            r => (r.ReviewerId == myId) && (cusorId == null || r.Id.CompareTo(cusorId) < 0),
            count
        );

        var versions = await _willService.GetWillVersionsAsync(
            [.. result.Select(r => r.VersionId)]
        );

        return
        [
            .. versions.Select(v =>
                WillReviewResponse.From(result.First(r => r.VersionId == v.Id), v)
            ),
        ];
    }

    public async Task<List<WillReviewResponse>> GetAllReviewRequestAsync(
        ClaimsPrincipal me,
        int count,
        string? cusorId
    )
    {
        ClaimsManager.EnsureRole(me, AccountRoles.Reviewer);
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var result = await _willReviewRepository.ReadAsync(
            r =>
                (r.Status == WillReviewStatus.Pending)
                && (cusorId == null || r.Id.CompareTo(cusorId) < 0),
            count
        );

        var versions = await _willService.GetWillVersionsAsync(
            [.. result.Select(r => r.VersionId)]
        );

        return
        [
            .. versions.Select(v =>
                WillReviewResponse.From(result.First(r => r.VersionId == v.Id), v)
            ),
        ];
    }

    public async Task ProcessReview(ClaimsPrincipal me, string reviewId)
    {
        ClaimsManager.EnsureRole(me, AccountRoles.Reviewer);
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var review = await EnsureAndGetAsync(reviewId);

        if (review.Status != WillReviewStatus.Pending)
        { // throw bussiness exception
            return;
        }

        review.ReviewerId = myId;
        review.Status = WillReviewStatus.InProgress;

        await _willReviewRepository.UpdateAsync(review);
    }

    public async Task CompleteReview(
        ClaimsPrincipal me,
        string reviewId,
        string comment,
        WillReviewStatus status
    )
    {
        ClaimsManager.EnsureRole(me, AccountRoles.Reviewer);
        var myId = ClaimsManager.GetAuthorizedUserId(me);

        var review = await EnsureAndGetAsync(reviewId);

        if (review.ReviewerId != myId)
        {
            throw new ForbiddenException(myId);
        }

        if (review.Status != WillReviewStatus.InProgress)
        { // throw bussiness exception
            return;
        }

        review.Comments = comment;
        review.Status = status;
        review.ReviewedAt = DateTime.UtcNow;
        await _willReviewRepository.UpdateAsync(review);
    }

    protected async Task<WillReview> EnsureAndGetAsync(string reviewId)
    {
        return await _willReviewRepository.ReadAsync(reviewId)
            ?? throw new WillReviewNotFoundException(reviewId);
    }
}
