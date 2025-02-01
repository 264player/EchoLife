using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EchoLife.User.Data;
using Microsoft.IdentityModel.Tokens;

namespace EchoLife.User.Services
{
    public class IdentityUserService
    {
        private IdentitySettings _settings;

        public IdentityUserService(IdentitySettings settings)
        {
            _settings = settings;
        }

        public string GenerateJwtToken(string username, string role)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, username),
                new(ClaimTypes.Role, role),
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
