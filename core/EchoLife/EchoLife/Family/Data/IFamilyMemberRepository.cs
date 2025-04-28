using EchoLife.Common.CRUD;
using EchoLife.Family.Models;

namespace EchoLife.Family.Data;

public interface IFamilyMemberRepository : IEntityRepository<FamilyMember>
{
    Task<List<FamilyMember>> ReadByFamilyIdAsync(string familyId);
}
