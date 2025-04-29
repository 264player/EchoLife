using EchoLife.Common.Exceptions;
using EchoLife.Family.Dtos;
using EchoLife.Family.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.Family.Controllers;

[Authorize]
[Route("api")]
[ApiController]
[ExceptionHandling]
public class FamilyTreeController(IFamilyService _familyTreeService) : ControllerBase
{
    [HttpPost("families")]
    public async Task<IActionResult> PostFamily([FromBody] FamilyTreeRequest familyTreeRequest)
    {
        return Ok(await _familyTreeService.CreateFamilyTreeAsync(User, familyTreeRequest));
    }

    [HttpGet("families/{familyId}")]
    public async Task<IActionResult> GetMyFamily([FromRoute] string familyId)
    {
        return Ok(await _familyTreeService.GetFamilyTreeAsync(User, familyId));
    }

    [HttpGet("families")]
    public async Task<IActionResult> GetMyFamilies([FromQuery] int count, string? cursorId)
    {
        return Ok(await _familyTreeService.GetFamilyTreeAsync(User, count, cursorId));
    }

    [HttpPut("families/{familyId}")]
    public async Task<IActionResult> UpdateFamily(
        [FromRoute] string familyId,
        [FromBody] FamilyTreeRequest familyTreeRequest
    )
    {
        return Ok(
            await _familyTreeService.UpdateFamilyTreeAsync(User, familyId, familyTreeRequest)
        );
    }

    [HttpDelete("families/{familyId}")]
    public async Task<IActionResult> DeleteFamily(string familyId)
    {
        await _familyTreeService.DeleteFamilyTreeAsync(User, familyId);
        return NoContent();
    }
}
