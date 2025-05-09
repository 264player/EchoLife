using EchoLife.Common.CRUD;
using EchoLife.Common.Dtos;
using EchoLife.Life.Models;

namespace EchoLife.Life.Data;

public interface ILifePointRepository : IEntityRepository<LifePoint>
{
    Task<List<LifePoint>> ReadMyLifePointAsync(string userId, PageInfo pageInfo);
}
