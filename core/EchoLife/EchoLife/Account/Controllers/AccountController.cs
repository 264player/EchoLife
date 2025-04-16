using EchoLife.Account.Dtos;
using EchoLife.Account.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.Account.Controllers;

[Route("api/account")]
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
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _accountService.LoginWithUsernameAsync(loginRequest);

        if (result.Succeeded)
        {
            return Empty;
        }
        if (result.RequiresTwoFactor)
        {
            return Challenge();
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
        return Ok();
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

    //[HttpPost("forget-password")]
    //public async Task<IActionResult> ForgetPassword(string userName)
    //{
    //    var user = await userManager.FindByNameAsync(userName);
    //    if (user == null)
    //    {
    //        return BadRequest("User not found");
    //    }
    //    var token = await userManager.GeneratePasswordResetTokenAsync(user);
    //    return Ok(token);
    //}

    //[HttpPost("reset-password")]
    //public async Task<IActionResult> ResetPassword(string userName, string token, string password)
    //{
    //    var user = await userManager.FindByNameAsync(userName);
    //    if (user == null)
    //    {
    //        return BadRequest("User not found");
    //    }
    //    var result = await userManager.ResetPasswordAsync(user, token, password);
    //    if (!result.Succeeded)
    //    {
    //        return BadRequest(result.Errors);
    //    }
    //    return Ok("Password reset successful");
    //}

    //[HttpPost("manage/2fa")]
    //public async Task<IActionResult> ManageTwoFactorAuthentication(bool enable)
    //{
    //    var user = await userManager.GetUserAsync(User);
    //    if (user == null)
    //    {
    //        return BadRequest("User not found");
    //    }
    //    if (enable)
    //    {
    //        await userManager.SetTwoFactorEnabledAsync(user, true);
    //    }
    //    else
    //    {
    //        await userManager.SetTwoFactorEnabledAsync(user, false);
    //    }
    //    return Ok("Two-factor authentication updated");
    //}

    //[HttpPost("manage/info")]
    //public async Task<IActionResult> ManageInfo(string email, string phoneNumber)
    //{
    //    var user = await userManager.GetUserAsync(User);
    //    if (user == null)
    //    {
    //        return BadRequest("User not found");
    //    }
    //    user.Email = email;
    //    user.PhoneNumber = phoneNumber;
    //    await userManager.UpdateAsync(user);
    //    return Ok("User information updated");
    //}

    //[HttpGet("manage/info")]
    //public async Task<IActionResult> GetInfo()
    //{
    //    var user = await userManager.GetUserAsync(User);
    //    if (user == null)
    //    {
    //        return BadRequest("User not found");
    //    }
    //    return Ok(new { user.Email, user.PhoneNumber });
    //}

    //[Authorize]
    //[HttpGet("check-loined")]
    //public async Task<IActionResult> CheckLoined()
    //{
    //    return Ok();
    //}
}
