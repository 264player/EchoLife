﻿using EchoLife.Common.CRUD;
using EchoLife.Family.Models;

namespace EchoLife.Family.Data;

public interface IFamilyTreeRepository : IEntityRepository<FamilyTree>
{
    Task<List<FamilyTree>> ReadAsync(IEnumerable<string> ids);
}
