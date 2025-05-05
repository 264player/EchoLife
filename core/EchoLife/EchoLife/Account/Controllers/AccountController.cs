using EchoLife.Account.Dtos;
using EchoLife.Account.Services;
using EchoLife.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.Account.Controllers;

[Route("/api/account")]
[ExceptionHandling]
[ApiController]
public class AccountController(IAccountService _accountService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        var result = await _accountService.RegisterAsync(registerRequest);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Empty;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _accountService.LoginWithUsernameAsync(loginRequest);

        if (result.Succeeded)
        {
            return Empty;
        }

        if (result.IsLockedOut)
        {
            return BadRequest("Account was locked.");
        }
        else
        {
            return BadRequest("Login failed.");
        }
    }

    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        await _accountService.RefreshSignInAsync(User);
        return Empty;
    }

    [Authorize]
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await _accountService.LogoutAsync();
        return Ok();
    }

    [Authorize]
    [HttpGet("userinfo")]
    public async Task<IActionResult> GetUserInfo()
    {
        return Ok(await _accountService.GetUserInfoAsync(User));
    }

    [Authorize]
    [HttpGet("{userId}/userinfo")]
    public async Task<IActionResult> GetUserInfo([FromRoute] string userId)
    {
        return Ok(await _accountService.GetUserInfoAsync(userId));
    }

    [Authorize]
    [HttpPost("become-reviewer")]
    public async Task<IActionResult> BecomeAReviewer()
    {
        await _accountService.BecomeAReviewerAsync(User);
        return Ok();
    }
}
