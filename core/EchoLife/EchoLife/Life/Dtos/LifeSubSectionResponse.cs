using EchoLife.Life.Models;

namespace EchoLife.Life.Dtos;

public record LifeSubSectionResponse(
    string Id,
    string Title,
    string Content,
    string? FatherId,
    string LifeHistoryId,
    int index,
    DateTime CreatedAt,
    DateTime UpdatedAt
)
{
    public static LifeSubSectionResponse From(LifeSubSection lifeSubSection)
    {
        return new LifeSubSectionResponse(
            lifeSubSection.Id,
            lifeSubSection.Title,
            lifeSubSection.Content,
            lifeSubSection.FatherId,
            lifeSubSection.LifeHistoryId,
            lifeSubSection.Deep,
            lifeSubSection.CreatedAt,
            lifeSubSection.UpdatedAt
        );
    }
}
