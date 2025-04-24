using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EchoLife.Will.Models;
using static System.Guid;

namespace EchoLife.Tests.Integration.Will.Utils
{
    internal static class WillSeeder
    {
        public static OfficiousWill SeedOfficiousWill(
            string? id = null,
            string? willType = null,
            string? contentId = null,
            string? testaorId = null
        )
        {
            return new OfficiousWill
            {
                Id = id ?? NewGuid().ToString(),
                WillType = willType ?? NewGuid().ToString(),
                VersionId = contentId ?? NewGuid().ToString(),
                TestaorId = testaorId ?? NewGuid().ToString(),
            };
        }

        public static WillVersion SeedWillVersion(
            string? id = null,
            string? willId = null,
            string? content = null,
            string? willType = null,
            DateTime? dateTime = null
        )
        {
            return new WillVersion
            {
                Id = id ?? NewGuid().ToString(),
                WillId = willId ?? NewGuid().ToString(),
                Content = content ?? NewGuid().ToString(),
                WillType = willType ?? NewGuid().ToString(),
                CreatedAt = dateTime ?? DateTime.UtcNow,
            };
        }
    }
}
