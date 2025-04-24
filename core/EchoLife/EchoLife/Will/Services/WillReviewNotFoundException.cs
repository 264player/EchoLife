using EchoLife.Common.Exceptions;

namespace EchoLife.Will.Services;

public class WillReviewNotFoundException : ResourceNotFoundException
{
    public WillReviewNotFoundException(string willReviewId)
        : base("will review notfound.", $"Will review with ID ${willReviewId} not found.") { }

    public WillReviewNotFoundException(string error, string errorInfo)
        : base(error, errorInfo) { }

    public WillReviewNotFoundException(string error, string errorInfo, Exception innerException)
        : base(error, errorInfo, innerException) { }
}
