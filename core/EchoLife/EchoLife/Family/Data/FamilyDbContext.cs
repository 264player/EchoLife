using EchoLife.Family.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EchoLife.Family.Data;

public class FamilyDbContext(
    DbContextOptions<FamilyDbContext> options,
    IOptions<FamilyDbContextSettings> familyDbContextSettings
) : DbContext(options)
{
    private FamilyDbContextSettings Settings => familyDbContextSettings.Value;
    public DbSet<FamilyTree> FamilyTrees { get; set; }
    public DbSet<FamilyMember> FamilyMembers { get; set; }
    public DbSet<FamilyHistory> FamilyHistories { get; set; }
    public DbSet<FamilySubSection> SubSections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FamilyTree>(tree =>
        {
            tree.ToTable(Settings.FamilyTreeTableName);
            tree.HasKey(t => t.Id);
            tree.Property(t => t.Id).IsRequired();
            tree.Property(t => t.Name);
            tree.Property(t => t.CreatedUserId).IsRequired();
            tree.Property(t => t.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<FamilyMember>(member =>
        {
            member.ToTable(Settings.FamilyMemberTableName);
            member.HasKey(m => m.Id);
            member.Property(m => m.Id).IsRequired();
            member.Property(m => m.UserId).IsRequired();
            member.Property(m => m.FamilyId).IsRequired();
            member.Property(m => m.DisplayName).IsRequired();
            member.Property(m => m.Gender).IsRequired();
            member.Property(m => m.FatherId);
            member.Property(m => m.MotherId);
            member.Property(m => m.SpouseId);
            member.Property(m => m.BirthDate);
            member.Property(m => m.DeathDate);
            member.Property(m => m.Generation).IsRequired();
            member.Property(m => m.PowerLevel).IsRequired();
        });

        modelBuilder.Entity<FamilyHistory>(history =>
        {
            history.ToTable(Settings.FamilyHistoryTableName);
            history.HasKey(h => h.Id);
            history.Property(h => h.Id).IsRequired();
            history.Property(h => h.FamilyId).IsRequired();
            history.Property(h => h.CreatedAt).IsRequired();
            history.Property(h => h.UpdatedAt).IsRequired();
        });

        modelBuilder.Entity<FamilySubSection>(subSection =>
        {
            subSection.ToTable(Settings.SubSectionTableName);
            subSection.HasKey(s => s.Id);
            subSection.Property(s => s.Id).IsRequired();
            subSection.Property(s => s.FamilyHistoryId).IsRequired();
            subSection.Property(s => s.Title).IsRequired();
            subSection.Property(s => s.Content).IsRequired();
            subSection.Property(s => s.Index).IsRequired();
        });
    }
}
