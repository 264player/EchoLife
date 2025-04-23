using EchoLife.Common.AIAgent.TogetherAI.Text.Dtos;
using Microsoft.Extensions.Options;

namespace EchoLife.Common.AIAgent.TogetherAI.Text.Services;

public class TogetherAITextToTextClient(
    HttpClient _httpClient,
    IOptions<TextToTextSettings> _options
) : ITextToTextClient
{
    private readonly TextToTextSettings settings = _options.Value;

    public async Task<string> TalkAsync(string message)
    {
        var request = new TextToTextRequest
        {
            Model = settings.Model,
            Messages = [new() { Role = "user", Content = message }],
        };

        var result = await TalkAsync(request);

        return result.Choices[0].Message.Content;
    }

    public async Task<string> TalkAsync(string message, string prompt)
    {
        var request = new TextToTextRequest
        {
            Model = settings.Model,
            Messages =
            [
                new() { Role = "system", Content = prompt },
                new() { Role = "user", Content = message },
            ],
        };

        var result = await TalkAsync(request);

        return result.Choices[0].Message.Content;
    }

    private async Task<TextToTextResponse> TalkAsync(TextToTextRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(settings.Url, request);

        response.EnsureSuccessStatusCode();

        var result =
            await response.Content.ReadFromJsonAsync<TextToTextResponse>() ?? throw new Exception();

        return result;
    }
}
