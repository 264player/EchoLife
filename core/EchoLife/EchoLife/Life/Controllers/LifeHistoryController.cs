using EchoLife.Common.Exceptions;
using EchoLife.Life.Dtos;
using EchoLife.Life.Services;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.Life.Controllers;

[Route("api")]
[ApiController]
[ExceptionHandling]
public class LifeHistoryController(ILifeHistoryService _lifeHistoryService) : ControllerBase
{
    #region History
    [HttpPost("life/history")]
    public async Task<IActionResult> Post([FromBody] LifeHistoryRequest lifeHistoryRequest)
    {
        return Ok(await _lifeHistoryService.CreateLifeHistoryAsync(User, lifeHistoryRequest));
    }

    [HttpGet("life/history")]
    public async Task<IActionResult> Get(
        [FromQuery] QueryLifeHistoryRequest queryLifeHistoryRequest
    )
    {
        return Ok(await _lifeHistoryService.GetMyLifeHistoryAsync(User, queryLifeHistoryRequest));
    }

    [HttpGet("life/history/{historyId}")]
    public async Task<IActionResult> Get([FromRoute] string historyId)
    {
        return Ok(await _lifeHistoryService.GetLifeHistoryAsync(User, historyId));
    }

    [HttpPut("life/history/{historyId}")]
    public async Task<IActionResult> Put(
        [FromRoute] string historyId,
        [FromBody] LifeHistoryRequest lifeHistoryRequest
    )
    {
        await _lifeHistoryService.UpdateLifeHistoryAsync(User, historyId, lifeHistoryRequest);
        return NoContent();
    }

    [HttpDelete("life/history/{historyId}")]
    public async Task<IActionResult> Delete([FromRoute] string historyId)
    {
        await _lifeHistoryService.DeleteLifeHistoryAsync(User, historyId);
        return NoContent();
    }

    #endregion

    #region History Section

    [HttpPost("life/history/subsection")]
    public async Task<IActionResult> PostSection(
        [FromBody] LifeSubSectionRequest lifeSubSectionRequest
    )
    {
        return Ok(await _lifeHistoryService.CreateLifeSubSectionAsync(User, lifeSubSectionRequest));
    }

    [HttpGet("life/history/{historyId}/subsections")]
    public async Task<IActionResult> GetSection(
        [FromRoute] string historyId,
        [FromQuery] QueryLifeSubSectionRequest queryLifeSubSectionRequest
    )
    {
        return Ok(
            await _lifeHistoryService.GetLifeSubSectionAsync(
                User,
                historyId,
                queryLifeSubSectionRequest
            )
        );
    }

    [HttpGet("life/history/subsections/{sectionId}")]
    public async Task<IActionResult> GetSection([FromRoute] string sectionId)
    {
        return Ok(await _lifeHistoryService.GetLifeSubSectionAsync(User, sectionId));
    }

    [HttpPut("life/history/subsections/{sectionId}")]
    public async Task<IActionResult> PutSection(
        [FromRoute] string sectionId,
        [FromBody] LifeSubSectionRequest lifeSubSectionRequest
    )
    {
        await _lifeHistoryService.UpdateLifeSubSectionAsync(User, sectionId, lifeSubSectionRequest);
        return NoContent();
    }

    [HttpDelete("life/history/subsections/{sectionId}")]
    public async Task<IActionResult> DeleteSection([FromRoute] string sectionId)
    {
        await _lifeHistoryService.DeleteLifeSubSectionAsync(User, sectionId);
        return NoContent();
    }
    #endregion
}
