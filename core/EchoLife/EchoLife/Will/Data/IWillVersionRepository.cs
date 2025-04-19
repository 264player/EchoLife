using EchoLife.Common.CRUD;
using EchoLife.Will.Models;

namespace EchoLife.Will.Data
{
    public interface IWillVersionRepository : IEntityRepository<WillVersion>
    {
        Task<List<WillVersion>> ReadAsync(string willId, int count, string? cursorId);
        Task<bool> DeleteAllVersionsByWillIdAsync(string willId);
    }
}
