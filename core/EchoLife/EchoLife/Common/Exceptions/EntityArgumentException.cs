namespace EchoLife.Common.Exceptions;

public class EntityArgumentException : InternalException
{
    public EntityArgumentException(string error, string errorInfo, Exception innerException)
        : base(error, errorInfo, innerException) { }

    public EntityArgumentException(string error, string errorInfo)
        : base(error, errorInfo) { }
}
