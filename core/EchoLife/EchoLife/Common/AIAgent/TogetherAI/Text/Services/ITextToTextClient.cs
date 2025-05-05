namespace EchoLife.Common.AIAgent.TogetherAI.Text.Services;

public interface ITextToTextClient
{
    Task<string> TalkAsync(string text);
    Task<string> TalkAsync(string message, string prompt);
}
