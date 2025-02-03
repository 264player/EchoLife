namespace EchoLife.User.Dtos
{
    public class BaseUserRequest
    {
        public string Username { get; set; }
        public string NickName { get; set; } = null!;
    }
}
