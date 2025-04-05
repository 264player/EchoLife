using System.Security.Claims;
using EchoLife.Will.Dtos;

namespace EchoLife.Will.Services
{
    public interface IWillService
    {
        Task<string> CreateWillAsync(ClaimsPrincipal user);
        Task<string> CreateWillVersionsAsync(
            ClaimsPrincipal user,
            string willId,
            WillVersionRequest willVersionRequest,
            bool isDraft
        );
        Task<List<WillResponse>> GetMyWillsAsync(ClaimsPrincipal user, int count, string? cursorId);
        Task<List<WillVersionResponse>> GetMyWillVersionsAsync(
            ClaimsPrincipal user,
            string willId,
            int count,
            string? cursorId
        );
        Task<string> UpdateWillAsync(ClaimsPrincipal user, string willId, string versionId);
        Task<string> UpdateWillVersionAsync(
            ClaimsPrincipal user,
            string willId,
            string versionId,
            string conten
        );
        Task DeleteWillAsync(ClaimsPrincipal user, string willId);
        Task DeleteWillVersionAsync(ClaimsPrincipal user, string willId, string versionId);
    }
}
