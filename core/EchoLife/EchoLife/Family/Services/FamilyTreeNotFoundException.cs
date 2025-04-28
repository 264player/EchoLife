using EchoLife.Common.Exceptions;

namespace EchoLife.Family.Services;

public class FamilyTreeNotFoundException : ResourceNotFoundException
{
    public FamilyTreeNotFoundException(string familyTreeId)
        : base("family tree not found.", $"Family tree with {familyTreeId} is not found.") { }
}
