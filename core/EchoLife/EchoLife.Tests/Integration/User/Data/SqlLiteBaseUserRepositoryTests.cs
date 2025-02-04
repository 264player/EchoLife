using EchoLife.Tests.Integration.Utils;
using EchoLife.User.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace EchoLife.Tests.Integration.User.Data
{
    [TestFixture]
    public class SqlLiteBaseUserRepositoryTests : SqlLiteTestsBase
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _databasePath = "users.db";
            var connectionString = $"Data Source={_databasePath}";

            _dbContext = new BaseUserDbContext(
                new DbContextOptionsBuilder<BaseUserDbContext>()
                    .UseSqlite(connectionString)
                    .Options,
                Options.Create(
                    new BaseUserDbContextSettings
                    {
                        BaseUserTableName = "BaseUsers",
                        SqlLiteConnectionString = connectionString,
                    }
                )
            );
            SetUpDatabase();
        }

        [SetUp]
        public void SetUp()
        {
            Assert.Pass();
        }

        [Test]
        public void sample()
        {
            Assert.Pass();
        }
    }
}
