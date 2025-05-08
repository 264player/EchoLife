using EchoLife.Common.CRUD;
using EchoLife.Family.Models;

namespace EchoLife.Family.Data;

public interface IFamilySubSectionRepository : IEntityRepository<FamilySubSection>
{
    Task<List<FamilySubSection>> ReadAllAsync(string historyId);
}
