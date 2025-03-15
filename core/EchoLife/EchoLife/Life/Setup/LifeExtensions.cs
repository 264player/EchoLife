using EchoLife.Life.Data;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Life.Setup
{
    public static class LifeExtensions
    {
        public static IServiceCollection AddLife(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var dbContextSettings =
                configuration.GetSection("Life").Get<LifeDbContextSettings>()
                ?? throw new InvalidOperationException("'BaseUser' settings not found.");

            services.Configure<LifeDbContextSettings>(configuration.GetSection("Life"));
            if (!string.IsNullOrEmpty(dbContextSettings.MysqlConnetionString))
            {
                services.AddDbContext<LifeDbContext>(options =>
                    options.UseMySql(
                        dbContextSettings.MysqlConnetionString,
                        MySqlServerVersion.AutoDetect(dbContextSettings.MysqlConnetionString)
                    )
                );
            }
            else
            {
                services.AddDbContext<LifeDbContext>(options =>
                    options.UseSqlite(
                        $"Data Source={Path.Combine(
                            Directory.GetCurrentDirectory(),
                            dbContextSettings.SqlLiteConnectionString
                        )}"
                    )
                );
            }

            return services;
        }
    }
}
