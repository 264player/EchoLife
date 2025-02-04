namespace EchoLife.User.Services
{
    public interface IIdentityUserService
    {
        public string GenerateToken(string username, string role);
    }
}
