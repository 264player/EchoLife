namespace EchoLife.User.Dtos
{
    public record class LoginOrRegisterResponse
    {
        public required string Token { get; set; } = null!;
        public required string Id { get; set; } = null;
    }
}
