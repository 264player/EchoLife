using System.Linq.Expressions;
using EchoLife.Will.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Will.Data
{
    public class SqlLiteOfficiousWillRepository(WillDbContext _dbContext) : IOfficiousWillRepository
    {
        private DbSet<OfficiousWill> OfficiousWills => _dbContext.Wills;

        public async Task<OfficiousWill?> CreateAsync(OfficiousWill entity)
        {
            await OfficiousWills.AddAsync(entity);
            return await _dbContext.SaveChangesAsync() > 0 ? entity : null;
        }

        public async Task<OfficiousWill?> ReadAsync(string id)
        {
            return await OfficiousWills.Where(w => w.Id == id).SingleOrDefaultAsync();
        }

        public async Task<List<OfficiousWill>> ReadAsync(
            string testaorId,
            string? cursorId,
            int count
        )
        {
            return await OfficiousWills
                .Where(w =>
                    (w.TestaorId == testaorId) && (cursorId == null || w.Id.CompareTo(cursorId) < 0)
                )
                .OrderByDescending(w => w.Id)
                .Take(count)
                .ToListAsync();
        }

        public async Task<OfficiousWill?> ReadByUserIdAsync(string userId, string willId)
        {
            return await OfficiousWills
                .Where(w => w.Id == willId && w.TestaorId == userId)
                .SingleOrDefaultAsync();
        }

        public async Task<OfficiousWill?> UpdateAsync(OfficiousWill entity)
        {
            var result = await OfficiousWills
                .Where(u => u.Id == entity.Id)
                .ExecuteUpdateAsync(will => will.SetProperty(w => w.VersionId, entity.VersionId));
            return result > 0 ? entity : null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return (await OfficiousWills.Where(w => w.Id == id).ExecuteDeleteAsync()) > 0;
        }

        public async Task<List<OfficiousWill>> ReadAsync(
            Expression<Func<OfficiousWill, bool>> express,
            int count
        )
        {
            return await OfficiousWills
                .Where(express)
                .OrderByDescending(w => w.Id)
                .Take(count)
                .ToListAsync();
        }
    }
}
