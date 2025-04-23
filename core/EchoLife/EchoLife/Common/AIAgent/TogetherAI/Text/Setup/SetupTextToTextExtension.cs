using EchoLife.Common.AIAgent.TogetherAI.Text.Services;

namespace EchoLife.Common.AIAgent.TogetherAI.Text.Setup;

public static class SetupTextToTextExtension
{
    public static IServiceCollection AddTextToTextAiAgent(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var textToTextSettings =
            configuration.GetSection("AIAgent:TogetherAI:Text").Get<TextToTextSettings>()
            ?? throw new Exception();

        services
            .Configure<TextToTextSettings>(configuration.GetSection("AIAgent:TogetherAI:Text"))
            .AddHttpClient<ITextToTextClient, TogetherAITextToTextClient>(x =>
            {
                x.DefaultRequestHeaders.Add("Content-Type", "application/json");
                x.DefaultRequestHeaders.Add("Authorization", $"Bearer ${textToTextSettings.Token}");
            });

        return services;
    }
}
