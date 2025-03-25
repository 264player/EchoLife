using EchoLife.Account.Data;
using EchoLife.Account.Models;
using EchoLife.Account.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Account.Setup;

public static class AccountExtensions
{
    public static IServiceCollection AddAccout(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment
    )
    {
        var dbContextSettings = configuration.GetSection("Account").Get<AccountDbContextSettings>();
        services.Configure<AccountDbContextSettings>(configuration.GetSection("Account"));

        //services.AddDbContext<AccountDbContext>(options =>
        //{
        //    options.UseInMemoryDatabase("Account");
        //});

        if (!string.IsNullOrEmpty(dbContextSettings?.MysqlConnectionString))
        {
            services.AddDbContext<AccountDbContext>(options =>
                options.UseMySql(
                    dbContextSettings.MysqlConnectionString,
                    MySqlServerVersion.AutoDetect(dbContextSettings.MysqlConnectionString)
                )
            );
        }
        else
        {
            services.AddDbContext<AccountDbContext>(options =>
                options.UseSqlite(
                    $"Data Source={Path.Combine(
                        Directory.GetCurrentDirectory(),
                        dbContextSettings.SqlLiteConnectionString
                    )}"
                )
            );
        }

        services
            .AddIdentityCore<IdentityAccount>()
            .AddEntityFrameworkStores<AccountDbContext>()
            .AddApiEndpoints()
            .AddDefaultTokenProviders();

        services
            .AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            })
            .AddCookie(IdentityConstants.ApplicationScheme);
        //.AddJwtBearer(IdentityConstants.BearerScheme);

        services.AddAuthorization();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "hollow.love";
            // Cookie 的过期时间
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            // 如果需要，可以设置滑动过期
            options.SlidingExpiration = true;
            // Cookie 的安全属性
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.HttpOnly = true;
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;
        });

        services.AddScoped<IAccountService, AccountService>();

        return services;
    }

    public static WebApplication EnsureCreatedAccountDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AccountDbContext>();
        context.Database.EnsureCreated();
        return app;
    }
}
