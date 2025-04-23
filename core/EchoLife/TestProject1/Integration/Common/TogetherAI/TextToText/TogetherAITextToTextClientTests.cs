using System.Net.Http.Headers;
using EchoLife.Common.AIAgent.TogetherAI.Text;
using EchoLife.Common.AIAgent.TogetherAI.Text.Services;
using EchoLife.Tests.Integration.Utils.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EchoLife.Tests.Integration.Common.TogetherAI.TextToText;

internal class TogetherAITextToTextClientTests : TestBase<TogetherAITextToTextClient>
{
    [OneTimeSetUp]
    public new void OneTimeSetUp()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Configuration.AddUserSecrets<Program>();
        var config = builder.Configuration;
        var setting = config.GetSection("AIAgent:TogetherAI:Text").Get<TextToTextSettings>();
        Assert.That(setting, Is.Not.Null);

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            setting.Token
        );

        Sut = new TogetherAITextToTextClient(httpClient, Options.Create(setting));
    }

    [Explicit]
    [Test]
    public async Task TalkAsync_ShouldReturnMessages()
    {
        // Arrange
        // Act
        var result = await Sut.TalkAsync("this is a joke...");

        // Assert
        Assert.That(result, Is.Not.Null);
    }

    [Explicit]
    [Test]
    public async Task TalkAsync_WhenUsePrompt_ShouldReturnMessages()
    {
        // Arrange
        // Act
        var result = await Sut.TalkAsync("this is a joke...", "this is real test");

        // Assert
        Assert.That(result, Is.Not.Null);
    }
}
