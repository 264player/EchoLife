using EchoLife.Life.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Life.Data;

public class SqlLiteLifePointUriMapRepository(LifeDbContext _lifeDbContext)
    : ILifePointUriMapRepository
{
    private DbSet<LifePointUriMap> LifePointUriMaps => _lifeDbContext.LifePointUriMaps;

    public async Task<LifePointUriMap?> CreateAsync(LifePointUriMap entity)
    {
        await LifePointUriMaps.AddAsync(entity);
        return await _lifeDbContext.SaveChangesAsync() > 0 ? entity : null;
    }

    public async Task<LifePointUriMap?> ReadAsync(string id)
    {
        return await LifePointUriMaps.Where(s => s.Id == id).SingleOrDefaultAsync();
    }

    public async Task<List<LifePointUriMap>> ReadAsync(
        Func<LifePointUriMap, bool> express,
        string? startId,
        int count
    )
    {
        return await LifePointUriMaps
            .Where(s => express(s) && (startId == null || s.Id.CompareTo(startId) > 0))
            .Take(count)
            .ToListAsync();
    }

    public async Task<LifePointUriMap?> UpdateAsync(LifePointUriMap entity)
    {
        var result = await LifePointUriMaps
            .Where(u => u.Id == entity.Id)
            .ExecuteUpdateAsync(sub => sub.SetProperty(s => s.Uri, entity.Uri));
        return result > 0 ? entity : null;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await (LifePointUriMaps.Where(s => s.Id == id).ExecuteDeleteAsync()) > 0;
    }
}
