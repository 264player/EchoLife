using EchoLife.Tests.Integration.Utils;
using EchoLife.Tests.Integration.Will.Utils;
using EchoLife.Will.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EchoLife.Tests.Integration.Will.Data
{
    internal class SqlLiteOfficiousWillRepositoryTests : SqlLiteTestsBase<WillDbContext>
    {
        private readonly SqlLiteOfficiousWillRepository Sut;

        public SqlLiteOfficiousWillRepositoryTests()
        {
            _databasePath = $"EchoLifeTest_{Guid.NewGuid()}.db";
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
            _dbContext!.Wills.RemoveRange(_dbContext.Wills);
        }

        [Test]
        public async Task CreateAsync_WhenValidEntity_ShouldReturnEntity()
        {
            // Arrange
            var will = WillSeeder.SeedOfficiousWill();

            // Act
            var result = await Sut.CreateAsync(will);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(will.Id));
        }

        [Test]
        public async Task CreateAsync_WhenWillAlreadyExists_ShouldFail()
        {
            // Arrange
            var will = WillSeeder.SeedOfficiousWill();
            await Sut.CreateAsync(will);

            // Act
            // Assert
            Assert.ThrowsAsync<DbUpdateException>(async () => await Sut.CreateAsync(will));
        }

        [Test]
        public async Task ReadAsync_WhenEntityExists_ShouldReturnEntity()
        {
            // Arrange
            var will = WillSeeder.SeedOfficiousWill();
            await Sut.CreateAsync(will);

            // Act
            var result = await Sut.ReadAsync(will.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(will.Id));
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
            var will = WillSeeder.SeedOfficiousWill();
            await Sut.CreateAsync(will);

            // Act
            will.VersionId = Guid.NewGuid().ToString();
            var result = await Sut.UpdateAsync(will);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(will.Id));
                Assert.That(result.VersionId, Is.EqualTo(will.VersionId));
            });
        }

        [Test]
        public async Task UpdateAsync_WhenEntityDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var will = WillSeeder.SeedOfficiousWill();

            // Act
            var result = await Sut.UpdateAsync(will);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WhenEntityExists_ShouldReturnTrue()
        {
            // Arrange
            var will = WillSeeder.SeedOfficiousWill();
            await Sut.CreateAsync(will);

            // Act
            var result = await Sut.DeleteAsync(will.Id);

            // Assert
            Assert.That(result, Is.True);
            var deletedWill = await Sut.ReadAsync(will.Id);
            Assert.That(deletedWill, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WhenEntityDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            // Act
            var result = await Sut.DeleteAsync(Guid.NewGuid().ToString());

            // Assert
            Assert.IsFalse(result);
        }
    }
}
