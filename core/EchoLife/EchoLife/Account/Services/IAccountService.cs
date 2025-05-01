using System.Security.Claims;
using EchoLife.Account.Dtos;
using EchoLife.Account.Models;
using Microsoft.AspNetCore.Identity;

namespace EchoLife.Account.Services;

public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(RegisterRequest registerRequest);
    Task<IdentityResult> SudoCreateUserAsync(IdentityAccount identityAccount, string password);
    Task<SignInResult> LoginWithUsernameAsync(LoginRequest loginRequest);
    Task LogoutAsync();
    Task RefreshSignInAsync(ClaimsPrincipal user);
    Task<IdentityAccountResponse?> GetUserInfoAsync(ClaimsPrincipal user);
    Task BecomeAReviewerAsync(ClaimsPrincipal me);
    Task AddRoleToUserAsync(string userId, AccountRoles accountRoles);
}
