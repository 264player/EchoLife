using System.Security.Claims;
using EchoLife.User.Data;
using EchoLife.User.Dtos;
using EchoLife.User.Model;

namespace EchoLife.User.Services
{
    public class BaseUserService : IBaseUserService
    {
        private IBaseUserRepository _baseUserRepository;
        private IIdentityUserService _identityUserService;

        public BaseUserService(
            IBaseUserRepository baseUserRepository,
            IIdentityUserService identityUserService
        )
        {
            _baseUserRepository = baseUserRepository;
            _identityUserService = identityUserService;
        }

        public async Task DeleteBaseUser(string userId)
        {
            await _baseUserRepository.DeleteAsync(userId);
        }

        public async Task DeleteBaseUser(ClaimsPrincipal principal)
        {
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new Exception("UserId NotFound!");
            }
            await DeleteBaseUser(userId);
        }

        public async Task<BaseUserResponse> GetBaseUserInfo(ClaimsPrincipal principal)
        {
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new Exception("UserId NotFound!");
            }
            return await GetBaseUserInfo(userId);
        }

        public async Task<BaseUserResponse> GetBaseUserInfo(string userId)
        {
            var baseUser = await _baseUserRepository.ReadAsync(userId);
            if (baseUser == null)
            {
                throw new Exception("UserNotFound");
            }
            return BaseUserResponse.From(baseUser);
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var baseUser = await _baseUserRepository.ReadByUsernameAsync(username);
            if (baseUser == null)
            {
                throw new Exception($"{username} is notfound.");
            }

            if (baseUser.Password == password)
            {
                return _identityUserService.GenerateToken(username, password);
            }
            return null;
        }

        public async Task<LoginOrRegisterResponse> RegisterAsync(string username, string password)
        {
            var baseUser = await _baseUserRepository.ReadByUsernameAsync(username);
            if (baseUser != null)
            {
                throw new Exception($"User {username} was already exists.");
            }
            var userId = Guid.NewGuid().ToString();
            await _baseUserRepository.CreateAsync(
                new BaseUser
                {
                    Id = userId,
                    Username = username,
                    Password = password,
                    NickName = username,
                }
            );
            return new LoginOrRegisterResponse
            {
                Id = userId,
                Token = _identityUserService.GenerateToken(userId, password),
            };
        }

        public async Task<BaseUserResponse> UpdateBaseUserInfo(
            ClaimsPrincipal principal,
            BaseUserRequest request
        )
        {
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new Exception("UserId NotFound!");
            }
            return await UpdateBaseUserInfo(userId, request);
        }

        public async Task<BaseUserResponse> UpdateBaseUserInfo(
            string userId,
            BaseUserRequest request
        )
        {
            var baseUser = new BaseUser
            {
                Id = userId,
                Username = request.Username,
                NickName = request.NickName,
            };
            return BaseUserResponse.From(await _baseUserRepository.UpdateAsync(baseUser));
        }
    }
}
