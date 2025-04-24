namespace EchoLife.Common.Exceptions;

public class ForbiddenException : InternalException
{
    public ForbiddenException(string error, string errorInfo, Exception innerException)
        : base(error, errorInfo, innerException) { }

    public ForbiddenException(string error, string errorInfo)
        : base(error, errorInfo) { }

    public ForbiddenException(string userId)
        : base("Forbidden", $"User {userId}, you do not possess the required identity.") { }
}
