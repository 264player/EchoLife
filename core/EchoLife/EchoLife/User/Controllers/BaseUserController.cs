using EchoLife.User.Dtos;
using EchoLife.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.User.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BaseUserController : ControllerBase
    {
        private IBaseUserService _baseUserService;

        public BaseUserController(IBaseUserService baseUserService)
        {
            _baseUserService = baseUserService;
        }

        [HttpPost("BaseUser/Register")]
        public async Task<IActionResult> Register([FromBody] LoginOrRegisterRequest request)
        {
            try
            {
                var result = await _baseUserService.RegisterAsync(
                    request.Username,
                    request.Password
                );
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("BaseUser/Login")]
        public async Task<IActionResult> Login([FromBody] LoginOrRegisterRequest request)
        {
            try
            {
                var result = await _baseUserService.LoginAsync(request.Username, request.Password);
                return Ok(new { token = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Get()
        {
            var result = await _baseUserService.GetBaseUserInfo(User);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("me")]
        public async Task<IActionResult> Put(BaseUserRequest request)
        {
            return Ok(await _baseUserService.UpdateBaseUserInfo(User, request));
        }

        [Authorize]
        [HttpDelete("me")]
        public async Task<IActionResult> Delete()
        {
            await _baseUserService.DeleteBaseUser(User);
            return NoContent();
        }
    }
}
