using EchoLife.Will.Data;
using EchoLife.Will.Services;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Will.SetUp
{
    public static class WillExtensions
    {
        public static IServiceCollection AddWill(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var dbContextSettings =
                configuration.GetSection("Will").Get<WillDbContextSettings>()
                ?? throw new InvalidOperationException("'BaseUser' settings not found.");

            services.Configure<WillDbContextSettings>(configuration.GetSection("BaseUser"));
            if (!string.IsNullOrEmpty(dbContextSettings.MysqlConnetionString))
            {
                services.AddDbContext<WillDbContext>(options =>
                    options.UseMySql(
                        dbContextSettings.MysqlConnetionString,
                        MySqlServerVersion.AutoDetect(dbContextSettings.MysqlConnetionString)
                    )
                );
            }
            else
            {
                services.AddDbContext<WillDbContext>(options =>
                    options.UseSqlite(
                        $"Data Source={Path.Combine(
                            Directory.GetCurrentDirectory(),
                            dbContextSettings.SqlLiteConnectionString
                        )}"
                    )
                );
            }

            services
                .AddScoped<IOfficiousWillRepository, SqlLiteOfficiousWillRepository>()
                .AddScoped<IWillVersionRepository, SqlLiteWillVersionRepository>()
                .AddScoped<IWillService, WillService>();

            return services;
        }
    }
}
