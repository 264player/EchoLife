using EchoLife.Common.CRUD;

namespace EchoLife.Will.Models
{
    public class OfficiousWill : IEntity
    {
        public string Id { get; set; } = null!;

        /// <summary>
        /// TestaorId is the ID of the person to whom the will belongs.
        /// </summary>
        public string TestaorId { get; set; } = null!;

        /// <summary>
        /// The current version`s Id.
        /// </summary>
        public string ContentId { get; set; } = null!;
        public string WillType { get; set; } = null!;
    }
}
