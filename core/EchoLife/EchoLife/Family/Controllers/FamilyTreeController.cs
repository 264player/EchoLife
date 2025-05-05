using EchoLife.Common.Dtos;
using EchoLife.Common.Exceptions;
using EchoLife.Family.Dtos;
using EchoLife.Family.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.Family.Controllers;

[Authorize]
[Route("/api")]
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
    public async Task<IActionResult> GetMyFamilies([FromQuery] PageInfo pageInfo)
    {
        return Ok(
            await _familyTreeService.GetFamilyTreeAsync(User, pageInfo.Count, pageInfo.CursorId)
        );
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
    public async Task<IActionResult> DeleteFamily([FromRoute] string familyId)
    {
        await _familyTreeService.DeleteFamilyTreeAsync(User, familyId);
        return Ok();
    }

    [HttpPost("families/members")]
    public async Task<IActionResult> PostMember([FromBody] FamilyMemberRequest familyMemberRequest)
    {
        return Ok(await _familyTreeService.CreateFamilyMemberAsync(User, familyMemberRequest));
    }

    [HttpGet("families/{familyId}/members")]
    public async Task<IActionResult> GetMembers([FromRoute] string familyId)
    {
        return Ok(await _familyTreeService.GetFamilyMembersAsync(familyId));
    }

    [HttpGet("families/members/{memberId}")]
    public async Task<IActionResult> GetMember([FromRoute] string memberId)
    {
        return Ok(await _familyTreeService.GetFamilyMemberAsync(memberId));
    }

    [HttpPut("families/members")]
    public async Task<IActionResult> UpdateMember(
        [FromBody] FamilyMemberRequest familyMemberRequest
    )
    {
        return Ok(await _familyTreeService.UpdateFamilyMemberAsync(User, familyMemberRequest));
    }

    [HttpDelete("families/members/{memberId}")]
    public async Task<IActionResult> DeleteMember(string memberId)
    {
        await _familyTreeService.DeleteFamilyMemberAsync(User, memberId);
        return NoContent();
    }
}
