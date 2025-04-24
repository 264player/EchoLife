using System.Linq.Expressions;
using EchoLife.Will.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Will.Data
{
    public class SqlLiteWillVersionRepository(WillDbContext _dbContext) : IWillVersionRepository
    {
        private DbSet<WillVersion> Versions => _dbContext.WillVersions;

        public async Task<WillVersion?> CreateAsync(WillVersion entity)
        {
            await Versions.AddAsync(entity);
            return await _dbContext.SaveChangesAsync() > 0 ? entity : null;
        }

        public async Task<WillVersion?> ReadAsync(string id)
        {
            return await Versions.Where(v => v.Id == id).SingleOrDefaultAsync();
        }

        public async Task<List<WillVersion>> ReadAsync(
            Expression<Func<WillVersion, bool>> express,
            int count
        )
        {
            return await Versions.Where(express).Take(count).ToListAsync();
        }

        public async Task<List<WillVersion>> ReadAsync(string willId, int count, string? cursorId)
        {
            return await Versions
                .Where(x =>
                    (x.WillId == willId) && (cursorId == null || x.Id.CompareTo(cursorId) < 0)
                )
                .OrderByDescending(x => x.Id)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<WillVersion>> ReadAsync(IEnumerable<string> versionIds)
        {
            return await Versions
                .Where(v => versionIds.Contains(v.Id))
                .OrderByDescending(v => v.Id)
                .ToListAsync();
        }

        public async Task<WillVersion?> UpdateAsync(WillVersion entity)
        {
            var result = await Versions
                .Where(u => u.Id == entity.Id)
                .ExecuteUpdateAsync(version => version.SetProperty(v => v.Content, entity.Content));
            return result > 0 ? entity : null;
        }

        public async Task<bool> DeleteAllVersionsByWillIdAsync(string willId)
        {
            return (await Versions.Where(v => v.WillId == willId).ExecuteDeleteAsync()) > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await (Versions.Where(v => v.Id == id).ExecuteDeleteAsync()) > 0;
        }
    }
}
