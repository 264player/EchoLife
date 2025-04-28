using EchoLife.Common.Exceptions;

namespace EchoLife.Family.Services;

public class FamilyHistoryNotFoundException : ResourceNotFoundException
{
    public FamilyHistoryNotFoundException(string familyHistoryId)
        : base("family history not found.", $"Family history with {familyHistoryId} not fount.") { }
}
