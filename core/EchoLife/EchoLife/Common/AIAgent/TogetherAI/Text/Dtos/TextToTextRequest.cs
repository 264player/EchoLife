using System.Text.Json.Serialization;

namespace EchoLife.Common.AIAgent.TogetherAI.Text.Dtos;

internal record TextToTextRequest
{
    [JsonPropertyName("model")]
    public string Model { get; set; } = null!;

    [JsonPropertyName("messages")]
    public List<MessageRequest> Messages { get; set; } = null!;
}

internal record MessageRequest
{
    [JsonPropertyName("role")]
    public string Role { get; set; } = null!;

    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
}
