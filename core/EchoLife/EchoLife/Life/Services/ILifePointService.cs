using System.Security.Claims;
using EchoLife.Life.Dtos;
using EchoLife.Life.Models;

namespace EchoLife.Life.Services;

public interface ILifePointService
{
    Task<LifePointResponse> CreateLifePointAsync(ClaimsPrincipal user, LifePointRequest lifePoint);
    Task<LifePointResponse> GetLifePointAsync(ClaimsPrincipal me, string pointId);
    Task<List<LifePoint>> GetLifePointByUserIdAsync(
        ClaimsPrincipal me,
        string userId,
        QueryLifePointsRequest queryLifePointsRequest
    );
    Task<LifePointResponse> UpdateLifePointAsync(
        ClaimsPrincipal me,
        string pointId,
        LifePointRequest lifePoint
    );
    Task DeleteLifePointAsync(ClaimsPrincipal me, string pointId);
    Task JoinLifePointAsync(ClaimsPrincipal me, string pointId, IEnumerable<string> userIdList);
    Task LeaveLifePointAsync(ClaimsPrincipal me, string pointId);
}
