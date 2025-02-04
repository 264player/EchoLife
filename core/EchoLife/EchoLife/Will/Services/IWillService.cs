namespace EchoLife.Will.Services
{
    public interface IWillService
    {
        Task<string> CreateWillAsync();

        /// <summary>
        /// Get the full will, the will include all will-versions.
        /// </summary>
        /// <param name="willId"></param>
        /// <returns></returns>
        Task GetWillAsync(string willId);
        Task GetWillVresionAsync(string versionId);
        Task<string> UpdateWillAsync(string willId, string versionId);
        Task<string> UpdateWillVersionAsync(string versionId, string conten);
        Task DeleteWillAsync(string willId);
        Task DeleteWillVersionAsync(string versionId);
    }
}
