using EchoLife.Common.Exceptions;

namespace EchoLife.Account.Exceptions;

public class UserNotFoundException : ResourceNotFoundException
{
    public UserNotFoundException(string username)
        : base($"\"{username}\" is not found.") { }

    public UserNotFoundException()
        : base() { }
}
