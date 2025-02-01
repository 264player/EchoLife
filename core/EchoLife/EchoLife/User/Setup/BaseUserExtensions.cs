using EchoLife.User.Data;
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
            services.Configure<UserDbContextSettings>(builder =>
                configuration.GetSection("BaseUser").Get<UserDbContextSettings>()
            );
            if (dbContextSettings.MysqlConnetionString != null)
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
                    options.UseSqlite(dbContextSettings.SqlLiteConnectionString)
                );
            }

            services.AddScoped<IBaseUserRepository, SqlLiteBaseUserRepository>();
            return services;
        }
    }
}
