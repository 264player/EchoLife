using EchoLife.Common.Exceptions;

namespace EchoLife.Life.Services;

public class LifePointNotFoundException : ResourceNotFoundException
{
    public LifePointNotFoundException(string pointId)
        : base("life point not found", $"Life potin with {pointId} not found.") { }
}
