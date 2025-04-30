using EchoLife.Account.Models;

namespace EchoLife.Account.Dtos;

public record IdentityAccountResponse(string Id, string? Username)
{
    public static IdentityAccountResponse From(IdentityAccount identityAccount)
    {
        return new IdentityAccountResponse(identityAccount.Id, identityAccount.UserName);
    }
}
