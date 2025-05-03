using EchoLife.Life.Models;

namespace EchoLife.Life.Dtos;

public record LifeHistoryResponse(
    string Id,
    string UserId,
    string Title,
    DateTime CreatedAt,
    DateTime UpdatedAt
)
{
    public static LifeHistoryResponse From(LifeHistory lifeHistory)
    {
        return new(
            lifeHistory.Id,
            lifeHistory.UserId,
            lifeHistory.Title,
            lifeHistory.CreatedAt,
            lifeHistory.UpdatedAt
        );
    }
};
