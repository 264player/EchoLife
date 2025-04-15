using EchoLife.Family.Data;
using EchoLife.Family.Services;
using EchoLife.Will.Data;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Family.Setup;

public static class FamilyExtensions
{
    public static IServiceCollection AddFamily(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var dbContextSettings =
            configuration.GetSection("Family").Get<FamilyDbContextSettings>()
            ?? throw new InvalidOperationException("'Family' settings not found.");
        services.Configure<FamilyDbContextSettings>(configuration.GetSection("Family"));
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
            .AddScoped<IFamilyHistoryRepository, SqlLiteFamilyHistoryRepository>()
            .AddScoped<IFamilySubSectionRepository, SqlLiteSubSectionRepository>()
            .AddScoped<IFamilyTreeRepository, SqlLiteFamilyTreeRepository>()
            .AddScoped<IFamilyMemberRepository, SqlLiteFamilyMemberRepotisory>()
            .AddScoped<IFamilyHistoryService, FamilyHistoryService>()
            .AddScoped<IFamilyTreeService, FamilyTreeService>();

        return services;
    }
}
