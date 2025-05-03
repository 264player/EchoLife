using EchoLife.Common.Exceptions;
using EchoLife.Life.Dtos;
using EchoLife.Life.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.Life.Controllers;

[Route("api")]
[ApiController]
[ExceptionHandling]
public class LifePointController(ILifePointService _lifePointService) : ControllerBase
{
    [Authorize]
    [HttpPost("life/points")]
    public async Task<IActionResult> Post([FromBody] LifePointRequest lifePointRequest)
    {
        return Ok(await _lifePointService.CreateLifePointAsync(User, lifePointRequest));
    }

    [Authorize]
    [HttpGet("{userId}/life/points")]
    public async Task<IActionResult> Get(
        [FromRoute] string userId,
        [FromQuery] QueryLifePointsRequest queryLifePointsRequest
    )
    {
        return Ok(
            await _lifePointService.GetLifePointByUserIdAsync(User, userId, queryLifePointsRequest)
        );
    }

    [Authorize]
    [HttpGet("life/points/{pointId}")]
    public async Task<IActionResult> Get([FromRoute] string pointId)
    {
        return Ok(await _lifePointService.GetLifePointAsync(User, pointId));
    }

    [Authorize]
    [HttpPut("life/points/{pointId}")]
    public async Task<IActionResult> Put(
        [FromRoute] string pointId,
        [FromBody] LifePointRequest lifePointRequest
    )
    {
        await _lifePointService.UpdateLifePointAsync(User, pointId, lifePointRequest);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("life/points/{pointId}")]
    public async Task<IActionResult> Delete([FromRoute] string pointId)
    {
        await _lifePointService.DeleteLifePointAsync(User, pointId);
        return NoContent();
    }

    [Authorize]
    [HttpPost("life/points/{pointId}/join")]
    public async Task<IActionResult> InviteUserToPoint(
        [FromRoute] string pointId,
        [FromBody] IEnumerable<string> userIdList
    )
    {
        await _lifePointService.JoinLifePointAsync(User, pointId, userIdList);
        return Ok();
    }

    [Authorize]
    [HttpDelete("life/points/{pointId}/leave")]
    public async Task<IActionResult> LeavePoint([FromRoute] string pointId)
    {
        await _lifePointService.LeaveLifePointAsync(User, pointId);
        return Ok();
    }
}
