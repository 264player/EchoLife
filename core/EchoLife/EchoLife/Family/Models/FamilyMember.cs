using EchoLife.Common.CRUD;

namespace EchoLife.Family.Models;

public class FamilyMember : IEntity
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string FamilyId { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string? FatherId { get; set; }
    public string? MotherId { get; set; }
    public string? SpouseId { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public int Generation { get; set; }
    public int PowerLevel { get; set; } = 0;
}
