using EchoLife.Common.Exceptions;
using EchoLife.Family.Dtos;
using EchoLife.Family.Services;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.Family.Controllers;

[Route("api")]
[ApiController]
[ExceptionHandling]
public class FamilyHistoryController(IFamilyHistoryService _familyHistoryService) : ControllerBase
{
    #region History
    [HttpPost("family/history")]
    public async Task<IActionResult> Post([FromBody] FamilyHistoryRequest familyHistoryRequest)
    {
        await _familyHistoryService.CreateFamilyHistoryAsync(User, familyHistoryRequest);
        return Created();
    }

    [HttpGet("family/history")]
    public async Task<IActionResult> Get(
        [FromQuery] QueryFamilyHistoryRequest queryLifeHistoryRequest
    )
    {
        return Ok(await _familyHistoryService.GetFamilyHistoryAsync(User, queryLifeHistoryRequest));
    }

    [HttpGet("family/history/{historyId}")]
    public async Task<IActionResult> Get([FromRoute] string historyId)
    {
        return Ok(await _familyHistoryService.GetFamilyHistoryAsync(User, historyId));
    }

    [HttpPut("family/history/{historyId}")]
    public async Task<IActionResult> Put(
        [FromRoute] string historyId,
        [FromBody] FamilyHistoryRequest familyHistoryRequest
    )
    {
        await _familyHistoryService.UpdateFamilyHistoryAsync(User, historyId, familyHistoryRequest);
        return NoContent();
    }

    [HttpDelete("family/history/{historyId}")]
    public async Task<IActionResult> Delete([FromRoute] string historyId)
    {
        await _familyHistoryService.DeleteFamilyHistoryAsync(User, historyId);
        return NoContent();
    }

    #endregion

    #region History Section

    [HttpPost("family/history/subsection")]
    public async Task<IActionResult> PostSection(
        [FromBody] FamilySubSectionRequest familySubSectionRequest
    )
    {
        await _familyHistoryService.CreateFamilySubSectionAsync(User, familySubSectionRequest);
        return Created();
    }

    [HttpGet("family/history/{historyId}/subsections")]
    public async Task<IActionResult> GetSection(
        [FromRoute] string historyId,
        [FromQuery] QueryFamilySubSectionRequest queryFamilySubSectionRequest
    )
    {
        return Ok(
            await _familyHistoryService.GetFamilySubSectionAsync(
                User,
                historyId,
                queryFamilySubSectionRequest
            )
        );
    }

    [HttpGet("family/history/subsections/{sectionId}")]
    public async Task<IActionResult> GetSection([FromRoute] string sectionId)
    {
        return Ok(await _familyHistoryService.GetFamilySubSectionAsync(User, sectionId));
    }

    [HttpPut("family/history/subsections/{sectionId}")]
    public async Task<IActionResult> PutSection(
        [FromRoute] string sectionId,
        [FromBody] FamilySubSectionRequest familySubSectionRequest
    )
    {
        await _familyHistoryService.UpdateFamilySubSectionAsync(
            User,
            sectionId,
            familySubSectionRequest
        );
        return NoContent();
    }

    [HttpDelete("family/history/subsections/{sectionId}")]
    public async Task<IActionResult> DeleteSection([FromRoute] string sectionId)
    {
        await _familyHistoryService.DeleteFamilySubSectionAsync(User, sectionId);
        return NoContent();
    }
    #endregion
}
