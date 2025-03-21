using EchoLife.User.Data;
using EchoLife.User.Setup;

namespace EchoLife
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Modules
            builder.Services.AddBaseUser(builder.Configuration);
            builder.Services.AddIdentityUser(builder.Configuration);
            #endregion
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
                    try
                    {
                        dbContext.Database.EnsureCreated();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Database migration is fail!\n" + ex.Message);
                    }
                }
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
