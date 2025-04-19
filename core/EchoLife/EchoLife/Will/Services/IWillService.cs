using System.Security.Claims;
using EchoLife.Will.Dtos;

namespace EchoLife.Will.Services
{
    public interface IWillService
    {
        #region Will
        Task<string> CreateWillAsync(ClaimsPrincipal user, WillRequest willRequest);
        Task<List<WillResponse>> GetMyWillsAsync(ClaimsPrincipal user, int count, string? cursorId);
        Task<WillResponse> GetMyWillAsync(ClaimsPrincipal user, string willId);
        Task<string> UpdateWillAsync(
            ClaimsPrincipal user,
            string willId,
            string versionId,
            string name
        );
        Task DeleteWillAsync(ClaimsPrincipal user, string willId);
        Task<string> CreateWillVersionsAsync(
            ClaimsPrincipal user,
            string willId,
            WillVersionRequest willVersionRequest,
            bool isDraft
        );
        #endregion
        #region Will Version
        Task<List<WillVersionResponse>> GetMyWillVersionsAsync(
            ClaimsPrincipal user,
            string willId,
            int count,
            string? cursorId
        );
        Task<string> UpdateWillVersionAsync(
            ClaimsPrincipal user,
            string versionId,
            WillVersionRequest willVersionRequest
        );
        Task DeleteWillVersionAsync(ClaimsPrincipal user, string versionId);
        #endregion
    }
}
