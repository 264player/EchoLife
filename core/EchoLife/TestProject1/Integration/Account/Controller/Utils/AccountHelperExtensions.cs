using EchoLife.Account.Dtos;
using EchoLife.Account.Services;
using EchoLife.Tests.Integration.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EchoLife.Tests.Integration.Account.Controller.Utils;

public static class AccountHelperExtensions
{
    public static async Task<IdentityResult> RegisterAsync(
        this TestWebAppFactory sut,
        RegisterRequest request
    )
    {
        using var scope = sut.Services.CreateAsyncScope();
        var service = scope.ServiceProvider.GetRequiredService<IAccountService>();
        var result = await service.RegisterAsync(request);
        return result;
    }
}
