using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EchoLife.Tests.Integration.Utils
{
    public class SqlLiteTestsBase
    {
        protected string? _databasePath;
        protected DbContext? _dbContext;

        [OneTimeSetUp]
        public void SetUpDatabase()
        {
            _databasePath = Path.Combine(Path.GetTempPath(), _databasePath);
            if (File.Exists(_databasePath))
            {
                File.Delete(_databasePath);
            }

            _dbContext.Database.EnsureCreated();

            TestContext.WriteLine($"✅ SQLite 数据库已创建: {_databasePath}");
        }

        [OneTimeTearDown]
        public void CleanupDatabase()
        {
            _dbContext.Dispose();

            if (File.Exists(_databasePath))
            {
                File.Delete(_databasePath);
                TestContext.WriteLine($"❌ SQLite 数据库已删除: {_databasePath}");
            }
        }
    }
}
