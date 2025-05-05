using EchoLife.Family.Models;

namespace EchoLife.Family.Dtos;

public record FamilyMemberResponse(
    string Id,
    string UserId,
    string FamilyId,
    string DisplayName,
    Gender Gender,
    string? FatherId,
    string? MotherId,
    string? SpouseId,
    DateTime BirthDate,
    DateTime? DeathDate,
    int Generation,
    int PowerLevel
)
{
    public static FamilyMemberResponse From(FamilyMember familyMember)
    {
        return new(
            familyMember.Id,
            familyMember.UserId,
            familyMember.FamilyId,
            familyMember.DisplayName,
            familyMember.Gender,
            familyMember.FatherId,
            familyMember.MotherId,
            familyMember.SpouseId,
            familyMember.BirthDate,
            familyMember.DeathDate,
            familyMember.Generation,
            familyMember.PowerLevel
        );
    }
}
