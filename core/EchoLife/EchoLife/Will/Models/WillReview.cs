using EchoLife.Common.CRUD;

namespace EchoLife.Will.Models;

public class WillReview : IEntity
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string VersionId { get; set; } = null!;
    public string? ReviewerId { get; set; }
    public WillReviewStatus Status { get; set; } = WillReviewStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ReviewedAt { get; set; }
    public string? Comments { get; set; }
}
