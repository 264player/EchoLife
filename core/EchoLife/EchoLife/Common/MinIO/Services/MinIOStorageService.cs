using Minio;
using Minio.DataModel.Args;

namespace EchoLife.Common.MinIO.Services;

public class MinIOStorageService(IMinioClient _minio) : IStorageService
{
    #region Bucket
    public async Task MakeBucketAsync(string bucketName)
    {
        await _minio
            .MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName))
            .ConfigureAwait(false);
    }

    public async Task<List<string>> ListBucketsAsync()
    {
        var list = await _minio.ListBucketsAsync().ConfigureAwait(false);
        return [.. list.Buckets.Select(b => b.Name)];
    }

    public async Task EnsureBucketExistsAsync(string bucketName)
    {
        var args = new BucketExistsArgs().WithBucket(bucketName);
        var found = await _minio.BucketExistsAsync(args).ConfigureAwait(false);
        if (!found)
        {
            await MakeBucketAsync(bucketName).ConfigureAwait(false);
        }
    }

    public async Task RemoveBucketAsync(string bucketName)
    {
        await _minio
            .RemoveBucketAsync(new RemoveBucketArgs().WithBucket(bucketName))
            .ConfigureAwait(false);
    }

    #endregion

    #region Objects
    public async Task<string> PutObjectAsync(string bucketName, string objectName)
    {
        var args = new PresignedPutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithExpiry(1000);
        var presignedUrl = await _minio.PresignedPutObjectAsync(args).ConfigureAwait(false);
        return presignedUrl;
    }

    public async Task<string> PresignedGetObjectAsync(string bucketName, string objectName)
    {
        var args = new PresignedGetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName)
            .WithExpiry(1000);
        return await _minio.PresignedGetObjectAsync(args).ConfigureAwait(false);
    }

    public async Task RemoveObjectAsync(string bucketName, string objectName)
    {
        var args = new RemoveObjectArgs().WithBucket(bucketName).WithObject(objectName);
        await _minio.RemoveObjectAsync(args).ConfigureAwait(false);
    }

    public async Task<bool> StateObjectAsync(string bucketName, string objectName)
    {
        try
        {
            var objectStatArgs = new StatObjectArgs().WithBucket(bucketName).WithObject(objectName);
            var statObject = await _minio.StatObjectAsync(objectStatArgs).ConfigureAwait(false);
            return true;
        }
        catch (Minio.Exceptions.ObjectNotFoundException)
        {
            return false;
        }
    }
    #endregion
}
