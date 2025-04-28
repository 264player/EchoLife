using EchoLife.Common.Exceptions;

namespace EchoLife.Life.Services;

public class LifeHistoryNotFoundException : ResourceNotFoundException
{
    public LifeHistoryNotFoundException(string lifeHistoryId)
        : base("life history not found", $"Life hisotry with {lifeHistoryId} not found") { }
}
