using System.Security.Claims;
using EchoLife.Account.Services;
using EchoLife.Common;
using EchoLife.Common.Exceptions;
using EchoLife.Will.Data;
using EchoLife.Will.Dtos;
using EchoLife.Will.Models;

namespace EchoLife.Will.Services
{
    public class WillService(
        IWillVersionRepository _willVersionRepository,
        IOfficiousWillRepository _officiousWillRepository
    ) : IWillService
    {
        public async Task<string> CreateWillAsync(ClaimsPrincipal user, WillRequest willRequest)
        {
            var userId = ClaimsManager.EnsureGetUserId(user);

            var firstVersionId = IdGenerator.GenerateUlid();
            var will = new OfficiousWill
            {
                Id = IdGenerator.GenerateUlid(),
                TestaorId = userId,
                Name = willRequest.Name,
                WillType = WillType.SELF_WRITTEN_WILL.ToString(),
                ContentId = firstVersionId,
            };

            await _willVersionRepository.CreateAsync(
                new WillVersion
                {
                    Id = firstVersionId,
                    WillId = will.Id,
                    WillType = "self",
                    Content = "",
                }
            );

            will = await _officiousWillRepository.CreateAsync(will) ?? throw new UnknowException();

            return will.Id;
        }

        public async Task<string> CreateWillVersionsAsync(
            ClaimsPrincipal user,
            string willId,
            WillVersionRequest willVersionRequest,
            bool isDraft
        )
        {
            var userId = ClaimsManager.EnsureGetUserId(user);

            var will = await EnsureGetWill(willId, userId);

            var version =
                await _willVersionRepository.CreateAsync(
                    new WillVersion
                    {
                        Id = IdGenerator.GenerateUlid(),
                        WillId = willId,
                        Content = willVersionRequest.Value,
                        WillType = willVersionRequest.WillType,
                    }
                ) ?? throw new UnknowException();

            if (!isDraft)
            {
                will.ContentId = version.Id;
                await _officiousWillRepository.UpdateAsync(will);
            }

            return version.Id;
        }

        public async Task DeleteWillAsync(ClaimsPrincipal user, string willId)
        {
            var userId = ClaimsManager.EnsureGetUserId(user);

            await EnsureGetWill(willId, userId);

            await _officiousWillRepository.DeleteAsync(willId);
            await _willVersionRepository.DeleteAllVersionsByWillIdAsync(willId);
        }

        public async Task DeleteWillVersionAsync(ClaimsPrincipal user, string versionId)
        {
            var userId = ClaimsManager.EnsureGetUserId(user);

            var version = await EnsureGetWillVersionAsync(versionId);

            await EnsureGetWill(version.WillId, userId);

            await _willVersionRepository.DeleteAsync(versionId);
        }

        public async Task<WillResponse> GetMyWillAsync(ClaimsPrincipal user, string willId)
        {
            var userId = ClaimsManager.EnsureGetUserId(user);

            var will = await EnsureGetWill(willId, userId);

            return WillResponse.From(will);
        }

        public async Task<List<WillResponse>> GetMyWillsAsync(
            ClaimsPrincipal user,
            int count,
            string? cursorId
        )
        {
            var userId = ClaimsManager.EnsureGetUserId(user);

            var result = await _officiousWillRepository.ReadAsync(userId, cursorId, count);

            return [.. result.Select(WillResponse.From)];
        }

        public async Task<List<WillVersionResponse>> GetMyWillVersionsAsync(
            ClaimsPrincipal user,
            string willId,
            int count,
            string? cursorId
        )
        {
            var userId = ClaimsManager.EnsureGetUserId(user);

            await EnsureGetWill(willId, userId);

            var result = await _willVersionRepository.ReadAsync(willId, count, cursorId);

            return [.. result.Select(WillVersionResponse.From)];
        }

        public async Task<string> UpdateWillAsync(
            ClaimsPrincipal user,
            string willId,
            string versionId,
            string name
        )
        {
            var userId = ClaimsManager.EnsureGetUserId(user);

            var will = await EnsureGetWill(willId, userId);

            will.ContentId = versionId;
            will.Name = name;
            await _officiousWillRepository.UpdateAsync(will);
            return willId;
        }

        public async Task<string> UpdateWillVersionAsync(
            ClaimsPrincipal user,
            string versionId,
            WillVersionRequest willVersionRequest
        )
        {
            var userId = ClaimsManager.EnsureGetUserId(user);

            var version = await EnsureGetWillVersionAsync(versionId);
            await EnsureGetWill(version.WillId, userId);

            await _willVersionRepository.UpdateAsync(Update(version, willVersionRequest));

            return versionId;

            WillVersion Update(WillVersion willVersion, WillVersionRequest request)
            {
                willVersion.WillType = request.WillType;
                willVersion.Content = request.Value;
                return willVersion;
            }
        }

        protected async Task<OfficiousWill> EnsureGetWill(string willId)
        {
            var will =
                await _officiousWillRepository.ReadAsync(willId)
                ?? throw new ResourceNotFoundException();
            return will;
        }

        protected async Task<OfficiousWill> EnsureGetWill(string willId, string testaorId)
        {
            var will = await EnsureGetWill(willId);
            if (will.TestaorId != testaorId)
            {
                throw new ForbiddenException();
            }
            return will;
        }

        protected async Task<WillVersion> EnsureGetWillVersionAsync(string willVersionId)
        {
            var version =
                await _willVersionRepository.ReadAsync(willVersionId)
                ?? throw new ResourceNotFoundException();
            return version;
        }
    }
}
