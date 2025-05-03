using System.Linq.Expressions;
using EchoLife.Life.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Life.Data;

public class SqlLiteLifeHistoryRepository(LifeDbContext _lifeDbContext) : ILifeHitoryRepository
{
    private DbSet<LifeHistory> LifeHistories => _lifeDbContext.LifeHistories;

    public async Task<LifeHistory?> CreateAsync(LifeHistory entity)
    {
        await LifeHistories.AddAsync(entity);
        return await _lifeDbContext.SaveChangesAsync() > 0 ? entity : null;
    }

    public async Task<LifeHistory?> ReadAsync(string id)
    {
        return await LifeHistories.Where(s => s.Id == id).SingleOrDefaultAsync();
    }

    public async Task<List<LifeHistory>> ReadAsync(
        Expression<Func<LifeHistory, bool>> express,
        int count
    )
    {
        return await LifeHistories
            .Where(express)
            .OrderByDescending(h => h.Id)
            .Take(count)
            .ToListAsync();
    }

    public async Task<LifeHistory?> UpdateAsync(LifeHistory entity)
    {
        var result = await LifeHistories
            .Where(u => u.Id == entity.Id)
            .ExecuteUpdateAsync(sub =>
                sub.SetProperty(s => s.Title, entity.Title)
                    .SetProperty(s => s.UpdatedAt, entity.UpdatedAt)
            );
        return result > 0 ? entity : null;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await (LifeHistories.Where(s => s.Id == id).ExecuteDeleteAsync()) > 0;
    }
}
