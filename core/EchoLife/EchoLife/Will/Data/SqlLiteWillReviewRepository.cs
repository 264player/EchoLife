using System.Linq.Expressions;
using EchoLife.Will.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Will.Data;

public class SqlLiteWillReviewRepository(WillDbContext _dbContext) : IWillReviewRepository
{
    private DbSet<WillReview> WillReviews => _dbContext.WillReviews;

    public async Task<WillReview?> CreateAsync(WillReview entity)
    {
        await WillReviews.AddAsync(entity);
        return await _dbContext.SaveChangesAsync() > 0 ? entity : null;
    }

    public async Task<WillReview?> ReadAsync(string id)
    {
        return await WillReviews.Where(w => w.Id == id).SingleOrDefaultAsync();
    }

    public async Task<List<WillReview>> ReadAsync(
        Expression<Func<WillReview, bool>> express,
        int count
    )
    {
        return await WillReviews
            .Where(express)
            .OrderByDescending(w => w.Id)
            .Take(count)
            .ToListAsync();
    }

    public async Task<WillReview?> UpdateAsync(WillReview entity)
    {
        var result = await WillReviews.Where(u => u.Id == entity.Id).ExecuteUpdateAsync(w => w);
        return result > 0 ? entity : null;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return (await WillReviews.Where(w => w.Id == id).ExecuteDeleteAsync()) > 0;
    }
}
