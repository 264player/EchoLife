using EchoLife.User.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EchoLife.User.Setup
{
    public static class IdentityUserExtensions
    {
        public static IServiceCollection AddIdentityUser(this IServiceCollection services,IConfiguration configuration)
        {
            var settings = configuration.GetSection("Jwt").Get<IdentitySettings>()??throw new InvalidOperationException("'Jwt' settings not found."); ;
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = settings.Issuer,
                        ValidAudience = settings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey))
                    };
                });

            return services;
        }
    }
}
