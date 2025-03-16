using EchoLife.Life.Models;

namespace EchoLife.Life.Services;

public interface ILifePointService
{
    Task CreateLifePointAsync(LifePoint lifePoint);
    Task GetLifePointAsync(string pointId);
    Task<List<LifePoint>> GetLifePointByUserIdAsync(
        string userId,
        bool isMe,
        string? startId,
        int count
    );
    Task JoinLifePointAsync(string userId, string pointId);
    Task LeaveLifePointAsync(string userId, string pointId);
    Task UpdateLifePointAsync(LifePoint lifePoint);
    Task DeleteLifePointAsync(string userId, string pointId);
}
