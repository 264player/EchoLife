using System.Net;
using System.Net.Http.Json;
using EchoLife.Tests.Integration.Account.Utils;
using EchoLife.Tests.Integration.Utils;
using static System.Guid;

namespace EchoLife.Tests.Integration.Account.Controller;

[TestFixture]
internal class AccountTests : ControllerTestsBase
{
    [Test]
    public async Task Register_WhenValidUsernameAndPassword_ShouldSuccess()
    {
        // Arrange
        var password = "Passw0rd";
        var registerRequest = AccountSeeder.SeedRegisterRequest(
            NewGuid().ToString("N"),
            password,
            password
        );
        using var httpClient = GetClient();

        // Act
        var response = await httpClient.PostAsJsonAsync(UrlPackage.Register(), registerRequest);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task Register_WhenAccountAlreadyCreated_ShouldFail()
    {
        // Arrange
        var password = "Passw0rd";
        var registerRequest = AccountSeeder.SeedRegisterRequest(
            NewGuid().ToString("N"),
            password,
            password
        );
        using var httpClient = GetClient();
        await httpClient.PostAsJsonAsync(UrlPackage.Register(), registerRequest);

        // Act
        var response = await httpClient.PostAsJsonAsync(UrlPackage.Register(), registerRequest);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task Register_WhenInvalidUsernameAndPassword_ShouldFail()
    {
        // Arrange
        var password = "";
        var registerRequest = AccountSeeder.SeedRegisterRequest(
            NewGuid().ToString("N"),
            password,
            password
        );
        using var httpClient = GetClient();

        // Act
        var response = await httpClient.PostAsJsonAsync(UrlPackage.Register(), registerRequest);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
    }
}
