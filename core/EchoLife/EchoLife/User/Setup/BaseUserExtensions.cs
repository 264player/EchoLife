using EchoLife.User.Data;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.User.Setup
{
    public static class BaseUserExtensions
    {
        public static IServiceCollection AddBaseUser(this IServiceCollection services, IConfiguration configuration)
        {
            var dbContextSettings = configuration.GetSection("BaseUser").Get<BaseUserDbContextSettings>() ?? throw new InvalidOperationException("'BaseUser' settings not found.");

            services.AddDbContext<BaseUserDbContext>(options => options.UseMySql(dbContextSettings.ConnectionString, MySqlServerVersion.AutoDetect(dbContextSettings.ConnectionString)));

            services.AddScoped<IBaseUserRepository, BaseUserRepository>();
            return services;
        }
    }
}
