using EchoLife.Life.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EchoLife.Life.Data;

public class LifeDbContext(
    DbContextOptions<LifeDbContext> options,
    IOptions<LifeDbContextSettings> lifeDbContextSettings
) : DbContext(options)
{
    public LifeDbContextSettings Settings => lifeDbContextSettings.Value;
    public DbSet<PointUserMap> PointUserMaps { get; set; } = null!;
    public DbSet<LifePoint> LifePoints { get; set; } = null!;
    public DbSet<LifeSubSection> LifeSubSections { get; set; } = null!;
    public DbSet<LifeHistory> LifeHistories { get; set; } = null!;
    public DbSet<LifePointUriMap> LifePointUriMaps { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PointUserMap>(life =>
        {
            life.ToTable(Settings.LifeTableName);
            life.HasKey(l => l.Id);
            life.Property(l => l.Id).IsRequired();
            life.Property(l => l.UserId).IsRequired();
            life.Property(l => l.PointId).IsRequired();
        });

        modelBuilder.Entity<LifePointUriMap>(life =>
        {
            life.ToTable(Settings.LifePointUriMapTableName);
            life.HasKey(l => l.Id);
            life.Property(l => l.Id).IsRequired();
            life.Property(l => l.Uri).IsRequired();
            life.Property(l => l.LifePointId).IsRequired();
        });

        modelBuilder.Entity<LifePoint>(life =>
        {
            life.ToTable(Settings.LifePotintTableName);
            life.HasKey(l => l.Id);
            life.Property(l => l.Id).IsRequired();
            life.Property(l => l.Content).IsRequired();
            life.Property(l => l.Visibility).IsRequired();
            life.Property(l => l.CreatedAt).IsRequired();
            life.Property(l => l.UpdatedAt).IsRequired();
        });

        modelBuilder.Entity<LifeHistory>(life =>
        {
            life.ToTable(Settings.LifePotintTableName);
            life.HasKey(l => l.Id);
            life.Property(l => l.Id).IsRequired();
            life.Property(l => l.UserId).IsRequired();
            life.Property(l => l.Title).IsRequired();
            life.Property(l => l.CreatedAt).IsRequired();
            life.Property(l => l.UpdatedAt).IsRequired();
        });

        modelBuilder.Entity<LifeSubSection>(life =>
        {
            life.ToTable(Settings.LifePotintTableName);
            life.HasKey(l => l.Id);
            life.Property(l => l.Id).IsRequired();
            life.Property(l => l.Title).IsRequired();
            life.Property(l => l.Content).IsRequired();
            life.Property(l => l.LifeHistoryId).IsRequired();
            life.Property(l => l.Deep).IsRequired();
            life.Property(l => l.CreatedAt).IsRequired();
            life.Property(l => l.UpdatedAt).IsRequired();
        });
        base.OnModelCreating(modelBuilder);
    }
}
