namespace EchoLife.Common.MinIO.Services;

public interface IStorageService
{
    #region Bucket
    Task MakeBucketAsync(string bucketName);
    Task<List<string>> ListBucketsAsync();
    Task EnsureBucketExistsAsync(string bucketName);
    Task RemoveBucketAsync(string bucketName);
    #endregion

    #region Objects
    Task<string> PutObjectAsync(string bucketName, string objectName);
    Task<IEnumerable<string>> ListObjectsAsync(
        string bucketName,
        string prefix,
        bool recursive = true,
        bool versions = false
    );
    Task<string> PresignedGetObjectAsync(string bucketName, string objectName);
    Task RemoveObjectAsync(string bucketName, string objectName);
    Task<bool> StateObjectAsync(string bucketName, string objectName);
    #endregion
}
