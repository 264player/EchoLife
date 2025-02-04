namespace EchoLife.User.Dtos
{
    public record class LoginOrRegisterRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
