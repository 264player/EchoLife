﻿using System.Linq.Expressions;
using EchoLife.Life.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Life.Data;

public class SqlLiteLifePointUserMapRepository(LifeDbContext _lifeDbContext)
    : ILifePointUserMapRepository
{
    private DbSet<PointUserMap> PointUserMaps => _lifeDbContext.PointUserMaps;

    public async Task<PointUserMap?> CreateAsync(PointUserMap entity)
    {
        await PointUserMaps.AddAsync(entity);
        return await _lifeDbContext.SaveChangesAsync() > 0 ? entity : null;
    }

    public async Task<PointUserMap?> ReadAsync(string id)
    {
        return await PointUserMaps.Where(s => s.Id == id).SingleOrDefaultAsync();
    }

    public async Task<List<PointUserMap>> ReadAsync(
        Expression<Func<PointUserMap, bool>> express,
        int count
    )
    {
        return await PointUserMaps.Where(express).Take(count).ToListAsync();
    }

    public async Task<PointUserMap?> UpdateAsync(PointUserMap entity)
    {
        var result = await PointUserMaps
            .Where(u => u.Id == entity.Id)
            .ExecuteUpdateAsync(sub => sub);
        return result > 0 ? entity : null;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await (PointUserMaps.Where(s => s.Id == id).ExecuteDeleteAsync()) > 0;
    }

    public async Task<bool> DeleteAsync(string userId, string pointId)
    {
        return await (
                PointUserMaps
                    .Where(p => p.UserId == userId && p.PointId == pointId)
                    .ExecuteDeleteAsync()
            ) > 0;
    }
}
