using EchoLife.Account.Models;

namespace EchoLife.Account.Dtos;

public record IdentiryAccountResponse(string? Username)
{
    public static IdentiryAccountResponse From(IdentityAccount identityAccount)
    {
        return new IdentiryAccountResponse(identityAccount.UserName);
    }
}
