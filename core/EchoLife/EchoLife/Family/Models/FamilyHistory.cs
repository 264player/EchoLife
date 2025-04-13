using EchoLife.Common.CRUD;

namespace EchoLife.Family.Models;

public class FamilyHistory : IEntity
{
    public string Id { get; set; } = null!;
    public string FamilyId { get; set; } = null!;
    public string Title { get; set; } = null!;

    /// <summary>
    /// It will update to familyId.
    /// </summary>
    public string UserId { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
