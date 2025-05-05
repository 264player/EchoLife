using EchoLife.Common.Exceptions;
using EchoLife.Common.MinIO.Data;
using EchoLife.Common.MinIO.Services;
using Minio.AspNetCore;

namespace EchoLife.Common.MinIO.Setup;

public static class MinIOExtensions
{
    public static IServiceCollection AddMinIOStorage(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var minIOSettings =
            configuration.GetSection("MinIOStorage").Get<MinIOSettings>()
            ?? throw new ResourceNotFoundException(
                "settings are not found",
                "MinIOStoragr settings are not found."
            );

        services.AddMinio(options =>
        {
            options.SecretKey = minIOSettings.SecretKey;
            options.AccessKey = minIOSettings.AccessKey;
            options.Endpoint = minIOSettings.EndPoint;
        });

        services.AddScoped<IStorageService, MinIOStorageService>();

        return services;
    }
}
