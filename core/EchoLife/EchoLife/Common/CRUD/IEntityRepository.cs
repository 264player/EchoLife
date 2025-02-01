namespace EchoLife.Common.CRUD
{
    public interface IEntityRepository<T>
        where T : class, IEntity
    {
        Task<T> CreateAsync(T entity);
        Task<T?> ReadAsync(string id);
        Task<T?> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string id);
    }
}
