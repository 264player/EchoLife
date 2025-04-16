using EchoLife.Family.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Family.Data;

public class SqlLiteFamilyHistoryRepository(FamilyDbContext _familyDbContext)
    : IFamilyHistoryRepository
{
    private DbSet<FamilyHistory> FamilyHistories => _familyDbContext.FamilyHistories;

    public async Task<FamilyHistory?> CreateAsync(FamilyHistory entity)
    {
        await FamilyHistories.AddAsync(entity);
        var result = await _familyDbContext.SaveChangesAsync();
        return result > 0 ? entity : null;
    }

    public async Task<FamilyHistory?> ReadAsync(string id)
    {
        return await FamilyHistories.Where(h => h.Id == id).SingleOrDefaultAsync();
    }

    public async Task<List<FamilyHistory>> ReadAsync(
        Func<FamilyHistory, bool> express,
        string? startId,
        int count
    )
    {
        return await FamilyHistories
            .Where(h => express(h) && (startId == null || h.Id.CompareTo(startId) > 0))
            .Take(count)
            .ToListAsync();
    }

    public async Task<FamilyHistory?> UpdateAsync(FamilyHistory entity)
    {
        var result = await FamilyHistories
            .Where(u => u.Id == entity.Id)
            .ExecuteUpdateAsync(history =>
                history
                    .SetProperty(h => h.UpdatedAt, entity.UpdatedAt)
                    .SetProperty(h => h.Title, entity.Title)
            );
        return result > 0 ? entity : null;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await (FamilyHistories.Where(w => w.Id == id).ExecuteDeleteAsync()) > 0;
    }
}
