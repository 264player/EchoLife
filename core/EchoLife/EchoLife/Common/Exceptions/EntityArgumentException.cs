namespace EchoLife.Common.Exceptions;

public class EntityArgumentException : InternalException
{
    public EntityArgumentException(string message, Exception innerException)
        : base(message, innerException) { }

    public EntityArgumentException(string message)
        : base(message) { }

    public EntityArgumentException() { }
}
