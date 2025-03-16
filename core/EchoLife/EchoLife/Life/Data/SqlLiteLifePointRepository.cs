using EchoLife.Life.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Life.Data;

public class SqlLiteLifePointRepository(LifeDbContext _lifeDbContext) : ILifePointRepository
{
    private DbSet<LifePoint> LifePoints => _lifeDbContext.LifePoints;

    public async Task<LifePoint?> CreateAsync(LifePoint entity)
    {
        await LifePoints.AddAsync(entity);
        return await _lifeDbContext.SaveChangesAsync() > 0 ? entity : null;
    }

    public async Task<LifePoint?> ReadAsync(string id)
    {
        return await LifePoints.Where(s => s.Id == id).SingleOrDefaultAsync();
    }

    public async Task<List<LifePoint>> ReadAsync(
        Func<LifePoint, bool> express,
        string? startId,
        int count
    )
    {
        return await LifePoints
            .Where(s => express(s) && (startId == null || s.Id.CompareTo(startId) > 0))
            .Take(count)
            .ToListAsync();
    }

    public async Task<LifePoint?> UpdateAsync(LifePoint entity)
    {
        var result = await LifePoints
            .Where(u => u.Id == entity.Id)
            .ExecuteUpdateAsync(sub =>
                sub.SetProperty(s => s.Content, entity.Content)
                    .SetProperty(s => s.Hidden, entity.Hidden)
                    .SetProperty(s => s.UpdatedAt, entity.UpdatedAt)
            );
        return result > 0 ? entity : null;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await (LifePoints.Where(s => s.Id == id).ExecuteDeleteAsync()) > 0;
    }
}
