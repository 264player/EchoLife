using System.Net;
using System.Net.Http.Json;
using EchoLife.Tests.Integration.Account.Controller.Utils;
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

    [Test]
    public async Task Login_WhenIncorrectPasswordOrUsername_ShouldFail()
    {
        // Arrange
        var password = "Passw0rd";
        var registerRequest = AccountSeeder.SeedRegisterRequest(
            NewGuid().ToString("N"),
            password,
            password
        );
        await Sut.RegisterAsync(registerRequest);
        var loginRequest = AccountSeeder.SeedLoginRequest(
            registerRequest.Username,
            "IncorrectPassword"
        );
        using var httpClient = GetClient();

        // Act
        var response = await httpClient.PostAsJsonAsync(UrlPackage.Login(), loginRequest);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.Headers.Contains("Set-Cookie"), Is.False);
        });
    }

    [Test]
    public async Task Login_WhenCorrectUsernameAndPassword_ShouldSuccess()
    {
        // Arrange
        var password = "Passw0rd";
        var registerRequest = AccountSeeder.SeedRegisterRequest(
            NewGuid().ToString("N"),
            password,
            password
        );
        await Sut.RegisterAsync(registerRequest);
        var loginRequest = AccountSeeder.SeedLoginRequest(
            registerRequest.Username,
            registerRequest.Password
        );
        using var httpClient = GetClient();

        // Act
        var response = await httpClient.PostAsJsonAsync(UrlPackage.Login(), loginRequest);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Headers.Contains("Set-Cookie"), Is.True);
        });
    }

    [Test]
    public async Task Logout_WhenUnauthorized_ShouldFail()
    {
        // Arrange
        using var httpClient = GetClient();

        // Act
        var response = await httpClient.GetAsync(UrlPackage.Logout());

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task Logout_WhenAuthorized_ShouldSuccess()
    {
        // Arrange
        var password = "Passw0rd";
        var registerRequest = AccountSeeder.SeedRegisterRequest(
            NewGuid().ToString("N"),
            password,
            password
        );
        using var httpClient = await GetCookieTokenClientAsync(registerRequest);

        // Act
        var response = await httpClient.GetAsync(UrlPackage.Logout());

        var stirng = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Headers.Contains("Set-Cookie"), Is.True);
        });
    }
}
