using EchoLife.Common.CRUD;

namespace EchoLife.Life.Models;

public class LifePoint : IEntity
{
    public string Id { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string Content { get; set; } = null!;
    public int Visibility { get; set; }
}
