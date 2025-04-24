using System.Security.Claims;
using EchoLife.Account.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace EchoLife.Account.Services;

public class AccountClaimsPrincipalFactory
    : UserClaimsPrincipalFactory<IdentityAccount, AccountRole>
{
    public AccountClaimsPrincipalFactory(
        UserManager<IdentityAccount> userManager,
        RoleManager<AccountRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor
    )
        : base(userManager, roleManager, optionsAccessor) { }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityAccount user)
    {
        var userRoles = await UserManager.GetRolesAsync(user);
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName),
        };
        foreach (var role in userRoles)
        {
            claims.Add(new(ClaimTypes.Role, role));
        }

        return new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
        ;
    }
}
