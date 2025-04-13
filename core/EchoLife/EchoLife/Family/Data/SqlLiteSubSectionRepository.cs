using EchoLife.Family.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Family.Data;

public class SqlLiteSubSectionRepository(FamilyDbContext _familyDbContext)
    : IFamilySubSectionRepository
{
    private DbSet<FamilySubSection> SubSections => _familyDbContext.SubSections;

    public async Task<FamilySubSection?> CreateAsync(FamilySubSection entity)
    {
        await SubSections.AddAsync(entity);
        var result = await _familyDbContext.SaveChangesAsync();
        return result > 0 ? entity : null;
    }

    public async Task<FamilySubSection?> ReadAsync(string id)
    {
        return await SubSections.Where(s => s.Id == id).SingleOrDefaultAsync();
    }

    public async Task<List<FamilySubSection>> ReadAsync(
        Func<FamilySubSection, bool> express,
        string? startId,
        int count
    )
    {
        return await SubSections
            .Where(s => express(s) && (startId == null || s.Id.CompareTo(startId) > 0))
            .Take(count)
            .ToListAsync();
    }

    public async Task<FamilySubSection?> UpdateAsync(FamilySubSection entity)
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
