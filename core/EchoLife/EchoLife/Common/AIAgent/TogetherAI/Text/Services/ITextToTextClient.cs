namespace EchoLife.Common.AIAgent.TogetherAI.Text.Services;

public interface ITextToTextClient
{
    Task<string> TalkAsync(string text);
}
