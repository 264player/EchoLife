using EchoLife.Family.Models;

namespace EchoLife.Family.Dtos;

public record FamilyHistoryResponse(
    string Id,
    string FamilyId,
    string Title,
    DateTime CreatedAt,
    DateTime UpdatedAt
)
{
    public static FamilyHistoryResponse From(FamilyHistory familyHistory)
    {
        return new FamilyHistoryResponse(
            familyHistory.Id,
            familyHistory.FamilyId,
            familyHistory.Title,
            familyHistory.CreatedAt,
            familyHistory.UpdatedAt
        );
    }
}
