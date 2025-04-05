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
        public async Task<string> CreateWillAsync(ClaimsPrincipal user)
        {
            var userId = ClaimsManager.EnsureGetUserId(user);

            var will = new OfficiousWill
            {
                Id = IdGenerator.GenerateGuid(),
                TestaorId = userId,
                WillType = WillType.SELF_WRITTEN_WILL.ToString(),
            };

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
                        Id = IdGenerator.GenerateGuid(),
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

        public async Task DeleteWillVersionAsync(
            ClaimsPrincipal user,
            string willId,
            string versionId
        )
        {
            var userId = ClaimsManager.EnsureGetUserId(user);

            await EnsureGetWill(willId, userId);

            await _willVersionRepository.DeleteAsync(versionId);
        }

        public async Task<List<WillResponse>> GetMyWillsAsync(
            ClaimsPrincipal user,
            int count,
            string? cursorId
        )
        {
            var userId = ClaimsManager.EnsureGetUserId(user);

            var result = await _officiousWillRepository.ReadAsync(
                w => w.TestaorId == userId,
                cursorId,
                count
            );

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

            var result = await _willVersionRepository.ReadAsync(
                v => v.WillId == willId,
                cursorId,
                count
            );

            return [.. result.Select(WillVersionResponse.From)];
        }

        public async Task<string> UpdateWillAsync(
            ClaimsPrincipal user,
            string willId,
            string versionId
        )
        {
            var userId = ClaimsManager.EnsureGetUserId(user);

            var will = await EnsureGetWill(willId, userId);

            will.ContentId = versionId;
            await _officiousWillRepository.UpdateAsync(will);
            return willId;
        }

        public async Task<string> UpdateWillVersionAsync(
            ClaimsPrincipal user,
            string willId,
            string versionId,
            string conten
        )
        {
            var userId = ClaimsManager.EnsureGetUserId(user);

            await EnsureGetWill(willId, userId);
            await _willVersionRepository.UpdateAsync(
                new WillVersion { Id = versionId, Content = conten }
            );
            return versionId;
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
    }
}
