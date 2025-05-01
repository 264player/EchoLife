using System.Net.Http.Json;
using EchoLife.Account.Dtos;
using EchoLife.Account.Models;
using EchoLife.Account.Services;
using EchoLife.Tests.Integration.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EchoLife.Tests.Integration.Account.Controller.Utils;

internal static class AccountHelperExtensions
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

    public static async Task<IdentityResult> SudoCreateAccountAsync(
        this TestWebAppFactory sut,
        IdentityAccount account,
        string password
    )
    {
        using var scope = sut.Services.CreateAsyncScope();
        var service = scope.ServiceProvider.GetRequiredService<IAccountService>();
        var result = await service.SudoCreateUserAsync(account, password);
        return result;
    }

    public static async Task<IdentityAccountResponse> GetUserInfoAsync(this HttpClient client)
    {
        var response = await client.GetAsync(UrlPackage.UserInfo());
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<IdentityAccountResponse>();
        Assert.That(result, Is.Not.Null);
        return result;
    }

    public static async Task AddRoleToUserAsync(
        this TestWebAppFactory sut,
        string userId,
        AccountRoles accountRoles
    )
    {
        using var scope = sut.Services.CreateAsyncScope();
        var service = scope.ServiceProvider.GetRequiredService<IAccountService>();
        await service.AddRoleToUserAsync(userId, accountRoles);
    }
}
