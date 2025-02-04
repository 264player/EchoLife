using EchoLife.Will.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.Will.Controllers
{
    [Route("api")]
    [ApiController]
    public class WillController(IWillService _willService) : ControllerBase
    {
        [Authorize]
        [HttpPost("wills")]
        public Task<IActionResult> PostWill()
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpPost("wills/versions")]
        public Task<IActionResult> PostWillVresinos()
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpGet("wills")]
        public Task<IActionResult> GetWillByUserId()
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpGet("wills/{willId}")]
        public Task<IActionResult> GetWillByWillId([FromRoute] string willId)
        {
            throw new NotImplementedException();
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
        [HttpPut("wills/{willId}")]
        public Task<IActionResult> PostWill([FromRoute] string willId)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpPut("wills/{willId}/versions/{versionId}")]
        public Task<IActionResult> PostWillVersion(
            [FromRoute] string willId,
            [FromRoute] string versionId,
            [FromBody] string newContent
        )
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpDelete("wills/{willId}")]
        public Task<IActionResult> DeleteWill([FromRoute] string willId)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpDelete("wills/{willId}/versions/{versionId}")]
        public Task<IActionResult> DeleteWillVresion(
            [FromRoute] string willId,
            [FromRoute] string versionId
        )
        {
            throw new NotImplementedException();
        }
    }
}
