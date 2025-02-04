using EchoLife.User.Dtos;
using EchoLife.User.Model;
using static System.Guid;

namespace EchoLife.Tests.Integration.User.Utils
{
    internal static class UserSeeder
    {
        public static BaseUser SeedBaseUser(
            string? id = null,
            string? username = null,
            string? nickName = null,
            string? password = null,
            DateTime? createAt = null
        )
        {
            return new()
            {
                Id = id ?? NewGuid().ToString(),
                Username = username ?? NewGuid().ToString(),
                NickName = nickName ?? NewGuid().ToString(),
                Password = password ?? NewGuid().ToString(),
                CreatedAt = createAt ?? DateTime.UtcNow,
            };
        }

        public static BaseUserRequest SeedBaseUserRequest(
            string? username = null,
            string? nickName = null
        )
        {
            return new()
            {
                Username = username ?? NewGuid().ToString(),
                NickName = nickName ?? NewGuid().ToString(),
            };
        }

        public static LoginOrRegisterRequest SeedLoginOrRegisterRequest(
            string? username = null,
            string? password = null
        )
        {
            return new()
            {
                Username = username ?? NewGuid().ToString(),
                Password = password ?? NewGuid().ToString(),
            };
        }
    }
}
