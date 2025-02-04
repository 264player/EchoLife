using EchoLife.User.Model;

namespace EchoLife.User.Dtos
{
    public record class BaseUserResponse
    {
        public string Id { get; set; } = null!;
        public string NickName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public static BaseUserResponse From(BaseUser baseUser)
        {
            return new BaseUserResponse
            {
                Id = baseUser.Id,
                UserName = baseUser.Username,
                NickName = baseUser.NickName,
                CreatedAt = baseUser.CreatedAt,
            };
        }
    }
}
