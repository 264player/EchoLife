namespace EchoLife.Common.CRUD
{
    /// <summary>
    /// Entity model class, all entities in this system must implement this interface.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Unique identitifier or entity.
        /// </summary>
        public string Id { get; set; }
    }
}
