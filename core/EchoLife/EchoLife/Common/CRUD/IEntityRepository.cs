namespace EchoLife.Common.CRUD
{
    public interface IEntityRepository<T> where T : class, IEntity
    {
        Task<T> CreateAsync(T entity);
        Task<T> ReadAsync(string id);
        Task<T> UpdateAsync(string id, T entity);
        Task<bool> DeleteAsync(string id);
    }
}
