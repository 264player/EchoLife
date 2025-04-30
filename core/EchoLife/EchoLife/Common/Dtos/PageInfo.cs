namespace EchoLife.Common.Dtos;

public record PageInfo
{
    public int Count { get; init; } = 30;
    public string? CursorId { get; init; } = null;
}
