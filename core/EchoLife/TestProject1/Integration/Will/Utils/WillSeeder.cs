using EchoLife.Common;
using EchoLife.Tests.Integration.Utils;
using EchoLife.Will.Dtos;
using EchoLife.Will.Models;
using static System.Guid;

namespace EchoLife.Tests.Integration.Will.Utils
{
    internal static class WillSeeder
    {
        public static OfficiousWill SeedOfficiousWill(Action<OfficiousWill>? will = null)
        {
            return will.Modify(
                new OfficiousWill
                {
                    Id = IdGenerator.GenerateUlid(),
                    Name = NewGuid().ToString(),
                    WillType = NewGuid().ToString(),
                    VersionId = NewGuid().ToString(),
                    TestaorId = NewGuid().ToString(),
                }
            );
        }

        public static WillVersion SeedWillVersion(Action<WillVersion>? willVersion = null)
        {
            return willVersion.Modify(
                new WillVersion
                {
                    Id = IdGenerator.GenerateUlid(),
                    Content = NewGuid().ToString(),
                    WillId = NewGuid().ToString(),
                    WillType = NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                }
            );
        }

        public static WillRequest SeedWillRequest(string? name = null)
        {
            return new WillRequest(name ?? NewGuid().ToString());
        }

        public static WillVersionRequest SeedWillVersionRequest(
            string? willType = null,
            string? value = null
        )
        {
            return new WillVersionRequest(
                willType ?? NewGuid().ToString(),
                value ?? NewGuid().ToString()
            );
        }

        public static WillReview SeedWillReview(Action<WillReview>? willReview = null)
        {
            return willReview.Modify(
                new WillReview
                {
                    Id = IdGenerator.GenerateUlid(),
                    Status = WillReviewStatus.Pending,
                    Comments = null,
                    CreatedAt = DateTime.UtcNow,
                    UserId = IdGenerator.GenerateGuid(),
                    VersionId = IdGenerator.GenerateUlid(),
                    ReviewerId = IdGenerator.GenerateGuid(),
                    ReviewedAt = DateTime.UtcNow,
                }
            );
        }
    }
}
