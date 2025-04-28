namespace EchoLife.Family.Dtos;

public record FamilySubSectionRequest(
    string Title,
    string Content,
    string FamilyHistoryId,
    string? FatherId,
    int Index
);
