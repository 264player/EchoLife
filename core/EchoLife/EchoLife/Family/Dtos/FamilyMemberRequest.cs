using EchoLife.Family.Models;

namespace EchoLife.Family.Dtos;

public record FamilyMemberRequest(
    string FamilyId,
    string DisplayName,
    Gender Gender,
    string? FatherId,
    string? MotherId,
    string? SpouseId,
    DateTime? BirthDate,
    DateTime? DeathDate,
    int Generation,
    int PowerLevel
);
