using EchoLife.Life.Data;
using EchoLife.Life.Services;
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

            services
                .Configure<LifeDbContextSettings>(configuration.GetSection("Life"))
                .AddScoped<ILifeHitoryRepository, SqlLiteLifeHistoryRepository>()
                .AddScoped<ILifePointRepository, SqlLiteLifePointRepository>()
                .AddScoped<ILifePointUriMapRepository, SqlLiteLifePointUriMapRepository>()
                .AddScoped<ILifePointUserMapRepository, SqlLiteLifePointUserMapRepository>()
                .AddScoped<ILifeSubSectionRepository, SqlLiteLifeSubSectionRepository>()
                .AddScoped<ILifePointService, LifePointService>()
                .AddScoped<ILifeHistoryService, LifeHistoryService>();

            return services;
        }

        public static WebApplication EnsureCreatedLifeDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<LifeDbContext>();
            context.Database.EnsureCreated();
            return app;
        }

        public static WebApplication EnsureDeletedLifeDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<LifeDbContext>();
            context.Database.EnsureDeleted();
            return app;
        }
    }
}
