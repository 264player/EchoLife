using EchoLife.User.Data;
using EchoLife.User.Services;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.User.Setup
{
    public static class BaseUserExtensions
    {
        public static IServiceCollection AddBaseUser(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var dbContextSettings =
                configuration.GetSection("BaseUser").Get<UserDbContextSettings>()
                ?? throw new InvalidOperationException("'BaseUser' settings not found.");
            services.Configure<UserDbContextSettings>(configuration.GetSection("BaseUser"));
            if (!string.IsNullOrEmpty(dbContextSettings.MysqlConnetionString))
            {
                services.AddDbContext<UserDbContext>(options =>
                    options.UseMySql(
                        dbContextSettings.MysqlConnetionString,
                        MySqlServerVersion.AutoDetect(dbContextSettings.MysqlConnetionString)
                    )
                );
            }
            else
            {
                services.AddDbContext<UserDbContext>(options =>
                    options.UseSqlite(
                        $"Data Source={Path.Combine(
                            Directory.GetCurrentDirectory(),
                            dbContextSettings.SqlLiteConnectionString
                        )}"
                    )
                );
            }

            services.AddScoped<IBaseUserRepository, SqlLiteBaseUserRepository>();
            services.AddScoped<IBaseUserService, BaseUserService>();
            return services;
        }
    }
}
