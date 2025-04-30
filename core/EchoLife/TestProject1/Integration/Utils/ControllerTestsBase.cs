using System.Net;
using System.Net.Http.Json;
using EchoLife.Account.Dtos;
using EchoLife.Tests.Integration.Account.Controller.Utils;
using EchoLife.Tests.Integration.Account.Utils;
using EchoLife.User.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EchoLife.Tests.Integration.Utils
{
    internal class ControllerTestsBase
    {
        protected TestWebAppFactory Sut;

        [OneTimeSetUp]
        public void IniWebAppFactory()
        {
            Sut = new TestWebAppFactory();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            Sut.Dispose();
        }

        protected HttpClient GetClient()
        {
            return Sut.CreateClient();
        }

        protected HttpClient GetAuthenticationClient(string userId, string role = "user")
        {
            var result = Sut.CreateClient();
            using var scope = Sut.Services.CreateScope();

            var identityService = scope.ServiceProvider.GetRequiredService<IIdentityUserService>();
            result.DefaultRequestHeaders.Add(
                "Authorization",
                $"Bearer {identityService.GenerateToken(userId, role)}"
            );
            return result;
        }

        protected async Task<HttpClient> GetCookieTokenClientAsync(RegisterRequest registerRequest)
        {
            var result = Sut.CreateClient();
            using var scope = Sut.Services.CreateScope();

            await Sut.RegisterAsync(registerRequest);
            var loginRequest = AccountSeeder.SeedLoginRequest(
                registerRequest.Username,
                registerRequest.Password
            );

            var response = await result.PostAsJsonAsync(UrlPackage.Login(), loginRequest);

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response.Headers.Contains("Set-Cookie"), Is.True);
            });

            if (response.Headers.TryGetValues("Set-Cookie", out var values))
            {
                var cookies = values.Select(v => v.Split(';')[0]).ToList();

                var cookieHeader = string.Join("; ", cookies);

                result.DefaultRequestHeaders.Add("Cookie", cookieHeader);
            }

            return result;
        }
    }
}
