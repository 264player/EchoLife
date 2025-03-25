using EchoLife.User.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EchoLife.Tests.Integration.Utils
{
    internal class ControllerTestsBase
    {
        private TestWebAppFactory _factory;

        [OneTimeSetUp]
        public void IniWebAppFactory()
        {
            _factory = new TestWebAppFactory();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _factory.Dispose();
        }

        protected HttpClient GetClient()
        {
            return _factory.CreateClient();
        }

        protected HttpClient GetAuthenticationClient(string userId, string role = "user")
        {
            var result = _factory.CreateClient();
            using var scope = _factory.Services.CreateScope();

            var identityService = scope.ServiceProvider.GetRequiredService<IIdentityUserService>();
            result.DefaultRequestHeaders.Add(
                "Authorization",
                $"Bearer {identityService.GenerateToken(userId, role)}"
            );
            return result;
        }

        protected HttpClient GetCookieTokenClient(string userId, string role = "user")
        {
            var result = _factory.CreateClient();
            using var scope = _factory.Services.CreateScope();

            return result;
        }
    }
}
