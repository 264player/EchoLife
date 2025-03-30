﻿using EchoLife.Account.Dtos;
using Microsoft.AspNetCore.Identity;

namespace EchoLife.Account.Services;

public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(RegisterRequest registerRequest);
    Task<SignInResult> LoginWithUsernameAsync(LoginRequest loginRequest);
    Task LogoutAsync();
}
