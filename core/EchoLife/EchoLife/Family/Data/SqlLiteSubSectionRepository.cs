using EchoLife.Family.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Family.Data;

public class SqlLiteSubSectionRepository(FamilyDbContext _familyDbContext) : ISubSectionRepository
{
    private DbSet<SubSection> SubSections => _familyDbContext.SubSections;

    public async Task<SubSection?> CreateAsync(SubSection entity)
    {
        await SubSections.AddAsync(entity);
        var result = await _familyDbContext.SaveChangesAsync();
        return result > 0 ? entity : null;
    }

    public async Task<SubSection?> ReadAsync(string id)
    {
        return await SubSections.Where(s => s.Id == id).SingleOrDefaultAsync();
    }

    public async Task<List<SubSection>> ReadAsync(
        Func<SubSection, bool> express,
        string? startId,
        int count
    )
    {
        return await SubSections
            .Where(s => express(s) && (startId == null || s.Id.CompareTo(startId) > 0))
            .Take(count)
            .ToListAsync();
    }

    public async Task<SubSection?> UpdateAsync(SubSection entity)
    {
        var result = await SubSections
            .Where(s => s.Id == entity.Id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(sc => sc.Title, entity.Title)
                    .SetProperty(sc => sc.Content, entity.Content)
                    .SetProperty(sc => sc.UpdatedAt, entity.UpdatedAt)
            );
        return result > 0 ? entity : null;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await (SubSections.Where(s => s.Id == id).ExecuteDeleteAsync()) > 0;
    }
}
