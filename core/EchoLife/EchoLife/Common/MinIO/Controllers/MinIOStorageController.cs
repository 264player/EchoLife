using EchoLife.Account.Services;
using EchoLife.Common.Exceptions;
using EchoLife.Common.MinIO.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.Common.MinIO.Controllers;

[Route("/api")]
[ApiController]
[ExceptionHandling]
public class MinIOStorageController(IStorageService _storageService) : ControllerBase
{
    const string BucketName = "static";

    [Authorize]
    [HttpGet("objects")]
    public async Task<IActionResult> GetMyObjects()
    {
        var myId = ClaimsManager.GetAuthorizedUserId(User);
        return Ok(await _storageService.ListObjectsAsync(BucketName, myId));
    }

    [Authorize]
    [HttpDelete("objects")]
    public async Task<IActionResult> DeleteObjects([FromQuery] string fileName)
    {
        var myId = ClaimsManager.GetAuthorizedUserId(User);
        await _storageService.RemoveObjectAsync(BucketName, $"{myId}/{fileName}");
        return Ok();
    }
}
