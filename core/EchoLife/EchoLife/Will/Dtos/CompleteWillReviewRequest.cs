using EchoLife.Will.Models;

namespace EchoLife.Will.Dtos;

public class CompleteWillReviewRequest
{
    public string Comment { get; set; } = null!;
    public WillReviewStatus Status { get; set; }
}
