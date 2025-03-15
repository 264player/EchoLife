using EchoLife.Common.CRUD;

namespace EchoLife.Life.Models;

public class PointUserMap : IEntity
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string PointId { get; set; } = null!;
}
