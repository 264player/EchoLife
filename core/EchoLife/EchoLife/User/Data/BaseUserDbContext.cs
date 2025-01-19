using EchoLife.User.Model;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.User.Data
{
    public class BaseUserDbContext : DbContext
    {
        public BaseUserDbContextSettings Settings { get; set; } = null!;
        public DbSet<BaseUser> BaseUsers { get; set; } = null!;

        public BaseUserDbContext(DbContextOptions<BaseUserDbContext> options)
       : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseUser>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id)
                    .IsRequired();

                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.NickName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(u => u.CreatedAt)
                    .IsRequired();
            });
        }
    }
}
