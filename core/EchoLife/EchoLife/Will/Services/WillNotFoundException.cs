using EchoLife.Common.Exceptions;

namespace EchoLife.Will.Services;

public class WillNotFoundException : ResourceNotFoundException
{
    public WillNotFoundException(string willId)
        : base("Will not found.", $"Will with ID ${willId} not found.") { }
}
