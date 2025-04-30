using EchoLife.Common.Exceptions;
using EchoLife.Will.Models;
using EchoLife.Will.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.Will.Controllers;

[Authorize]
[Route("api")]
[ApiController]
[ExceptionHandling]
public class WillReviewController(IWillReviewService _willReviewService) : ControllerBase
{
    [HttpPost("wills/review/ai")]
    public async Task<IActionResult> RequestAIReview([FromBody] string versionId)
    {
        return Ok(await _willReviewService.RequestAIReviewAsync(User, versionId));
    }

    [HttpPost("wills/review/human")]
    public async Task<IActionResult> RequestHumanReview([FromBody] string versionId)
    {
        return Ok(await _willReviewService.RequestAIReviewAsync(User, versionId));
    }

    [HttpPost("wills/review/{reviewId}/human/process")]
    public async Task<IActionResult> InprogressHumanReview([FromRoute] string reviewId)
    {
        await _willReviewService.ProcessReview(User, reviewId);
        return Ok();
    }

    [HttpPost("wills/review/{reviewId}/human/complete")]
    public async Task<IActionResult> CompleteHumanReview(
        [FromRoute] string reviewId,
        [FromBody] WillReviewStatus willReviewStatus,
        string comment
    )
    {
        await _willReviewService.CompleteReview(User, reviewId, comment, willReviewStatus);
        return Ok();
    }

    [HttpGet("wills/review/{reviewId}")]
    public async Task<IActionResult> GetReview([FromRoute] string reviewId)
    {
        return Ok(await _willReviewService.GetReviewAsync(User, reviewId));
    }

    [HttpGet("wills/review")]
    public async Task<IActionResult> GetMyReview(
        [FromQuery] int count,
        [FromQuery] string? cursorId
    )
    {
        return Ok(await _willReviewService.GetMyReviewAsync(User, count, cursorId));
    }

    [HttpGet("wills/review/requests")]
    public async Task<IActionResult> GetMyReviewRequest(
        [FromQuery] int count,
        [FromQuery] string? cursorId
    )
    {
        return Ok(await _willReviewService.GetMyReviewRequestAsync(User, count, cursorId));
    }

    [HttpGet("wills/review/requests/pendding")]
    public async Task<IActionResult> GetAllReviewRequest(
        [FromQuery] int count,
        [FromQuery] string? cursorId
    )
    {
        return Ok(await _willReviewService.GetAllReviewRequestAsync(User, count, cursorId));
    }
}
