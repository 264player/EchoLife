using EchoLife.Life.Models;

namespace EchoLife.Life.Data;

public class SqlLiteLifePointRepository : ILifePointRepository
{
    public Task<LifePoint?> CreateAsync(LifePoint entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<LifePoint?> ReadAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<LifePoint>> ReadAsync(
        Func<LifePoint, bool> express,
        string? startId,
        int count
    )
    {
        throw new NotImplementedException();
    }

    public Task<LifePoint?> UpdateAsync(LifePoint entity)
    {
        throw new NotImplementedException();
    }
}
