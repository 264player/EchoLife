using EchoLife.Common.CRUD;

namespace EchoLife.Life.Models;

public class LifePointUriMap : IEntity
{
    public string Id { get; set; } = null!;
    public string Uri { get; set; } = null!;
    public string LifePointId { get; set; } = null!;
}
