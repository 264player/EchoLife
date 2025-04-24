using EchoLife.Will.Models;

namespace EchoLife.Will.Dtos;

public record WillReviewResponse
{
    public string Id { get; set; } = null!;
    public string? ReviewerId { get; set; }
    public WillReviewStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public string? Comments { get; set; }
    public WillVersionResponse WillVersion { get; set; } = null!;

    public static WillReviewResponse From(WillReview willReview, WillVersionResponse willVersion)
    {
        return new()
        {
            Id = willReview.Id,
            ReviewerId = willReview.ReviewerId,
            Status = willReview.Status,
            ReviewedAt = willReview.ReviewedAt,
            CreatedAt = willReview.CreatedAt,
            Comments = willReview.Comments,
            WillVersion = willVersion,
        };
    }
}
