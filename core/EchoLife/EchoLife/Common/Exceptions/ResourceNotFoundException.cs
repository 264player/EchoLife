namespace EchoLife.Common.Exceptions;

public class ResourceNotFoundException : InternalException
{
    public ResourceNotFoundException(string error, string errorInfo, Exception innerException)
        : base(error, errorInfo, innerException) { }

    public ResourceNotFoundException(string error, string errorInfo)
        : base(error, errorInfo) { }
}
