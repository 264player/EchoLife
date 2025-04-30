using EchoLife.Tests.Integration.Utils;
using EchoLife.Will.Data;
using EchoLife.Will.Models;
using Microsoft.Extensions.DependencyInjection;

namespace EchoLife.Tests.Integration.Will.Controller.Utils;

internal static class WillHelperExtensions
{
    public static async Task<OfficiousWill?> CreateWillAsync(
        this TestWebAppFactory sut,
        OfficiousWill will
    )
    {
        using var scope = sut.Services.CreateAsyncScope();
        var repo = scope.ServiceProvider.GetRequiredService<IOfficiousWillRepository>();
        return await repo.CreateAsync(will);
    }

    public static async Task<OfficiousWill?> ReadWillAsync(
        this TestWebAppFactory sut,
        string willId
    )
    {
        using var scope = sut.Services.CreateAsyncScope();
        var repo = scope.ServiceProvider.GetRequiredService<IOfficiousWillRepository>();
        return await repo.ReadAsync(willId);
    }

    public static async Task<WillVersion?> CreateWillVersionAsync(
        this TestWebAppFactory sut,
        WillVersion version
    )
    {
        using var scope = sut.Services.CreateAsyncScope();
        var repo = scope.ServiceProvider.GetRequiredService<IWillVersionRepository>();
        return await repo.CreateAsync(version);
    }

    public static async Task<WillVersion?> ReadWillVersionIdAsync(
        this TestWebAppFactory sut,
        string versionId
    )
    {
        using var scope = sut.Services.CreateAsyncScope();
        var repo = scope.ServiceProvider.GetRequiredService<IWillVersionRepository>();
        return await repo.ReadAsync(versionId);
    }
}
