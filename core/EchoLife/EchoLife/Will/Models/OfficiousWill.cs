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

        public string Name { get; set; } = null!;

        /// <summary>
        /// The current version`s Id.
        /// </summary>
        public string VersionId { get; set; } = null!;
        public WillType WillType { get; set; }
    }
}
