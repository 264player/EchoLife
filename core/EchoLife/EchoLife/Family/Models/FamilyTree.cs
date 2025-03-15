using EchoLife.Common.CRUD;

namespace EchoLife.Family.Models;

public class FamilyTree : IEntity
{
    public string Id { get; set; } = null!;
    public string? Name { get; set; }
    public string CreatedUserId { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
