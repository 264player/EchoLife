namespace EchoLife.Life.Dtos;

public record LifeHistoryResponse(
    string Id,
    string UserId,
    string Title,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
