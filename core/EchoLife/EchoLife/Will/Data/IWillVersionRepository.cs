using EchoLife.Common.CRUD;
using EchoLife.Will.Models;

namespace EchoLife.Will.Data
{
    public interface IWillVersionRepository : IEntityRepository<WillVersion>
    {
        Task<bool> DeleteAllVersionsByWillIdAsync(string willId);
    }
}
