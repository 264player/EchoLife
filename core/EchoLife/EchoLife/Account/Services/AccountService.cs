﻿using EchoLife.Account.Dtos;
using EchoLife.Account.Models;
using EchoLife.Common.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace EchoLife.Account.Services;

public class AccountService(
    UserManager<IdentityAccount> _userManager,
    SignInManager<IdentityAccount> _signInManager,
    IValidator<RegisterRequest> _registerRequestValidator
) : IAccountService
{
    public async Task<IdentityResult> RegisterAsync(RegisterRequest registerRequest)
    {
        _registerRequestValidator.ValidateAndThrowArgumentException(registerRequest);

        var user = new IdentityAccount { UserName = registerRequest.Username };
        return await _userManager.CreateAsync(user, registerRequest.Password);
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
}
