using EchoLife.Common.CRUD;

namespace EchoLife.User.Data
{
    public class UserUnitOfWork : IUnitOfWork
    {
        private UserDbContext _dbContext;

        public UserUnitOfWork(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
