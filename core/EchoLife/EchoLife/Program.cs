using System.Text.Json;
using System.Text.Json.Serialization;
using EchoLife.Account.Setup;
using EchoLife.Account.Validators;
using EchoLife.Common.AIAgent.TogetherAI.Text.Setup;
using EchoLife.Common.MinIO.Setup;
using EchoLife.Common.Setup;
using EchoLife.Family.Setup;
using EchoLife.Life.Setup;
using EchoLife.User.Setup;
using EchoLife.Will.Setup;
using FluentValidation;

namespace EchoLife;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Validators
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
        #endregion

        #region Modules
        builder.Services.AddBaseUser(builder.Configuration);
        builder.Services.AddIdentityUser(builder.Configuration);
        builder.Services.AddAccout(builder.Configuration, builder.Environment);
        builder.Services.AddLife(builder.Configuration);
        builder.Services.AddWill(builder.Configuration);
        builder.Services.AddFamily(builder.Configuration);
        builder.Services.AddTextToTextAiAgent(builder.Configuration);
        builder.Services.AddMinIOStorage(builder.Configuration);
        #endregion

        #region Logger
        builder.Logging.AddConsoleLogger();
        #endregion

        builder
            .Services.AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                )
            );

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowAll",
                builder =>
                {
                    builder
                        .WithOrigins("http://localhost:5173", "http://localhost")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                }
            );
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.EnsureDeletedDatabse();
            app.EnsureCreatedDatabase();
            app.UseCors("AllowAll");
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

public static class ProgramExtensions
{
    public static WebApplication EnsureCreatedDatabase(this WebApplication app)
    {
        app.EnsureCreatedAccountDatabase();
        app.EnsureCreatedWillDatabase();
        app.EnsureCreatedLifeDatabase();
        app.EnsureCreatedFamilyDatabase();
        return app;
    }

    public static WebApplication EnsureDeletedDatabse(this WebApplication app)
    {
        app.EnsureDeletedAccountDatabase();
        app.EnsureDeletedWillDatabase();
        app.EnsureDeletedLifeDatabase();
        app.EnsureDeletedFamilyDatabase();
        return app;
    }
}
