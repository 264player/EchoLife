using EchoLife.Life.Models;

namespace EchoLife.Life.Dtos;

public record LifePointResponse(
    string Id,
    string UserId,
    string Content,
    bool Hidden,
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
            lifePoint.Hidden,
            lifePoint.CreatedAt,
            lifePoint.UpdatedAt
        );
    }
}
