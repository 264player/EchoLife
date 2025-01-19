using EchoLife.User.Data;
using EchoLife.User.Setup;
using Microsoft.EntityFrameworkCore;

namespace EchoLife
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Modules
            builder.Services.AddBaseUser(builder.Configuration);
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
                    var dbContext = scope.ServiceProvider.GetRequiredService<BaseUserDbContext>();
                    dbContext.Database.Migrate(); // 自动应用迁移
                }
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
