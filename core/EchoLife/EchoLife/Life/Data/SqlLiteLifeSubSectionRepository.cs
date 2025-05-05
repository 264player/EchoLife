using System.Linq.Expressions;
using EchoLife.Life.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Life.Data;

public class SqlLiteLifeSubSectionRepository(LifeDbContext _lifeDbContext)
    : ILifeSubSectionRepository
{
    private DbSet<LifeSubSection> LifeSubSections => _lifeDbContext.LifeSubSections;

    public async Task<LifeSubSection?> CreateAsync(LifeSubSection entity)
    {
        await LifeSubSections.AddAsync(entity);
        return await _lifeDbContext.SaveChangesAsync() > 0 ? entity : null;
    }

    public async Task<LifeSubSection?> ReadAsync(string id)
    {
        return await LifeSubSections.Where(s => s.Id == id).SingleOrDefaultAsync();
    }

    public async Task<List<LifeSubSection>> ReadAsync(
        Expression<Func<LifeSubSection, bool>> express,
        int count
    )
    {
        return await LifeSubSections.Where(express).OrderBy(s => s.Id).Take(count).ToListAsync();
    }

    public async Task<List<LifeSubSection>> ReadAllSubSectionsAsync(string historyId)
    {
        return await LifeSubSections
            .Where(s => s.LifeHistoryId == historyId)
            .OrderBy(s => s.Id)
            .ToListAsync();
    }

    public async Task<LifeSubSection?> UpdateAsync(LifeSubSection entity)
    {
        var result = await LifeSubSections
            .Where(u => u.Id == entity.Id)
            .ExecuteUpdateAsync(sub =>
                sub.SetProperty(s => s.Title, entity.Title)
                    .SetProperty(s => s.Content, entity.Content)
                    .SetProperty(s => s.UpdatedAt, entity.UpdatedAt)
            );
        return result > 0 ? entity : null;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await (LifeSubSections.Where(s => s.Id == id).ExecuteDeleteAsync()) > 0;
    }
}
