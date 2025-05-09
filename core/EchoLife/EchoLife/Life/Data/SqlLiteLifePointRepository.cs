using System.Linq.Expressions;
using EchoLife.Common.Dtos;
using EchoLife.Life.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Life.Data;

public class SqlLiteLifePointRepository(LifeDbContext _lifeDbContext) : ILifePointRepository
{
    private DbSet<LifePoint> LifePoints => _lifeDbContext.LifePoints;
    private DbSet<PointUserMap> PointUserMap => _lifeDbContext.PointUserMaps;

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
        Expression<Func<LifePoint, bool>> express,
        int count
    )
    {
        return await LifePoints
            .Where(express)
            .OrderByDescending(l => l.Id)
            .Take(count)
            .ToListAsync();
    }

    public async Task<List<LifePoint>> ReadMyLifePointAsync(string userId, PageInfo pageInfo)
    {
        return await PointUserMap
            .Where(m => m.UserId == userId)
            .Join(
                LifePoints,
                map => map.PointId,
                point => point.Id,
                (map, point) => new { map, point }
            )
            .Where(x => (pageInfo.CursorId == null || x.point.Id.CompareTo(pageInfo.CursorId) < 0))
            .Select(x => x.point)
            .OrderByDescending(x => x.Id)
            .Take(pageInfo.Count)
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
