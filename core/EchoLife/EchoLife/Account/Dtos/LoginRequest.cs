namespace EchoLife.Account.Dtos;

public record class LoginRequest
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public bool RememberMe { get; set; } = false;

    public LoginRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public LoginRequest() { }
}
