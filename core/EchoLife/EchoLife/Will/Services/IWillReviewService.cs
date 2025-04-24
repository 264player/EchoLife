using System.Security.Claims;
using EchoLife.Will.Dtos;
using EchoLife.Will.Models;

namespace EchoLife.Will.Services;

public interface IWillReviewService
{
    Task<WillReviewResponse> RequestHumanReviewAsync(ClaimsPrincipal me, string willVersinoId);
    Task<WillReviewResponse> RequestAIReviewAsync(ClaimsPrincipal me, string willVersinoId);
    Task<WillReviewResponse> GetReviewAsync(ClaimsPrincipal me, string reviewId);
    Task<List<WillReviewResponse>> GetMyReviewAsync(ClaimsPrincipal me, int count, string? cusorId);
    Task<List<WillReviewResponse>> GetMyReviewRequestAsync(
        ClaimsPrincipal me,
        int count,
        string? cusorId
    );
    Task<List<WillReviewResponse>> GetAllReviewRequestAsync(
        ClaimsPrincipal me,
        int count,
        string? cusorId
    );
    Task ProcessReview(ClaimsPrincipal me, string reviewId);
    Task CompleteReview(
        ClaimsPrincipal me,
        string reviewId,
        string comment,
        WillReviewStatus status
    );
}
