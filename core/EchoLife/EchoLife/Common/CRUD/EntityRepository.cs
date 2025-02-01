namespace EchoLife.Common.CRUD
{
    public class EntityRepository<T> : IEntityRepository<T>
        where T : class, IEntity
    {
        public Task<T> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T?> ReadAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T?> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
