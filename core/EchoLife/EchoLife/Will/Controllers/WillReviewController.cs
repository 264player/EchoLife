using EchoLife.Common.Dtos;
using EchoLife.Common.Exceptions;
using EchoLife.Will.Dtos;
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
    [HttpPost("wills/versions/{versionId}/reviews/ai")]
    public async Task<IActionResult> RequestAIReview([FromRoute] string versionId)
    {
        return Ok(await _willReviewService.RequestAIReviewAsync(User, versionId));
    }

    [HttpPost("wills/versions/{versionId}/reviews/human")]
    public async Task<IActionResult> RequestHumanReview([FromRoute] string versionId)
    {
        return Ok(await _willReviewService.RequestHumanReviewAsync(User, versionId));
    }

    [HttpGet("wills/versions/reviews/{reviewId}")]
    public async Task<IActionResult> GetReview([FromRoute] string reviewId)
    {
        return Ok(await _willReviewService.GetReviewAsync(User, reviewId));
    }

    [HttpGet("wills/versions/reviews")]
    public async Task<IActionResult> GetMyReview([FromQuery] PageInfo pageInfo)
    {
        return Ok(
            await _willReviewService.GetMyReviewAsync(User, pageInfo.Count, pageInfo.CursorId)
        );
    }

    [HttpGet("wills/versions/reviews/requests")]
    public async Task<IActionResult> GetMyReviewRequest(
        [FromQuery] int count,
        [FromQuery] string? cursorId
    )
    {
        return Ok(await _willReviewService.GetMyReviewRequestAsync(User, count, cursorId));
    }

    [HttpGet("wills/versions/reviews/requests/pendding")]
    public async Task<IActionResult> GetAllReviewRequest(
        [FromQuery] int count,
        [FromQuery] string? cursorId
    )
    {
        return Ok(await _willReviewService.GetAllReviewRequestAsync(User, count, cursorId));
    }

    [HttpPut("wills/versions/reviews/{reviewId}/human/process")]
    public async Task<IActionResult> ProcessHumanReview([FromRoute] string reviewId)
    {
        await _willReviewService.ProcessReview(User, reviewId);
        return Ok();
    }

    [HttpPut("wills/versions/reviews/{reviewId}/human/complete")]
    public async Task<IActionResult> CompleteHumanReview(
        [FromRoute] string reviewId,
        [FromBody] CompleteWillReviewRequest completeWillReviewRequest
    )
    {
        await _willReviewService.CompleteReview(
            User,
            reviewId,
            completeWillReviewRequest.Comment,
            completeWillReviewRequest.Status
        );
        return Ok();
    }

    [HttpPut("wills/versions/reviews/{reviewId}/human/cancel")]
    public async Task<IActionResult> CancleHumanReview([FromRoute] string reviewId)
    {
        await _willReviewService.CancelReviewRequestAsync(User, reviewId);
        return Ok();
    }
}
