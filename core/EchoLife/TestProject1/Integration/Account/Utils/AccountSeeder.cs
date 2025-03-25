using EchoLife.Account.Dtos;

namespace EchoLife.Tests.Integration.Account.Utils;

internal class AccountSeeder
{
    public static RegisterRequest SeedRegisterRequest(
        string? username = null,
        string? password = null,
        string? ensurePassowrd = null
    )
    {
        return new RegisterRequest
        {
            Username = username ?? Guid.NewGuid().ToString(),
            Password = password ?? Guid.NewGuid().ToString(),
            EnsurePassword = ensurePassowrd ?? Guid.NewGuid().ToString(),
        };
    }
}
