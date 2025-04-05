using EchoLife.Common.CRUD;

namespace EchoLife.Will.Models
{
    public class WillVersion : IEntity
    {
        public string Id { get; set; } = null!;
        public string WillId { get; set; } = null!;
        public string WillType { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
