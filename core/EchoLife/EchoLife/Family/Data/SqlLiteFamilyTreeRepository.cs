﻿using EchoLife.Family.Models;
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

    public async Task<List<FamilyTree>> ReadAsync(
        Func<FamilyTree, bool> express,
        string? startId,
        int count
    )
    {
        return await FamilyTrees
            .Where(t => express(t) && (startId == null || t.Id.CompareTo(startId) > 0))
            .Take(count)
            .ToListAsync();
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
