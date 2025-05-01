using System.Security.Claims;
using EchoLife.Account.Dtos;
using EchoLife.Account.Exceptions;
using EchoLife.Account.Models;
using EchoLife.Common;
using EchoLife.Common.Exceptions;
using EchoLife.Common.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace EchoLife.Account.Services;

public class AccountService(
    UserManager<IdentityAccount> _userManager,
    SignInManager<IdentityAccount> _signInManager,
    RoleManager<AccountRole> _roleManager,
    IValidator<RegisterRequest> _registerRequestValidator
) : IAccountService
{
    public async Task<IdentityResult> RegisterAsync(RegisterRequest registerRequest)
    {
        _registerRequestValidator.ValidateAndThrowArgumentException(registerRequest);

        var user = new IdentityAccount
        {
            Id = IdGenerator.GenerateUlid(),
            UserName = registerRequest.Username,
        };
        return await _userManager.CreateAsync(user, registerRequest.Password);
    }

    public async Task<IdentityResult> SudoCreateUserAsync(
        IdentityAccount identityAccount,
        string password
    )
    {
        return await _userManager.CreateAsync(identityAccount, password);
    }

    public async Task<SignInResult> LoginWithUsernameAsync(LoginRequest loginRequest)
    {
        var result = await _signInManager.PasswordSignInAsync(
            loginRequest.Username,
            loginRequest.Password,
            loginRequest.RememberMe,
            false
        );
        return result;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task RefreshSignInAsync(ClaimsPrincipal user)
    {
        var result = await _userManager.GetUserAsync(user);
        if (result == null)
        {
            throw new UserNotFoundException(ClaimsManager.GetAuthorizedUserId(user));
        }
        await _signInManager.RefreshSignInAsync(result);
    }

    public async Task<IdentityAccountResponse?> GetUserInfoAsync(ClaimsPrincipal user)
    {
        var result =
            await _userManager.GetUserAsync(user)
            ?? throw new ForbiddenException(ClaimsManager.GetAuthorizedUserId(user));
        return IdentityAccountResponse.From(result);
    }

    public async Task BecomeAReviewerAsync(ClaimsPrincipal me)
    {
        var user =
            await _userManager.GetUserAsync(me)
            ?? throw new ForbiddenException(ClaimsManager.GetAuthorizedUserId(me));

        var roleName = AccountRoles.Reviewer.ToString();
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new AccountRole(roleName));
        }

        if (!await _userManager.IsInRoleAsync(user, roleName))
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                throw new UnknowException("Add role fail.", "Add role to user fail ");
            }
        }
    }

    public async Task AddRoleToUserAsync(string userId, AccountRoles accountRoles)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? throw new ForbiddenException(userId);

        var roleName = accountRoles.ToString();
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new AccountRole(roleName));
        }

        if (!await _userManager.IsInRoleAsync(user, roleName))
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                throw new UnknowException("Add role fail.", "Add role to user fail ");
            }
        }
    }
}
