namespace EchoLife.Account.Dtos;

public record class RegisterRequest
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string EnsurePassword { get; set; } = default!;

    public RegisterRequest(string username, string password, string ensurePassword)
    {
        Username = username;
        Password = password;
        EnsurePassword = ensurePassword;
    }

    public RegisterRequest() { }
}
