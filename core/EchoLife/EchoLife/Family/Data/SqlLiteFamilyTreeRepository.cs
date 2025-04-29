using System.Linq.Expressions;
using EchoLife.Family.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Family.Data;

public class SqlLiteFamilyTreeRepository(FamilyDbContext _familyDbContext) : IFamilyTreeRepository
{
    private DbSet<FamilyTree> FamilyTrees => _familyDbContext.FamilyTrees;

    public async Task<FamilyTree?> CreateAsync(FamilyTree entity)
    {
        await FamilyTrees.AddAsync(entity);
        var result = await _familyDbContext.SaveChangesAsync();
        return result > 0 ? entity : null;
    }

    public async Task<FamilyTree?> ReadAsync(string id)
    {
        return await FamilyTrees.Where(h => h.Id == id).SingleOrDefaultAsync();
    }

    public async Task<List<FamilyTree>> ReadAsync(IEnumerable<string> ids)
    {
        return await FamilyTrees.Where(f => ids.Contains(f.Id)).ToListAsync();
    }

    public async Task<List<FamilyTree>> ReadAsync(
        Expression<Func<FamilyTree, bool>> express,
        int count
    )
    {
        return await FamilyTrees.Where(express).Take(count).ToListAsync();
    }

    public async Task<FamilyTree?> UpdateAsync(FamilyTree entity)
    {
        var result = await FamilyTrees
            .Where(u => u.Id == entity.Id)
            .ExecuteUpdateAsync(will => will.SetProperty(t => t.Name, entity.Name));
        return result > 0 ? entity : null;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await (FamilyTrees.Where(t => t.Id == id).ExecuteDeleteAsync()) > 0;
    }
}
