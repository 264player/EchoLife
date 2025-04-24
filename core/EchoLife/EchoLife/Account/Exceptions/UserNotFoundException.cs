using EchoLife.Common.Exceptions;

namespace EchoLife.Account.Exceptions;

public class UserNotFoundException : ResourceNotFoundException
{
    public UserNotFoundException(string userId)
        : base("user not found", $"\"{userId}\". is not found.") { }
}
