using EchoLife.Common.CRUD;
using EchoLife.Life.Models;

namespace EchoLife.Life.Data;

public interface ILifePointUserMapRepository : IEntityRepository<PointUserMap>
{
    Task<bool> DeleteAsync(string userId, string pointId);
}
