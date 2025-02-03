using EchoLife.User.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EchoLife.User.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContextSettings Settings { get; set; } = null!;
        public DbSet<BaseUser> BaseUsers { get; set; } = null!;

        public UserDbContext(
            DbContextOptions<UserDbContext> options,
            IOptions<UserDbContextSettings> baseUserDbContextSettings
        )
            : base(options)
        {
            Settings = baseUserDbContextSettings.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseUser>(entity =>
            {
                entity.ToTable(Settings.BaseUserTableName);
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id).IsRequired();

                entity.Property(u => u.Username).IsRequired().HasMaxLength(100);

                entity.Property(u => u.NickName).IsRequired().HasMaxLength(150);

                entity.Property(u => u.CreatedAt).IsRequired();

                entity.Property(u => u.Password).IsRequired().HasMaxLength(256);
            });
        }
    }
}
