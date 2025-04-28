using EchoLife.Common.Exceptions;

namespace EchoLife.Life.Services;

public class LifeSubSectionNotFoundException : ResourceNotFoundException
{
    public LifeSubSectionNotFoundException(string subSectionId)
        : base("life subsection not found.", $"Life subsection with {subSectionId} not found.") { }
}
