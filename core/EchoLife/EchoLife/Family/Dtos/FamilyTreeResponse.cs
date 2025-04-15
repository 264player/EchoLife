using EchoLife.Family.Models;

namespace EchoLife.Family.Dtos;

public record FamilyTreeResponse(string Id, string Name, string CreatedUserId, DateTime CreatedAt)
{
    public static FamilyTreeResponse From(FamilyTree familyTree)
    {
        return new(familyTree.Id, familyTree.Name, familyTree.CreatedUserId, familyTree.CreatedAt);
    }
}
