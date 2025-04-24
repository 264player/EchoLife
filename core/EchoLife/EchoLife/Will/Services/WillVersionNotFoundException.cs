using EchoLife.Common.Exceptions;

namespace EchoLife.Will.Services;

public class WillVersionNotFoundException : ResourceNotFoundException
{
    public WillVersionNotFoundException(string versionId)
        : base("Will version not found.", $"Will version with ID ${versionId} not found.") { }
}
