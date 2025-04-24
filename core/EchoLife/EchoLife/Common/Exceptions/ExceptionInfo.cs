using System.Text.Json.Serialization;

namespace EchoLife.Common.Exceptions;

public record ExceptionInfo
{
    [JsonPropertyName("error")]
    public string Error { get; set; } = null!;

    [JsonPropertyName("errorinfo")]
    public string ErrorInfo { get; set; } = null!;
}
