using System.Linq.Expressions;
using EchoLife.User.Model;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.User.Data
{
    public class SqlLiteBaseUserRepository : IBaseUserRepository
    {
        private UserDbContext _dbContext;

        public SqlLiteBaseUserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BaseUser> CreateAsync(BaseUser entity)
        {
            await _dbContext.BaseUsers.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<BaseUser?> ReadAsync(string id)
        {
            return await _dbContext.BaseUsers.FindAsync(id);
        }

        public async Task<BaseUser?> ReadByUsernameAsync(string username)
        {
            return await _dbContext.BaseUsers.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<BaseUser?> UpdateAsync(BaseUser entity)
        {
            var result = await _dbContext
                .BaseUsers.Where(u => u.Id == entity.Id)
                .ExecuteUpdateAsync(u =>
                    u.SetProperty(user => user.Username, entity.Username)
                        .SetProperty(user => user.NickName, entity.NickName)
                );
            return result > 0 ? entity : null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return (await _dbContext.BaseUsers.Where(user => user.Id == id).ExecuteDeleteAsync())
                > 0;
        }

        public Task<List<BaseUser>> ReadAsync(Expression<Func<BaseUser, bool>> express, int count)
        {
            throw new NotImplementedException();
        }
    }
}
