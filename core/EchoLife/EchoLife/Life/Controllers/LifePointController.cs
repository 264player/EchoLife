using EchoLife.Common.Exceptions;
using EchoLife.Life.Dtos;
using EchoLife.Life.Services;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.Life.Controllers;

[Route("api")]
[ApiController]
[ExceptionHandling]
public class LifePointController(ILifePointService _lifePointService) : ControllerBase
{
    [HttpPost("life/points")]
    public async Task<IActionResult> Post([FromBody] LifePointRequest lifePointRequest)
    {
        await _lifePointService.CreateLifePointAsync(User, lifePointRequest);
        return Created();
    }

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

    [HttpGet("life/points/{pointId}")]
    public async Task<IActionResult> Get([FromRoute] string pointId)
    {
        return Ok(await _lifePointService.GetLifePointAsync(User, pointId));
    }

    [HttpPut("life/points/{pointId}")]
    public async Task<IActionResult> Put(
        [FromRoute] string pointId,
        [FromBody] LifePointRequest lifePointRequest
    )
    {
        await _lifePointService.UpdateLifePointAsync(User, pointId, lifePointRequest);
        return NoContent();
    }

    [HttpDelete("life/points/{pointId}")]
    public async Task<IActionResult> Delete([FromRoute] string pointId)
    {
        await _lifePointService.DeleteLifePointAsync(User, pointId);
        return NoContent();
    }
}
