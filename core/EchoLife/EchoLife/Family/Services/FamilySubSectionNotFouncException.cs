using EchoLife.Common.Exceptions;

namespace EchoLife.Family.Services;

public class FamilySubSectionNotFouncException : ResourceNotFoundException
{
    public FamilySubSectionNotFouncException(string subsectionId)
        : base("family subsection not found.", $"Family subsection with {subsectionId} not fount.")
    { }
}
