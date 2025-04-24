namespace EchoLife.Common.Exceptions;

public class UnknowException : InternalException
{
    public UnknowException(string error, string errorInfo, Exception innerException)
        : base(error, errorInfo, innerException) { }

    public UnknowException(string error, string errorInfo)
        : base(error, errorInfo) { }

    public UnknowException()
        : base("unknown error", "unknow") { }
}
