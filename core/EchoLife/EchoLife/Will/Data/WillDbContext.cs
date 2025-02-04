using EchoLife.Will.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EchoLife.Will.Data
{
    public class WillDbContext : DbContext
    {
        public WillDbContextSettings Settings { get; set; } = null!;
        public DbSet<OfficiousWill> Wills { get; set; } = null!;
        public DbSet<WillVersion> WillVersions { get; set; } = null!;

        public WillDbContext(
            DbContextOptions<WillDbContext> options,
            IOptions<WillDbContextSettings> willDbContextSettings
        )
            : base(options)
        {
            Settings = willDbContextSettings.Value;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OfficiousWill>(will =>
            {
                will.ToTable(Settings.WillTableName);
                will.HasKey(w => w.Id);

                will.Property(w => w.Id).IsRequired();

                will.Property(w => w.WillType).IsRequired();

                will.Property(w => w.TestaorId).IsRequired();
            });

            modelBuilder.Entity<WillVersion>(wversion =>
            {
                wversion.ToTable(Settings.WillVersionTableName);

                wversion.HasKey(w => w.Id);

                wversion.Property(w => w.Id).IsRequired();

                wversion.Property(w => w.WillId).IsRequired();

                wversion.Property(w => w.Content).IsRequired();

                wversion.Property(w => w.CreatedAt).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
