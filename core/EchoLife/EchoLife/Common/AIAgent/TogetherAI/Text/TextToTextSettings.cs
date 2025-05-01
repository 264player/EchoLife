namespace EchoLife.Common.AIAgent.TogetherAI.Text;

public record TextToTextSettings
{
    public string Url { get; init; } = null!;
    public string Token { get; init; } = null!;
    public string Model { get; init; } = null!;
    public string Pretreatment { get; init; } = null!;
};
