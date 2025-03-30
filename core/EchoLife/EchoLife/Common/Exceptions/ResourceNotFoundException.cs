namespace EchoLife.Common.Exceptions;

public class ResourceNotFoundException : InternalException
{
    public ResourceNotFoundException(string message, Exception innerException)
        : base(message, innerException) { }

    public ResourceNotFoundException(string message)
        : base(message) { }

    public ResourceNotFoundException() { }
}
