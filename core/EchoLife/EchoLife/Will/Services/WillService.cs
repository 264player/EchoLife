using EchoLife.Will.Data;
using EchoLife.Will.Models;

namespace EchoLife.Will.Services
{
    public class WillService(
        IWillVersionRepository _willVersionRepository,
        IOfficiousWillRepository _officiousWillRepository
    ) : IWillService
    {
        public async Task<string> CreateWillAsync()
        {
            var will = new OfficiousWill
            {
                Id = Guid.NewGuid().ToString(),
                TestaorId = "",
                WillType = "",
            };
            var version = new WillVersion
            {
                Id = Guid.NewGuid().ToString(),
                WillId = will.Id,
                WillType = will.WillType,
            };
            will.ContentId = version.Id;
            await _officiousWillRepository.CreateAsync(will);
            await _willVersionRepository.CreateAsync(version);
            return will.Id;
        }

        public async Task DeleteWillAsync(string willId)
        {
            await _officiousWillRepository.DeleteAsync(willId);
            await _willVersionRepository.DeleteAllVersionsByWillIdAsync(willId);
        }

        public async Task DeleteWillVersionAsync(string versionId)
        {
            await _willVersionRepository.DeleteAsync(versionId);
        }

        public async Task GetWillAsync(string willId)
        {
            await _officiousWillRepository.ReadAsync(willId);
        }

        public async Task GetWillVresionAsync(string versionId)
        {
            await _willVersionRepository.ReadAsync(versionId);
        }

        public async Task<string> UpdateWillAsync(string willId, string versionId)
        {
            await _officiousWillRepository.UpdateAsync(
                new OfficiousWill { Id = willId, ContentId = versionId }
            );
            return willId;
        }

        public async Task<string> UpdateWillVersionAsync(string versionId, string conten)
        {
            await _willVersionRepository.UpdateAsync(
                new WillVersion { Id = versionId, Content = conten }
            );
            return versionId;
        }
    }
}
