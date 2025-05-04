using System.Security.Claims;
using EchoLife.Will.Dtos;
using EchoLife.Will.Models;

namespace EchoLife.Will.Services
{
    public interface IWillService
    {
        #region Will
        Task<WillResponse> CreateWillAsync(ClaimsPrincipal user, WillRequest willRequest);
        Task<List<WillResponse>> GetMyWillsAsync(ClaimsPrincipal user, int count, string? cursorId);
        Task<WillResponse> GetMyWillAsync(ClaimsPrincipal user, string willId);
        Task<WillResponse> UpdateWillAsync(
            ClaimsPrincipal user,
            string willId,
            string versionId,
            string name,
            WillType willType
        );
        Task DeleteWillAsync(ClaimsPrincipal user, string willId);
        #endregion

        #region Will Version
        Task<WillVersionResponse> CreateWillVersionsAsync(
            ClaimsPrincipal user,
            string willId,
            WillVersionRequest willVersionRequest,
            bool isDraft
        );
        Task<List<WillVersionResponse>> GetMyWillVersionsAsync(
            ClaimsPrincipal user,
            string willId,
            int count,
            string? cursorId
        );
        Task<List<WillVersionResponse>> GetWillVersionsAsync(IEnumerable<string> versionIds);
        Task<WillVersionResponse> GetWillVersionAsync(string versionId);
        Task<WillVersionResponse> UpdateWillVersionAsync(
            ClaimsPrincipal user,
            string versionId,
            WillVersionRequest willVersionRequest
        );
        Task DeleteWillVersionAsync(ClaimsPrincipal user, string versionId);
        #endregion
    }
}
