namespace EchoLife.Common.Exceptions;

public class ForbiddenException : InternalException
{
    public ForbiddenException(string message, Exception innerException)
        : base(message, innerException) { }

    public ForbiddenException(string message)
        : base(message) { }

    public ForbiddenException() { }
}
