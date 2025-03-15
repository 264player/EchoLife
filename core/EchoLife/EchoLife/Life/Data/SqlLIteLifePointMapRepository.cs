using EchoLife.Life.Models;

namespace EchoLife.Life.Data;

public class SqlLIteLifePointMapRepository : ILifePointUserMapRepository
{
    public Task<PointUserMap?> CreateAsync(PointUserMap entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<PointUserMap?> ReadAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PointUserMap>> ReadAsync(
        Func<PointUserMap, bool> express,
        string? startId,
        int count
    )
    {
        throw new NotImplementedException();
    }

    public Task<PointUserMap?> UpdateAsync(PointUserMap entity)
    {
        throw new NotImplementedException();
    }
}
