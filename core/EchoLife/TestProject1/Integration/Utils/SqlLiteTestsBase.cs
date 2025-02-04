using Microsoft.EntityFrameworkCore;

namespace EchoLife.Tests.Integration.Utils
{
    public class SqlLiteTestsBase<TDbContext>
        where TDbContext : DbContext
    {
        protected string? _databasePath;
        protected TDbContext? _dbContext;

        [OneTimeSetUp]
        public void SetUpDatabase()
        {
            string testDbFolder = Path.Combine(Directory.GetCurrentDirectory(), "TestDb");
            if (!Directory.Exists(testDbFolder))
            {
                Directory.CreateDirectory(testDbFolder);
            }
            _databasePath = Path.Combine(testDbFolder, _databasePath!);

            if (File.Exists(_databasePath))
            {
                File.Delete(_databasePath);
            }
            File.Create(_databasePath);
            _dbContext!.Database.EnsureCreatedAsync().Wait();

            TestContext.WriteLine($"✅ SQLite 数据库已创建: {_databasePath}");
        }

        [OneTimeTearDown]
        public void CleanupDatabase()
        {
            _dbContext!.Database.EnsureDeletedAsync().Wait();
            _dbContext!.Dispose();

            if (File.Exists(_databasePath))
            {
                File.Delete(_databasePath);
                TestContext.WriteLine($"❌ SQLite 数据库已删除: {_databasePath}");
            }
        }
    }
}
