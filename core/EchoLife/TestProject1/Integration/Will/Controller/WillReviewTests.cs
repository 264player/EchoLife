using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using EchoLife.Account.Models;
using EchoLife.Common;
using EchoLife.Tests.Integration.Utils;
using EchoLife.Tests.Integration.Will.Controller.Utils;
using EchoLife.Tests.Integration.Will.Utils;
using EchoLife.Will.Dtos;
using EchoLife.Will.Models;

namespace EchoLife.Tests.Integration.Will.Controller;

internal class WillReviewTests : ControllerTestsBase
{
    #region Will Review
    [Test]
    public async Task PostWillReview_WhenUnauthoried_ShouldFail()
    {
        // Arrange
        var client = GetClient();

        // Act
        var response = await client.PostAsJsonAsync(UrlPackage.HumanReview("whatever"), "");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task PostWillReview_WhenAuthoried_ShouldSuccess()
    {
        // Arrange
        var userId = IdGenerator.GenerateUlid();
        var client = await GetCookieTokenClientAsync(userId, AccountRoles.User);
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);
        var version = WillSeeder.SeedWillVersion(w => w.WillId = will.Id);
        await Sut.CreateWillVersionAsync(version);

        // Act
        var response = await client.PostAsJsonAsync(UrlPackage.HumanReview(version.Id), "");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() },
        };
        var result = await response.Content.ReadFromJsonAsync<WillReviewResponse>(options);
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.WillVersion.Id, Is.EqualTo(version.Id));
            Assert.That(result.Status, Is.EqualTo(WillReviewStatus.Pending));
        });
    }

    [Test]
    public async Task PostWillReview_WhenVersionDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var userId = IdGenerator.GenerateUlid();
        var client = await GetCookieTokenClientAsync(userId, AccountRoles.User);
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);

        // Act
        var response = await client.PostAsJsonAsync(UrlPackage.HumanReview("whatever"), "");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetWillReview_WhenUnauthorized_ShouldFail()
    {
        // Arrange
        var client = GetClient();

        // Act
        var response = await client.GetAsync(UrlPackage.Review("whatever"));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task GetWillReview_WhenNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var userId = IdGenerator.GenerateUlid();
        var client = await GetCookieTokenClientAsync(userId, AccountRoles.Reviewer);

        // Act
        var response = await client.GetAsync(UrlPackage.Review(IdGenerator.GenerateUlid()));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task GetWillReview_WhenExist_ShouldSuccess()
    {
        // Arrange
        var userId = IdGenerator.GenerateUlid();
        var client = await GetCookieTokenClientAsync(userId, AccountRoles.Reviewer);
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);
        var version = WillSeeder.SeedWillVersion(w => w.WillId = will.Id);
        await Sut.CreateWillVersionAsync(version);
        var review = WillSeeder.SeedWillReview(w =>
        {
            w.UserId = userId;
            w.VersionId = version.Id;
        });
        await Sut.CreateWillReviewAsync(review);

        // Act
        var response = await client.GetAsync(UrlPackage.Review(review.Id));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        var result = await response.Content.ReadFromJsonAsync<WillReviewResponse>(
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() },
            }
        );
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(review.Id));
            Assert.That(result.WillVersion.Id, Is.EqualTo(review.VersionId));
        });
    }

    [Test]
    public async Task GetMyWillReview_WhenUnauthorized_ShouldFail()
    {
        // Arrange
        var client = GetClient();

        // Act
        var response = await client.GetAsync(UrlPackage.MyReviews(2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task GetMyWillReview_WhenNotExist_ShouldReturnEmpty()
    {
        // Arrange
        var userId = IdGenerator.GenerateUlid();
        var client = await GetCookieTokenClientAsync(userId, AccountRoles.Reviewer);
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);
        var version = WillSeeder.SeedWillVersion(w => w.WillId = will.Id);
        await Sut.CreateWillVersionAsync(version);
        var review = WillSeeder.SeedWillReview(w =>
        {
            w.UserId = userId;
            w.VersionId = version.Id;
        });

        // Act
        var response = await client.GetAsync(UrlPackage.MyReviews(2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<List<WillReviewResponse>>(
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() },
            }
        );
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetMyWillReview_WhenExists_ShouldReturnList()
    {
        // Arrange
        var userId = IdGenerator.GenerateUlid();
        var client = await GetCookieTokenClientAsync(userId, AccountRoles.Reviewer);
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);
        var version = WillSeeder.SeedWillVersion(w => w.WillId = will.Id);
        await Sut.CreateWillVersionAsync(version);
        var review = WillSeeder.SeedWillReview(w =>
        {
            w.UserId = userId;
            w.ReviewerId = userId;
            w.VersionId = version.Id;
        });
        await Sut.CreateWillReviewAsync(review);

        // Act
        var response = await client.GetAsync(UrlPackage.MyReviews(2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<List<WillVersionResponse>>(
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() },
            }
        );
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.GreaterThanOrEqualTo(1));
        Assert.That(result.Any(x => x.Id == review.Id), Is.True);
    }

    [Test]
    public async Task GetMyWillReviewRequets_WhenUnauthorized_ShouldFail()
    {
        // Arrange
        var client = GetClient();

        // Act
        var response = await client.GetAsync(UrlPackage.MyReviewRequests(2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task GetMyWillReviewRequests_WhenNotExist_ShouldReturnEmpty()
    {
        // Arrange
        var userId = IdGenerator.GenerateUlid();
        var client = await GetCookieTokenClientAsync(userId);
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);
        var version = WillSeeder.SeedWillVersion(w => w.WillId = will.Id);
        await Sut.CreateWillVersionAsync(version);
        var review = WillSeeder.SeedWillReview(w =>
        {
            w.UserId = userId;
            w.VersionId = version.Id;
        });

        // Act
        var response = await client.GetAsync(UrlPackage.MyReviewRequests(2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<List<WillReviewResponse>>(
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() },
            }
        );
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetMyWillReviewRequests_WhenExists_ShouldReturnList()
    {
        // Arrange
        var userId = IdGenerator.GenerateUlid();
        var client = await GetCookieTokenClientAsync(userId);
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);
        var version = WillSeeder.SeedWillVersion(w => w.WillId = will.Id);
        await Sut.CreateWillVersionAsync(version);
        var review = WillSeeder.SeedWillReview(w =>
        {
            w.UserId = userId;
            w.VersionId = version.Id;
        });
        await Sut.CreateWillReviewAsync(review);

        // Act
        var response = await client.GetAsync(UrlPackage.MyReviewRequests(2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<List<WillVersionResponse>>(
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() },
            }
        );
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result.Any(x => x.Id == review.Id), Is.True);
    }

    [Test]
    public async Task GetAllWillReviewRequests_WhenUnauthorized_ShouldFail()
    {
        // Arrange
        var client = GetClient();

        // Act
        var response = await client.GetAsync(UrlPackage.AllReviewRequests(2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task GetAllWillReviewRequests_WhenNotExist_ShouldReturnEmpty()
    {
        // Arrange
        var userId = IdGenerator.GenerateUlid();
        var client = await GetCookieTokenClientAsync(userId, AccountRoles.Reviewer);
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);
        var version = WillSeeder.SeedWillVersion(w => w.WillId = will.Id);
        await Sut.CreateWillVersionAsync(version);
        var review = WillSeeder.SeedWillReview(w =>
        {
            w.UserId = userId;
            w.VersionId = version.Id;
        });

        // Act
        var response = await client.GetAsync(UrlPackage.AllReviewRequests(2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<List<WillReviewResponse>>(
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() },
            }
        );
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Select(x => x.Id), Is.Not.Contain(review.Id));
    }

    [Test]
    public async Task GetAllWillReviewRequests_WhenExists_ShouldReturnList()
    {
        // Arrange
        var userId = IdGenerator.GenerateUlid();
        var client = await GetCookieTokenClientAsync(userId, AccountRoles.Reviewer);
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);
        var version = WillSeeder.SeedWillVersion(w => w.WillId = will.Id);
        await Sut.CreateWillVersionAsync(version);
        var review = WillSeeder.SeedWillReview(w =>
        {
            w.UserId = userId;
            w.VersionId = version.Id;
        });
        await Sut.CreateWillReviewAsync(review);

        // Act
        var response = await client.GetAsync(UrlPackage.AllReviewRequests(2, null));

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await response.Content.ReadFromJsonAsync<List<WillVersionResponse>>(
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() },
            }
        );
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.GreaterThanOrEqualTo(1));
        Assert.That(result.Any(x => x.Id == review.Id), Is.True);
    }

    [Test]
    public async Task ProcessReview_WhenUnauthorized_ShouldFail()
    {
        // Arrange
        var client = GetClient();

        // Act
        var response = await client.PutAsJsonAsync<string?>(
            UrlPackage.ProcessReview("whatever"),
            null
        );

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task ProcessReview_WhenNotExists_ShouldReturnNotFound()
    {
        // Arrange
        var userId = IdGenerator.GenerateUlid();
        var client = await GetCookieTokenClientAsync(userId, AccountRoles.Reviewer);
        var review = WillSeeder.SeedWillReview();

        // Act
        var response = await client.PutAsJsonAsync(UrlPackage.ProcessReview(review.Id), "");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task ProcessReview_WhenExists_ShouldSuccess()
    {
        // Arrange
        var userId = IdGenerator.GenerateUlid();
        var client = await GetCookieTokenClientAsync(userId, AccountRoles.Reviewer);
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);
        var version = WillSeeder.SeedWillVersion(w => w.WillId = will.Id);
        await Sut.CreateWillVersionAsync(version);
        var review = WillSeeder.SeedWillReview(w =>
        {
            w.UserId = userId;
            w.VersionId = version.Id;
        });
        await Sut.CreateWillReviewAsync(review);

        // Act
        var response = await client.PutAsJsonAsync(UrlPackage.ProcessReview(review.Id), "");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await Sut.ReadWillReviewAsync(review.Id);
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(WillReviewStatus.InProgress));
            Assert.That(result.Id, Is.EqualTo(review.Id));
        });
    }

    [Test]
    public async Task CompleteReview_WhenUnauthorized_ShouldFail()
    {
        // Arrange
        var client = GetClient();

        // Act
        var response = await client.PutAsJsonAsync(
            UrlPackage.CompleteReview("whatever"),
            WillReviewStatus.Approved
        );

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
    }

    [Test]
    public async Task CompleteReview_WhenNotExists_ShouldReturnNotFound()
    {
        // Arrange
        var userId = IdGenerator.GenerateUlid();
        var client = await GetCookieTokenClientAsync(userId, AccountRoles.Reviewer);

        // Act
        var response = await client.PutAsJsonAsync(
            UrlPackage.CompleteReview(IdGenerator.GenerateUlid()),
            new CompleteWillReviewRequest
            {
                Status = WillReviewStatus.Approved,
                Comment = IdGenerator.GenerateGuid(),
            }
        );

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task CompleteReview_WhenExists_ShouldSuccess()
    {
        // Arrange
        var userId = IdGenerator.GenerateUlid();
        var client = await GetCookieTokenClientAsync(userId, AccountRoles.Reviewer);
        var will = WillSeeder.SeedOfficiousWill(w => w.TestaorId = userId);
        await Sut.CreateWillAsync(will);
        var version = WillSeeder.SeedWillVersion(w => w.WillId = will.Id);
        await Sut.CreateWillVersionAsync(version);
        var review = WillSeeder.SeedWillReview(w =>
        {
            w.UserId = userId;
            w.ReviewerId = userId;
            w.VersionId = version.Id;
            w.Status = WillReviewStatus.InProgress;
        });
        await Sut.CreateWillReviewAsync(review);

        // Act
        var response = await client.PutAsJsonAsync(
            UrlPackage.CompleteReview(review.Id),
            new CompleteWillReviewRequest
            {
                Status = WillReviewStatus.Approved,
                Comment = IdGenerator.GenerateGuid(),
            }
        );

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var result = await Sut.ReadWillReviewAsync(review.Id);
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(WillReviewStatus.Approved));
            Assert.That(result.Id, Is.EqualTo(review.Id));
        });
    }
    #endregion
}
