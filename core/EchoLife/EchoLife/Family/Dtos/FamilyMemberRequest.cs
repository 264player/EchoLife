namespace EchoLife.Family.Dtos;

public record FamilyMemberRequest(
    string FamilyId,
    string DisplayName,
    string Gender,
    string? FatherId,
    string? MotherId,
    string? SpouseId,
    DateTime? BirthDate,
    DateTime? DeathDate,
    int Generation,
    int PowerLevel
);
