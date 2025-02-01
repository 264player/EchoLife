using EchoLife.Common.CRUD;

namespace EchoLife.User.Model
{
    public class BaseUser : IEntity
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string NickName { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
