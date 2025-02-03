namespace EchoLife.Common.CRUD
{
    /// <summary>
    /// Repository specification for single entity operations, providing basic CRUD (Create, Read, Update, Delete) functionality.
    /// This interface is designed to be used with entities that implement the <see cref="IEntity"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of the entity model, which must implement the <see cref="IEntity"/> interface.</typeparam>
    public interface IEntityRepository<T>
        where T : class, IEntity
    {
        /// <summary>
        /// Creates a new entity in the repository.
        /// This method inserts the provided entity into the underlying data store and returns the created entity,
        /// including any modifications made during the creation process (e.g., auto-generated keys).
        /// </summary>
        /// <param name="entity">The entity to be created in the repository.</param>
        /// <returns>
        /// A <see cref="Task{T}"/> that represents the asynchronous operation.
        /// The result contains the created entity, which may include any auto-generated fields such as IDs.
        /// </returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Reads an entity from the repository based on the provided identifier.
        /// This method retrieves the entity corresponding to the given ID, or returns <c>null</c> if no matching entity is found.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be retrieved.</param>
        /// <returns>
        /// A <see cref="Task{T}"/> that represents the asynchronous operation.
        /// The result contains the entity if found; otherwise, <c>null</c> if no entity with the specified ID exists.
        /// </returns>
        Task<T?> ReadAsync(string id);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// This method updates the specified entity and persists the changes to the data store.
        /// If the entity does not exist, the method may return <c>null</c> or throw an exception, depending on the implementation.
        /// </summary>
        /// <param name="entity">The entity to be updated with new values.</param>
        /// <returns>
        /// A <see cref="Task{T}"/> that represents the asynchronous operation.
        /// The result contains the updated entity if the operation was successful, or <c>null</c> if the update failed.
        /// </returns>
        Task<T?> UpdateAsync(T entity);

        /// <summary>
        /// Deletes an entity from the repository based on the provided identifier.
        /// This method removes the entity with the given ID from the data store.
        /// It returns <c>true</c> if the deletion was successful, and <c>false</c> if no entity was found with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to be deleted.</param>
        /// <returns>
        /// A <see cref="Task{Boolean}"/> that represents the asynchronous operation.
        /// The result is <c>true</c> if the entity was deleted successfully, or <c>false</c> if no entity with the given ID was found.
        /// </returns>
        Task<bool> DeleteAsync(string id);
    }
}
