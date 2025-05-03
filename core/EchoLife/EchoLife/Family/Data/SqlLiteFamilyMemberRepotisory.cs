using System.Linq.Expressions;
using EchoLife.Family.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoLife.Family.Data;

public class SqlLiteFamilyMemberRepotisory(FamilyDbContext _familyDbContext)
    : IFamilyMemberRepository
{
    private DbSet<FamilyMember> FamilyMembers => _familyDbContext.FamilyMembers;

    public async Task<FamilyMember?> CreateAsync(FamilyMember entity)
    {
        await FamilyMembers.AddAsync(entity);
        var result = await _familyDbContext.SaveChangesAsync();
        return result > 0 ? entity : null;
    }

    public async Task<FamilyMember?> ReadAsync(string id)
    {
        return await FamilyMembers.Where(m => m.Id == id).SingleOrDefaultAsync();
    }

    public async Task<List<FamilyMember>> ReadAsync(
        Expression<Func<FamilyMember, bool>> express,
        int count
    )
    {
        return await FamilyMembers
            .Where(express)
            .OrderByDescending(m => m.Id)
            .Take(count)
            .ToListAsync();
    }

    public async Task<List<FamilyMember>> ReadByFamilyIdAsync(string familyId)
    {
        return await FamilyMembers.Where(m => m.FamilyId == familyId).ToListAsync();
    }

    public async Task<FamilyMember?> UpdateAsync(FamilyMember entity)
    {
        var result = await FamilyMembers
            .Where(m => m.Id == entity.Id)
            .ExecuteUpdateAsync(member =>
                member
                    .SetProperty(m => m.DisplayName, entity.DisplayName)
                    .SetProperty(m => m.FatherId, entity.FatherId)
                    .SetProperty(m => m.MotherId, entity.MotherId)
                    .SetProperty(m => m.SpouseId, entity.SpouseId)
                    .SetProperty(m => m.BirthDate, entity.BirthDate)
                    .SetProperty(m => m.DeathDate, entity.DeathDate)
                    .SetProperty(m => m.Generation, entity.Generation)
                    .SetProperty(m => m.PowerLevel, entity.PowerLevel)
            );
        return result > 0 ? entity : null;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await (FamilyMembers.Where(m => m.Id == id).ExecuteDeleteAsync()) > 0;
    }
}
