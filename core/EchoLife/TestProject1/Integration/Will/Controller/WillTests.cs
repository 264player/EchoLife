using System.Net;
using System.Net.Http.Json;
using EchoLife.Tests.Integration.Account.Controller.Utils;
using EchoLife.Tests.Integration.Utils;
using EchoLife.Tests.Integration.Will.Controller.Utils;
using EchoLife.Tests.Integration.Will.Utils;
using EchoLife.Will.Dtos;

namespace EchoLife.Tests.Integration.Will.Controller;

internal class WillTests : ControllerTestsBase
{
    #region Will
    [Test]
    public async Task PostWill_WhenUnauthoried_ShouldFail()
    {
        // Arrange
        var willRequest = WillSeeder.SeedWillRequest();
        var client = GetClient();

        // Act
        var response = await client.PostAsJsonAsync(UrlPackage.Wills(), willRequest);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task PostWill_WhenAuthoried_ShouldSuccess()
    {
        // Arrange
        var willRequest = WillSeeder.SeedWillRequest();
        var client = await GetCookieTokenClientAsync(Guid.NewGuid().ToString());

        // Act
        var response = await client.PostAsJsonAsync(UrlPackage.Wills(), willRequest);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<WillResponse>();
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(willRequest.Name));
    }

    [Test]
    public async Task GetWills_WhenUnauthorized_ShouldFail()
    {
        // Arrange
        var client = GetClient();

        // Act
        var response = await client.GetAsync(UrlPackage.Wills(2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task GetWills_WhenNotExist_ShouldReturnEmpty()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var client = await GetCookieTokenClientAsync(userId);

        // Act
        var response = await client.GetAsync(UrlPackage.Wills(2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<List<WillResponse>>();
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetWills_WhenExists_ShouldReturnList()
    {
        // Arrange
        var client = await GetCookieTokenClientAsync(Guid.NewGuid().ToString());
        var userId = (await client.GetUserInfoAsync()).Id;
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        Assert.That(await Sut.CreateWillAsync(will), Is.Not.Null);

        // Act
        var response = await client.GetAsync(UrlPackage.Wills(2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<List<WillResponse>>();
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Id, Is.EqualTo(will.Id));
    }

    [Test]
    public async Task PutWill_WhenUnauthorized_ShouldFail()
    {
        // Arrange
        var client = GetClient();
        var will = WillSeeder.SeedOfficiousWill();
        Assert.That(await Sut.CreateWillAsync(will), Is.Not.Null);
        var putWillRequest = new PutWillRequest(
            "newName",
            EchoLife.Will.Models.WillType.SelfWritten,
            "VersionId"
        );

        // Act
        var response = await client.PutAsJsonAsync(UrlPackage.Will(will.Id), putWillRequest);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task PutWill_WhenNotExists_ShouldReturnNotFound()
    {
        // Arrange
        var client = await GetCookieTokenClientAsync(Guid.NewGuid().ToString());
        var userId = (await client.GetUserInfoAsync()).Id;
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        var putWillRequest = new PutWillRequest(
            "newName",
            EchoLife.Will.Models.WillType.SelfWritten,
            "VersionId"
        );

        // Act
        var response = await client.PutAsJsonAsync(UrlPackage.Will(will.Id), putWillRequest);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task PutWill_WhenExists_ShouldSuccess()
    {
        // Arrange
        var client = await GetCookieTokenClientAsync(Guid.NewGuid().ToString());
        var userId = (await client.GetUserInfoAsync()).Id;
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        Assert.That(await Sut.CreateWillAsync(will), Is.Not.Null);
        var putWillRequest = new PutWillRequest(
            "newName",
            EchoLife.Will.Models.WillType.SelfWritten,
            "VersionId"
        );

        // Act
        var response = await client.PutAsJsonAsync(UrlPackage.Will(will.Id), putWillRequest);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<WillResponse>();
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo(putWillRequest.Name));
    }

    [Test]
    public async Task DeleteWill_WhenUnauthorized_ShouldFail()
    {
        // Arrange
        var client = GetClient();

        // Act
        var response = await client.DeleteAsync(UrlPackage.Will("whatever"));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task DeleteWill_WhenAuthorized_ShouldSuccess()
    {
        // Arrange
        var client = await GetCookieTokenClientAsync(Guid.NewGuid().ToString());
        var userId = (await client.GetUserInfoAsync()).Id;
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        Assert.That(await Sut.CreateWillAsync(will), Is.Not.Null);

        // Act
        var response = await client.DeleteAsync(UrlPackage.Will(will.Id));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        var result = await Sut.ReadWillAsync(will.Id);
        Assert.That(result, Is.Null);
    }
    #endregion

    #region Will Version
    [Test]
    public async Task PostWillVersion_WhenUnauthoried_ShouldFail()
    {
        // Arrange
        var willRequest = WillSeeder.SeedWillRequest();
        var client = GetClient();

        // Act
        var response = await client.PostAsJsonAsync(
            UrlPackage.WillVersions("whatever"),
            willRequest
        );

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task PostWillVersion_WhenAuthoried_ShouldSuccess()
    {
        // Arrange
        var versionRequest = WillSeeder.SeedWillVersionRequest();
        var client = await GetCookieTokenClientAsync(Guid.NewGuid().ToString());
        var userId = (await client.GetUserInfoAsync()).Id;
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);

        // Act
        var response = await client.PostAsJsonAsync(
            UrlPackage.WillVersions(will.Id),
            versionRequest
        );

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<WillVersionResponse>();
        Assert.That(result, Is.Not.Null);
        Assert.That(result.WillId, Is.EqualTo(will.Id));
    }

    [Test]
    public async Task GetWillVersions_WhenUnauthorized_ShouldFail()
    {
        // Arrange
        var client = GetClient();

        // Act
        var response = await client.GetAsync(UrlPackage.WillVersions("whatever", 2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task GetWillVersions_WhenNotExist_ShouldReturnEmpty()
    {
        // Arrange
        var client = await GetCookieTokenClientAsync(Guid.NewGuid().ToString());
        var userId = (await client.GetUserInfoAsync()).Id;
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);

        // Act
        var response = await client.GetAsync(UrlPackage.WillVersions(will.Id, 2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<List<WillVersionResponse>>();
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetWillVersions_WhenExists_ShouldReturnList()
    {
        // Arrange
        var client = await GetCookieTokenClientAsync(Guid.NewGuid().ToString());
        var userId = (await client.GetUserInfoAsync()).Id;
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);
        var version = WillSeeder.SeedWillVersion(w => w.WillId = will.Id);
        await Sut.CreateWillVersionAsync(version);

        // Act
        var response = await client.GetAsync(UrlPackage.WillVersions(will.Id, 2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<List<WillVersionResponse>>();
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Id, Is.EqualTo(version.Id));
    }

    [Test]
    public async Task PutWillVersion_WhenUnauthorized_ShouldFail()
    {
        // Arrange
        var client = GetClient();

        // Act
        var response = await client.PutAsJsonAsync<string?>(
            UrlPackage.WillVersion("whatever"),
            null
        );

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task PutWillVersion_WhenNotExists_ShouldReturnNotFound()
    {
        // Arrange
        var client = await GetCookieTokenClientAsync(Guid.NewGuid().ToString());
        var userId = (await client.GetUserInfoAsync()).Id;
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);
        var version = WillSeeder.SeedWillVersion(w => w.WillId = will.Id);
        var request = WillSeeder.SeedWillVersionRequest();

        // Act
        var response = await client.PutAsJsonAsync(UrlPackage.WillVersion(version.Id), request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task PutWillVersion_WhenExists_ShouldSuccess()
    {
        // Arrange
        var client = await GetCookieTokenClientAsync(Guid.NewGuid().ToString());
        var userId = (await client.GetUserInfoAsync()).Id;
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);
        var version = WillSeeder.SeedWillVersion(w => w.WillId = will.Id);
        await Sut.CreateWillVersionAsync(version);
        var request = WillSeeder.SeedWillVersionRequest();

        // Act
        var response = await client.PutAsJsonAsync(UrlPackage.WillVersion(version.Id), request);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<WillVersionResponse>();
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Value, Is.EqualTo(request.Value));
    }

    [Test]
    public async Task DeleteWillVersion_WhenUnauthorized_ShouldFail()
    {
        // Arrange
        var client = GetClient();

        // Act
        var response = await client.DeleteAsync(UrlPackage.WillVersion("whatever"));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task DeleteWillVersion_WhenAuthorized_ShouldSuccess()
    {
        // Arrange
        var client = await GetCookieTokenClientAsync(Guid.NewGuid().ToString());
        var userId = (await client.GetUserInfoAsync()).Id;
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);
        var version = WillSeeder.SeedWillVersion(w => w.WillId = will.Id);
        await Sut.CreateWillVersionAsync(version);

        // Act
        var response = await client.DeleteAsync(UrlPackage.WillVersion(version.Id));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        var result = await Sut.ReadWillVersionIdAsync(version.Id);
        Assert.That(result, Is.Null);
    }
    #endregion
}
