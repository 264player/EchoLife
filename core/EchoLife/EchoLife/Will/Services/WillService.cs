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
        IOfficiousWillRepository _officiousWillRepository,
        IWillReviewRepository _willReviewRepository
    ) : IWillService
    {
        #region Will
        public async Task<WillResponse> CreateWillAsync(
            ClaimsPrincipal user,
            WillRequest willRequest
        )
        {
            var userId = ClaimsManager.GetAuthorizedUserId(user);

            var firstVersionId = IdGenerator.GenerateUlid();
            var will = new OfficiousWill
            {
                Id = IdGenerator.GenerateUlid(),
                TestaorId = userId,
                Name = willRequest.Name,
                WillType = WillType.SelfWritten,
                VersionId = firstVersionId,
            };

            await _willVersionRepository.CreateAsync(
                new WillVersion
                {
                    Id = firstVersionId,
                    WillId = will.Id,
                    WillType = WillType.SelfWritten,
                    Content = "",
                }
            );

            will =
                await _officiousWillRepository.CreateAsync(will)
                ?? throw new UnknowException("Create fail.", "");

            return WillResponse.From(will);
        }

        public async Task<WillResponse> GetMyWillAsync(ClaimsPrincipal user, string willId)
        {
            var userId = ClaimsManager.GetAuthorizedUserId(user);

            var will = await EnsureAndGetWillAsync(willId, userId);

            return WillResponse.From(will);
        }

        public async Task<List<WillResponse>> GetMyWillsAsync(
            ClaimsPrincipal user,
            int count,
            string? cursorId
        )
        {
            var userId = ClaimsManager.GetAuthorizedUserId(user);

            var result = await _officiousWillRepository.ReadAsync(
                w => w.TestaorId == userId && (cursorId == null || w.Id.CompareTo(cursorId) < 0),
                count
            );

            return [.. result.Select(WillResponse.From)];
        }

        public async Task DeleteWillAsync(ClaimsPrincipal user, string willId)
        {
            var userId = ClaimsManager.GetAuthorizedUserId(user);

            await EnsureAndGetWillAsync(willId, userId);

            await _willVersionRepository.DeleteAllVersionsByWillIdAsync(willId);
            await _officiousWillRepository.DeleteAsync(willId);
        }

        public async Task<WillResponse> UpdateWillAsync(
            ClaimsPrincipal user,
            string willId,
            string versionId,
            string name,
            WillType willType
        )
        {
            var userId = ClaimsManager.GetAuthorizedUserId(user);

            var will = await EnsureAndGetWillAsync(willId, userId);

            will.VersionId = versionId;
            will.Name = name;
            will.WillType = willType;
            var updatedWill =
                await _officiousWillRepository.UpdateAsync(will) ?? throw new UnknowException();
            return WillResponse.From(updatedWill);
        }
        #endregion

        #region Will Version


        public async Task<WillVersionResponse> CreateWillVersionsAsync(
            ClaimsPrincipal user,
            string willId,
            WillVersionRequest willVersionRequest,
            bool isDraft
        )
        {
            var userId = ClaimsManager.GetAuthorizedUserId(user);

            var will = await EnsureAndGetWillAsync(willId, userId);

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
                will.VersionId = version.Id;
                await _officiousWillRepository.UpdateAsync(will);
            }

            return WillVersionResponse.From(version);
        }

        public async Task<List<WillVersionResponse>> GetMyWillVersionsAsync(
            ClaimsPrincipal user,
            string willId,
            int count,
            string? cursorId
        )
        {
            var userId = ClaimsManager.GetAuthorizedUserId(user);

            await EnsureAndGetWillAsync(willId, userId);

            var result = await _willVersionRepository.ReadAsync(willId, count, cursorId);

            return [.. result.Select(WillVersionResponse.From)];
        }

        public async Task<List<WillVersionResponse>> GetWillVersionsAsync(
            IEnumerable<string> versionIds
        )
        {
            return
            [
                .. (await _willVersionRepository.ReadAsync(versionIds)).Select(
                    WillVersionResponse.From
                ),
            ];
        }

        public async Task<WillVersionResponse> GetWillVersionAsync(string versionId)
        {
            return WillVersionResponse.From(
                await _willVersionRepository.ReadAsync(versionId)
                    ?? throw new WillVersionNotFoundException(versionId)
            );
        }

        public async Task<WillVersionResponse> UpdateWillVersionAsync(
            ClaimsPrincipal user,
            string versionId,
            WillVersionRequest willVersionRequest
        )
        {
            var userId = ClaimsManager.GetAuthorizedUserId(user);

            var version = await EnsureGetWillVersionAsync(versionId);
            await EnsureAndGetWillAsync(version.WillId, userId);

            await AllowModification(versionId);

            var updatedVersino =
                await _willVersionRepository.UpdateAsync(Update(version, willVersionRequest))
                ?? throw new UnknowException();

            return WillVersionResponse.From(updatedVersino);

            static WillVersion Update(WillVersion willVersion, WillVersionRequest request)
            {
                willVersion.WillType = request.WillType;
                willVersion.Content = request.Value;
                willVersion.UpdatedAt = DateTime.UtcNow;
                return willVersion;
            }
        }

        public async Task DeleteWillVersionAsync(ClaimsPrincipal user, string versionId)
        {
            var userId = ClaimsManager.GetAuthorizedUserId(user);
            try
            {
                var version = await EnsureGetWillVersionAsync(versionId);

                await AllowModification(versionId);

                await EnsureAndGetWillAsync(version.WillId, userId);
            }
            catch (ResourceNotFoundException) { }

            await _willVersionRepository.DeleteAsync(versionId);
        }

        #endregion
        protected async Task<OfficiousWill> EnsureGetWill(string willId)
        {
            var will =
                await _officiousWillRepository.ReadAsync(willId)
                ?? throw new WillNotFoundException(willId);
            return will;
        }

        protected async Task<OfficiousWill> EnsureAndGetWillAsync(string willId, string userId)
        {
            var will = await EnsureGetWill(willId);
            if (will.TestaorId != userId)
            {
                throw new ForbiddenException(userId);
            }
            return will;
        }

        protected async Task<WillVersion> EnsureGetWillVersionAsync(string willVersionId)
        {
            var version =
                await _willVersionRepository.ReadAsync(willVersionId)
                ?? throw new WillVersionNotFoundException(willVersionId);
            return version;
        }

        protected async Task AllowModification(string willVersionId)
        {
            var result = await _willReviewRepository
                .ReadAsync(r => r.VersionId == willVersionId, 1)
                .ConfigureAwait(false);
            if (result.Count != 0)
            {
                throw new EntityArgumentException(
                    "Operation is not allowed.",
                    "This version is under review, operation is not allowed."
                );
            }
        }
    }
}
