using System.Security.Claims;
using EchoLife.Life.Dtos;
using EchoLife.Life.Models;

namespace EchoLife.Life.Services;

public interface ILifePointService
{
    Task CreateLifePointAsync(ClaimsPrincipal user, LifePointRequest lifePoint);
    Task<LifePointResponse?> GetLifePointAsync(ClaimsPrincipal me, string pointId);
    Task<List<LifePoint>> GetLifePointByUserIdAsync(
        ClaimsPrincipal me,
        string userId,
        QueryLifePointsRequest queryLifePointsRequest
    );
    Task JoinLifePointAsync(string userId, string pointId);
    Task LeaveLifePointAsync(string userId, string pointId);
    Task UpdateLifePointAsync(ClaimsPrincipal me, string pointId, LifePointRequest lifePoint);
    Task DeleteLifePointAsync(ClaimsPrincipal me, string pointId);
}
