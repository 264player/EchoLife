using EchoLife.Common.Exceptions;

namespace EchoLife.Family.Services;

public class FamilyMemberNotFoundException : ResourceNotFoundException
{
    public FamilyMemberNotFoundException(string familyMemberId)
        : base("family member not found", $"Family member with ID {familyMemberId} is not found.")
    { }
}
