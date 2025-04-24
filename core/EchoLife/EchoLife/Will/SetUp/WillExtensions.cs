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
                ?? throw new InvalidOperationException("'Will' settings not found.");

            services.Configure<WillDbContextSettings>(configuration.GetSection("Will"));
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

            services
                .AddScoped<IWillReviewRepository, SqlLiteWillReviewRepository>()
                .AddScoped<IWillReviewService, WillReviewService>();

            return services;
        }

        public static WebApplication EnsureCreatedWillDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<WillDbContext>();
            context.Database.EnsureCreated();
            return app;
        }

        public static WebApplication EnsureDeletedWillDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<WillDbContext>();
            context.Database.EnsureDeleted();
            return app;
        }
    }
}
