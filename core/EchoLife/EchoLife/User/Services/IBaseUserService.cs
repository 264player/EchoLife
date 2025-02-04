using System.Security.Claims;
using EchoLife.User.Dtos;

namespace EchoLife.User.Services
{
    public interface IBaseUserService
    {
        Task<LoginOrRegisterResponse> RegisterAsync(string username, string password);
        Task<string> LoginAsync(string username, string password);
        Task<BaseUserResponse> GetBaseUserInfo(ClaimsPrincipal principal);
        Task<BaseUserResponse> GetBaseUserInfo(string userId);
        Task<BaseUserResponse> UpdateBaseUserInfo(
            ClaimsPrincipal principal,
            BaseUserRequest request
        );
        Task<BaseUserResponse> UpdateBaseUserInfo(string userId, BaseUserRequest request);
        Task DeleteBaseUser(ClaimsPrincipal principal);
        Task DeleteBaseUser(string userId);
    }
}
