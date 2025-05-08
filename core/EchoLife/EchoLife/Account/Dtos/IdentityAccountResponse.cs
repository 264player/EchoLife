using EchoLife.Account.Models;

namespace EchoLife.Account.Dtos;

public record IdentityAccountResponse(string Id, string? Username, IEnumerable<string>? Roles)
{
    public static IdentityAccountResponse From(
        IdentityAccount identityAccount,
        IEnumerable<string> roles
    )
    {
        return new IdentityAccountResponse(identityAccount.Id, identityAccount.UserName, roles);
    }

    public static IdentityAccountResponse From(IdentityAccount identityAccount)
    {
        return new IdentityAccountResponse(identityAccount.Id, identityAccount.UserName, null);
    }
}
