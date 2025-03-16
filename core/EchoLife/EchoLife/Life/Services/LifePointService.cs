using EchoLife.Common.Exceptions;
using EchoLife.Life.Data;
using EchoLife.Life.Models;

namespace EchoLife.Life.Services;

public class LifePointService(
    ILifePointRepository _lifePointRepository,
    ILifePointUserMapRepository _lifePointUserMapRepository
) : ILifePointService
{
    public async Task CreateLifePointAsync(LifePoint lifePoint)
    {
        await _lifePointRepository.CreateAsync(lifePoint);
        await _lifePointUserMapRepository.CreateAsync(
            new PointUserMap
            {
                Id = Guid.NewGuid().ToString(),
                UserId = lifePoint.UserId,
                PointId = lifePoint.Id,
            }
        );
    }

    public async Task GetLifePointAsync(string pointId)
    {
        await _lifePointRepository.ReadAsync(pointId);
    }

    public async Task<List<LifePoint>> GetLifePointByUserIdAsync(
        string userId,
        bool isMe,
        string? startId,
        int count
    )
    {
        return await _lifePointRepository.ReadAsync(
            p => (p.UserId == userId) && (isMe || !p.Hidden),
            startId,
            count
        );
    }

    public async Task JoinLifePointAsync(string userId, string pointId)
    {
        await _lifePointUserMapRepository.CreateAsync(
            new PointUserMap
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                PointId = pointId,
            }
        );
    }

    public async Task LeaveLifePointAsync(string userId, string pointId)
    {
        await _lifePointUserMapRepository.DeleteAsync(userId, pointId);
    }

    public async Task UpdateLifePointAsync(LifePoint lifePoint)
    {
        await _lifePointRepository.UpdateAsync(lifePoint);
    }

    public async Task DeleteLifePointAsync(string userId, string pointId)
    {
        var point = await _lifePointRepository.ReadAsync(pointId);
        if (point != null && point.UserId != userId)
        {
            throw new ResourceNotFoundException();
        }
        await _lifePointRepository.DeleteAsync(pointId);
    }
}
