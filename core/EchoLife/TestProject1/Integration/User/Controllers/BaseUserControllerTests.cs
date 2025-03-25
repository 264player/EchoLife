using System.Net;
using System.Net.Http.Json;
using EchoLife.Tests.Integration.User.Utils;
using EchoLife.Tests.Integration.Utils;
using EchoLife.User.Dtos;

namespace EchoLife.Tests.Integration.User.Controllers
{
    internal class BaseUserControllerTests : ControllerTestsBase
    {
        [Test]
        public async Task Register_WhenBaseUserNotExsit_ShouldSuccess()
        {
            // Arrange
            var baseUser = UserSeeder.SeedLoginOrRegisterRequest();
            using var httpClient = base.GetClient();

            // Act
            var response = await httpClient.PostAsync(
                UrlPackage.Register(),
                JsonContent.Create(baseUser)
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.RequestMessage, Is.Not.Null);
            });
        }

        [Test]
        public async Task Register_WhenBaseUserAlreadyExsit_ShouldFail()
        {
            // Arrange
            var baseUser = UserSeeder.SeedLoginOrRegisterRequest();
            using var httpClient = base.GetClient();
            await httpClient.PostAsync(UrlPackage.Register(), JsonContent.Create(baseUser));

            // Act
            var response = await httpClient.PostAsync(
                UrlPackage.Register(),
                JsonContent.Create(baseUser)
            );

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Login_WhenBaseUserNotExsit_ShouldFail()
        {
            // Arrange
            var baseUser = UserSeeder.SeedLoginOrRegisterRequest();
            using var httpClient = base.GetClient();

            // Act
            var response = await httpClient.PostAsync(
                UrlPackage.Login(),
                JsonContent.Create(baseUser)
            );

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Login_WhenBaseUserAlreadyExsit_ShouldSuccess()
        {
            // Arrange
            var baseUser = UserSeeder.SeedLoginOrRegisterRequest();
            using var httpClient = base.GetClient();
            await httpClient.PostAsync(UrlPackage.Register(), JsonContent.Create(baseUser));

            // Act
            var response = await httpClient.PostAsync(
                UrlPackage.Login(),
                JsonContent.Create(baseUser)
            );

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.RequestMessage, Is.Not.Null);
        }

        [Test]
        public async Task Get_WhenUnAuthentication_ShouldFail()
        {
            // Arrange
            using var httpClient = base.GetClient();

            // Act
            var response = await httpClient.GetAsync(UrlPackage.Me());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Get_WhenBaseUserAlreadyExsit_ShouldSuccess()
        {
            // Arrange
            var baseUser = UserSeeder.SeedLoginOrRegisterRequest();
            using var httpClient1 = base.GetClient();
            var registerResponse = await httpClient1.PostAsync(
                UrlPackage.Register(),
                JsonContent.Create(
                    new LoginOrRegisterRequest
                    {
                        Username = baseUser.Username,
                        Password = baseUser.Password,
                    }
                )
            );
            var id = await registerResponse.Content.ReadFromJsonAsync<LoginOrRegisterResponse>();
            using var httpClient2 = base.GetAuthenticationClient(id!.Id);

            // Act
            var response = await httpClient2.GetAsync(UrlPackage.Me());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var baseUserResponse = await response.Content.ReadFromJsonAsync<BaseUserResponse>();
            Assert.That(baseUserResponse, Is.Not.Null);
        }

        [Test]
        public async Task Delete_WhenUnAuthentication_ShouldFail()
        {
            // Arrange
            using var httpClient = base.GetClient();

            // Act
            var response = await httpClient.DeleteAsync(UrlPackage.Me());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Delete_WhenBaseUserAlreadyExsit_ShouldSuccess()
        {
            // Arrange
            var baseUser = UserSeeder.SeedLoginOrRegisterRequest();
            using var httpClient1 = base.GetClient();
            var registerResponse = await httpClient1.PostAsync(
                UrlPackage.Register(),
                JsonContent.Create(
                    new LoginOrRegisterRequest
                    {
                        Username = baseUser.Username,
                        Password = baseUser.Password,
                    }
                )
            );
            var id = await registerResponse.Content.ReadFromJsonAsync<LoginOrRegisterResponse>();
            using var httpClient2 = base.GetAuthenticationClient(id!.Id);

            // Act
            var response = await httpClient2.DeleteAsync(UrlPackage.Me());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public async Task Put_WhenUnAuthentication_ShouldFail()
        {
            // Arrange
            using var httpClient = base.GetClient();

            // Act
            var response = await httpClient.PutAsync(
                UrlPackage.Me(),
                JsonContent.Create("whatever")
            );

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Put_WhenBaseUserAlreadyExsit_ShouldSuccess()
        {
            // Arrange
            var baseUser = UserSeeder.SeedLoginOrRegisterRequest();
            using var httpClient1 = base.GetClient();
            var registerResponse = await httpClient1.PostAsync(
                UrlPackage.Register(),
                JsonContent.Create(
                    new LoginOrRegisterRequest
                    {
                        Username = baseUser.Username,
                        Password = baseUser.Password,
                    }
                )
            );
            var id = await registerResponse.Content.ReadFromJsonAsync<LoginOrRegisterResponse>();
            var request = UserSeeder.SeedBaseUserRequest();
            using var httpClient2 = base.GetAuthenticationClient(id!.Id);

            // Act
            var response = await httpClient2.PutAsync(UrlPackage.Me(), JsonContent.Create(request));

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var baseUserResponse = await response.Content.ReadFromJsonAsync<BaseUserResponse>();
            Assert.That(baseUserResponse, Is.Not.Null);
            Assert.That(baseUserResponse.UserName, Is.EqualTo(request.Username));
            Assert.That(baseUserResponse.NickName, Is.EqualTo(request.NickName));
        }
    }
}
