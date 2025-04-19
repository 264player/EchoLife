using EchoLife.Common.Exceptions;
using EchoLife.Will.Dtos;
using EchoLife.Will.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.Will.Controllers
{
    [Route("api")]
    [ApiController]
    [ExceptionHandling]
    public class WillController(IWillService _willService) : ControllerBase
    {
        #region Will
        [Authorize]
        [HttpPost("wills")]
        public async Task<IActionResult> PostWill([FromBody] WillRequest willRequest)
        {
            var response = await _willService.CreateWillAsync(User, willRequest);
            return Created("", new { willId = response });
        }

        [Authorize]
        [HttpGet("wills")]
        public async Task<IActionResult> GetWillByUserId(
            [FromQuery] QueryWillRequest queryWillRequest
        )
        {
            return Ok(
                await _willService.GetMyWillsAsync(
                    User,
                    queryWillRequest.Count,
                    queryWillRequest.CursorId
                )
            );
        }

        [Authorize]
        [HttpGet("wills/{willId}")]
        public async Task<IActionResult> GetWillByWillId([FromRoute] string willId)
        {
            return Ok(await _willService.GetMyWillAsync(User, willId));
        }

        [Authorize]
        [HttpPut("wills/{willId}")]
        public async Task<IActionResult> PutWill(
            [FromRoute] string willId,
            [FromBody] PutWillRequest putWillRequest
        )
        {
            var result = await _willService.UpdateWillAsync(
                User,
                willId,
                putWillRequest.VersionId,
                putWillRequest.Name
            );
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("wills/{willId}")]
        public async Task<IActionResult> DeleteWill([FromRoute] string willId)
        {
            await _willService.DeleteWillAsync(User, willId);
            return NoContent();
        }

        #endregion

        #region Will Version
        [Authorize]
        [HttpPost("wills/{willId}/versions")]
        public async Task<IActionResult> PostWillVresinos(
            [FromRoute] string willId,
            [FromBody] WillVersionRequest willRequest,
            [FromQuery] bool isDraft = false
        )
        {
            var response = await _willService.CreateWillVersionsAsync(
                User,
                willId,
                willRequest,
                isDraft
            );
            return Created("", response);
        }

        [Authorize]
        [HttpGet("wills/{willId}/versions")]
        public async Task<IActionResult> GetWillVersino(
            [FromRoute] string willId,
            [FromQuery] QueryWillVersionRequest queryWillVersionRequest
        )
        {
            return Ok(
                await _willService.GetMyWillVersionsAsync(
                    User,
                    willId,
                    queryWillVersionRequest.Count,
                    queryWillVersionRequest.CursorId
                )
            );
        }

        [Authorize]
        [HttpGet("wills/{willId}/versions/{versionId}")]
        public Task<IActionResult> GetWillByWillId(
            [FromRoute] string willId,
            [FromRoute] string versionId
        )
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpPut("wills/versions/{versionId}")]
        public async Task<IActionResult> PutWillVersion(
            [FromRoute] string versionId,
            [FromBody] WillVersionRequest willVersionRequest
        )
        {
            var result = await _willService.UpdateWillVersionAsync(
                User,
                versionId,
                willVersionRequest
            );
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("wills/versions/{versionId}")]
        public async Task<IActionResult> DeleteWillVresion([FromRoute] string versionId)
        {
            await _willService.DeleteWillVersionAsync(User, versionId);
            return NoContent();
        }
        #endregion
    }
}
