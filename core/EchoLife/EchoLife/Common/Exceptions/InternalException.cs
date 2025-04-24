using System.Text.Json;

namespace EchoLife.Common.Exceptions;

public class InternalException : Exception
{
    public ExceptionInfo ExceptionInfo { get; set; }

    public InternalException(string error, string errorInfo, Exception innerException)
        : base(errorInfo, innerException)
    {
        ExceptionInfo = new ExceptionInfo { Error = error, ErrorInfo = errorInfo };
    }

    public InternalException(string error, string errorInfo)
        : base(errorInfo)
    {
        ExceptionInfo = new ExceptionInfo { Error = error, ErrorInfo = errorInfo };
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(ExceptionInfo);
    }
}
