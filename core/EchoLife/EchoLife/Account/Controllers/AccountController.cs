using EchoLife.Account.Data;
using EchoLife.Account.Dtos;
using EchoLife.Account.Models;
using EchoLife.Account.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.Account.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController(
    UserManager<IdentityAccount> userManager,
    SignInManager<IdentityAccount> signInManager,
    IAccountService _accountService
) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(string userName, string password)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return BadRequest("Invalid username or password");
        }

        var result = await signInManager.CheckPasswordSignInAsync(
            user,
            password,
            lockoutOnFailure: false
        );
        if (!result.Succeeded)
        {
            return BadRequest("Invalid username or password");
        }

        await signInManager.SignInAsync(user, isPersistent: false);
        return Ok("Login successful");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var result = await _accountService.RegisterAsync(registerRequest);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        await signInManager.RefreshSignInAsync(user);
        return Ok("Refresh successful");
    }

    [HttpPost("forget-password")]
    public async Task<IActionResult> ForgetPassword(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        return Ok(token);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(string userName, string token, string password)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        var result = await userManager.ResetPasswordAsync(user, token, password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok("Password reset successful");
    }

    [HttpPost("manage/2fa")]
    public async Task<IActionResult> ManageTwoFactorAuthentication(bool enable)
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        if (enable)
        {
            await userManager.SetTwoFactorEnabledAsync(user, true);
        }
        else
        {
            await userManager.SetTwoFactorEnabledAsync(user, false);
        }
        return Ok("Two-factor authentication updated");
    }

    [HttpPost("manage/info")]
    public async Task<IActionResult> ManageInfo(string email, string phoneNumber)
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        user.Email = email;
        user.PhoneNumber = phoneNumber;
        await userManager.UpdateAsync(user);
        return Ok("User information updated");
    }

    [HttpGet("manage/info")]
    public async Task<IActionResult> GetInfo()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return BadRequest("User not found");
        }
        return Ok(new { user.Email, user.PhoneNumber });
    }

    [HttpGet("sample")]
    public async Task<IActionResult> Sample()
    {
        var user = new IdentityAccount { UserName = "test", Id = Guid.NewGuid().ToString() };
        var result1 = await userManager.CreateAsync(user, "P@ssw0rd");

        userManager.RegisterTokenProvider("Email", new MyProviders());

        user.TwoFactorEnabled = true;
        await userManager.UpdateAsync(user);

        var is2faEnabled = await userManager.GetTwoFactorEnabledAsync(user);
        var provider = await userManager.GetValidTwoFactorProvidersAsync(user);

        var loginresult = await signInManager.PasswordSignInAsync(
            user,
            "P@ssw0rd",
            isPersistent: false,
            lockoutOnFailure: false
        );

        return Ok();
    }

    [Authorize]
    [HttpGet("check-loined")]
    public async Task<IActionResult> CheckLoined()
    {
        return Ok();
    }

    [Authorize]
    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok();
    }
}
