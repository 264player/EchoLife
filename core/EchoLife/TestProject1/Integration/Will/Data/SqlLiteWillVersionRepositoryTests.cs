using EchoLife.Tests.Integration.Utils;
using EchoLife.Tests.Integration.Will.Utils;
using EchoLife.Will.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EchoLife.Tests.Integration.Will.Data
{
    internal class SqlLiteWillVersionRepositoryTests : SqlLiteTestsBase<WillDbContext>
    {
        private readonly SqlLiteWillVersionRepository Sut;

        public SqlLiteWillVersionRepositoryTests()
        {
            _databasePath = $"EchoLifeTest{Guid.NewGuid()}.db";
            var connectionString = $"Data Source={_databasePath}";

            _dbContext = new WillDbContext(
                new DbContextOptionsBuilder<WillDbContext>().UseSqlite(connectionString).Options,
                Options.Create(
                    new WillDbContextSettings
                    {
                        WillTableName = "OfficiousWills",
                        WillVersionTableName = "WillVersions",
                        SqlLiteConnectionString = connectionString,
                    }
                )
            );
            Sut = new(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.WillVersions.RemoveRange(_dbContext.WillVersions);
        }

        [Test]
        public async Task CreateAsync_WhenValidEntity_ShouldReturnEntity()
        {
            // Arrange
            var version = WillSeeder.SeedWillVersion();

            // Act
            var result = await Sut.CreateAsync(version);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(version.Id));
        }

        [Test]
        public async Task CreateAsync_WhenWillAlreadyExists_ShouldFail()
        {
            // Arrange
            var version = WillSeeder.SeedWillVersion();
            await Sut.CreateAsync(version);

            // Act
            // Assert
            Assert.ThrowsAsync<DbUpdateException>(async () => await Sut.CreateAsync(version));
        }

        [Test]
        public async Task ReadAsync_WhenEntityExists_ShouldReturnEntity()
        {
            // Arrange
            var version = WillSeeder.SeedWillVersion();
            await Sut.CreateAsync(version);

            // Act
            var result = await Sut.ReadAsync(version.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(version.Id));
        }

        [Test]
        public async Task ReadAsync_WhenEntityDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            // Act
            var result = await Sut.ReadAsync(Guid.NewGuid().ToString());

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task UpdateAsync_WhenEntityExists_ShouldReturnUpdatedEntity()
        {
            // Arrange
            var version = WillSeeder.SeedWillVersion();
            await Sut.CreateAsync(version);

            // Act
            version.Content = Guid.NewGuid().ToString();
            var result = await Sut.UpdateAsync(version);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(version.Id));
                Assert.That(result.Content, Is.EqualTo(version.Content));
            });
        }

        [Test]
        public async Task UpdateAsync_WhenEntityDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var version = WillSeeder.SeedWillVersion();

            // Act
            var result = await Sut.UpdateAsync(version);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WhenEntityExists_ShouldReturnTrue()
        {
            // Arrange
            var version = WillSeeder.SeedWillVersion();
            await Sut.CreateAsync(version);

            // Act
            var result = await Sut.DeleteAsync(version.Id);

            // Assert
            Assert.That(result, Is.True);
            var deletedWill = await Sut.ReadAsync(version.Id);
            Assert.That(deletedWill, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WhenEntityDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            // Act
            var result = await Sut.DeleteAsync(Guid.NewGuid().ToString());

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteAllVersionsByWillIdAsync_WhenEntitiesAlreadyExist_ShouldReturnTrue()
        {
            // Arrange
            int count = 3;
            var willId = Guid.NewGuid().ToString();
            for (int i = 0; i < count; i++)
                await Sut.CreateAsync(WillSeeder.SeedWillVersion(w => w.WillId = willId));

            // Act
            var result = await Sut.DeleteAllVersionsByWillIdAsync(willId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task DeleteAllVersionsByWillIdAsync_WhenEntitiesNotExist_ShouldReturnFalse()
        {
            // Arrange
            var willId = Guid.NewGuid().ToString();

            // Act
            var result = await Sut.DeleteAllVersionsByWillIdAsync(willId);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}
