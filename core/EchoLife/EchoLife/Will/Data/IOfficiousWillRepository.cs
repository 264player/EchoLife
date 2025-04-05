using EchoLife.Common.CRUD;
using EchoLife.Will.Models;

namespace EchoLife.Will.Data;

public interface IOfficiousWillRepository : IEntityRepository<OfficiousWill>
{
    Task<OfficiousWill?> ReadByUserIdAsync(string userId, string willId);
}
