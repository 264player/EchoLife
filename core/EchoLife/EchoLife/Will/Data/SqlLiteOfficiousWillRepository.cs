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

        public async Task<OfficiousWill?> UpdateAsync(OfficiousWill entity)
        {
            var result = await OfficiousWills
                .Where(u => u.Id == entity.Id)
                .ExecuteUpdateAsync(will => will.SetProperty(w => w.ContentId, entity.ContentId));
            return result > 0 ? entity : null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return (await OfficiousWills.Where(w => w.Id == id).ExecuteDeleteAsync()) > 0;
        }

        public async Task<List<OfficiousWill>> ReadAsync(
            Func<OfficiousWill, bool> express,
            string? startId,
            int count
        )
        {
            return await OfficiousWills
                .Where(w => express(w) && (startId == null || w.Id.CompareTo(startId) > 0))
                .Take(count)
                .ToListAsync();
        }
    }
}
