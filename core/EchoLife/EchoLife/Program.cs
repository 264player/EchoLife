using EchoLife.Account.Setup;
using EchoLife.Account.Validators;
using EchoLife.Family.Setup;
using EchoLife.Life.Setup;
using EchoLife.User.Setup;
using EchoLife.Will.SetUp;
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
        #endregion

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.EnsureCreatedDatabase();
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
        return app;
    }
}
