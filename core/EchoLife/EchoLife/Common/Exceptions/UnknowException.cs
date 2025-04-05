namespace EchoLife.Common.Exceptions;

public class UnknowException : InternalException
{
    public UnknowException(string message, Exception innerException)
        : base(message, innerException) { }

    public UnknowException(string message)
        : base(message) { }

    public UnknowException() { }
}
