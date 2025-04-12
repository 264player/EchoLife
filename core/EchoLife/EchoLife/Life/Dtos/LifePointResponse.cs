using EchoLife.Life.Models;

namespace EchoLife.Life.Dtos;

public record LifePointResponse(
    string Id,
    string UserId,
    string Content,
    DateTime CreatetdAt,
    DateTime UpdatedAt
)
{
    public static LifePointResponse From(LifePoint lifePoint)
    {
        return new LifePointResponse(
            lifePoint.Id,
            lifePoint.UserId,
            lifePoint.Content,
            lifePoint.CreatedAt,
            lifePoint.UpdatedAt
        );
    }
}
