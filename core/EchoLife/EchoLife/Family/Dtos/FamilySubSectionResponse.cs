using EchoLife.Family.Models;

namespace EchoLife.Family.Dtos;

public record FamilySubSectionResponse(
    string Id,
    string Title,
    string Content,
    string? FatherId,
    string FamilyHistoryId,
    int Deep,
    DateTime CreatedAt,
    DateTime UpdatedAt
)
{
    public static FamilySubSectionResponse From(FamilySubSection familySubSection)
    {
        return new FamilySubSectionResponse(
            familySubSection.Id,
            familySubSection.Title,
            familySubSection.Content,
            familySubSection.FatherId,
            familySubSection.FamilyHistoryId,
            familySubSection.Deep,
            familySubSection.CreatedAt,
            familySubSection.UpdatedAt
        );
    }
}
