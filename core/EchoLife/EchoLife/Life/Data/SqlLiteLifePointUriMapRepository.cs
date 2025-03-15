using EchoLife.Life.Models;

namespace EchoLife.Life.Data;

public class SqlLiteLifePointUriMapRepository : ILifePointUriMapRepository
{
    public Task<LifePointUriMap?> CreateAsync(LifePointUriMap entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<LifePointUriMap?> ReadAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<LifePointUriMap>> ReadAsync(
        Func<LifePointUriMap, bool> express,
        string? startId,
        int count
    )
    {
        throw new NotImplementedException();
    }

    public Task<LifePointUriMap?> UpdateAsync(LifePointUriMap entity)
    {
        throw new NotImplementedException();
    }
}
