using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EchoLife.User.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EchoLife.User.Services
{
    public class IdentityUserService : IIdentityUserService
    {
        private IdentitySettings _settings;

        public IdentityUserService(IOptions<IdentitySettings> settings)
        {
            _settings = settings.Value;
        }

        public string GenerateToken(string username, string role)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, username),
                new(ClaimTypes.Role, role),
                new(ClaimTypes.NameIdentifier, username),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
