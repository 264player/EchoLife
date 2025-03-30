using EchoLife.Account.Dtos;
using static System.Guid;

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
            Username = username ?? NewGuid().ToString(),
            Password = password ?? NewGuid().ToString(),
            EnsurePassword = ensurePassowrd ?? NewGuid().ToString(),
        };
    }

    public static LoginRequest SeedLoginRequest(
        string? username = null,
        string? password = null,
        bool remenmberMe = false
    )
    {
        return new LoginRequest
        {
            Username = username ?? NewGuid().ToString(),
            Password = password ?? NewGuid().ToString(),
            RememberMe = remenmberMe,
        };
    }
}
