using EchoLife.Account.Models;
using Microsoft.AspNetCore.Identity;

namespace EchoLife.Account.Data;

public class MyProviders : IUserTwoFactorTokenProvider<IdentityAccount>
{
    public async Task<bool> CanGenerateTwoFactorTokenAsync(
        UserManager<IdentityAccount> manager,
        IdentityAccount user
    )
    {
        return true;
    }

    public async Task<string> GenerateAsync(
        string purpose,
        UserManager<IdentityAccount> manager,
        IdentityAccount user
    )
    {
        return "fake";
    }

    public async Task<bool> ValidateAsync(
        string purpose,
        string token,
        UserManager<IdentityAccount> manager,
        IdentityAccount user
    )
    {
        return true;
    }
}
