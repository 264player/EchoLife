using EchoLife.Tests.Integration.User.Utils;
using EchoLife.Tests.Integration.Utils;
using EchoLife.User.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EchoLife.Tests.Integration.User.Data
{
    [TestFixture]
    public class SqlLiteBaseUserRepositoryTests : SqlLiteTestsBase<UserDbContext>
    {
        private readonly SqlLiteBaseUserRepository sqlLiteBaseUserRepository;

        public SqlLiteBaseUserRepositoryTests()
        {
            _databasePath = $"{Guid.NewGuid()}-users.db";
            var connectionString = $"Data Source={_databasePath}";

            _dbContext = new UserDbContext(
                new DbContextOptionsBuilder<UserDbContext>().UseSqlite(connectionString).Options,
                Options.Create(
                    new UserDbContextSettings
                    {
                        BaseUserTableName = "BaseUsers",
                        SqlLiteConnectionString = connectionString,
                    }
                )
            );

            sqlLiteBaseUserRepository = new(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.BaseUsers.RemoveRange(_dbContext.BaseUsers);
        }

        [Test]
        public async Task CreateBaseUser_WhenBaseUserNotExist_ShouldSuccess()
        {
            // Arrange
            var baseUser = UserSeeder.SeedBaseUser();

            // Act
            var result = await sqlLiteBaseUserRepository.CreateAsync(baseUser);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(baseUser.Id));
        }

        [Test]
        public async Task CreateBaseUser_WhenBaseUserAlreadyExist_ShouldThrowDbUpdateException()
        {
            // Arrange
            var baseUser = UserSeeder.SeedBaseUser();
            await sqlLiteBaseUserRepository.CreateAsync(baseUser);

            // Act
            // Assert
            Assert.ThrowsAsync<DbUpdateException>(
                () => sqlLiteBaseUserRepository.CreateAsync(baseUser)
            );
        }

        [Test]
        public async Task ReadBaseUser_WhenBaseUserNotExist_ShouldReturnNull()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();

            // Act
            var result = await sqlLiteBaseUserRepository.ReadAsync(userId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task ReadBaseUser_WhenBaseUserAlreadyExist_ShouldSuccess()
        {
            // Arrange
            var baseUser = UserSeeder.SeedBaseUser();
            await sqlLiteBaseUserRepository.CreateAsync(baseUser);

            // Act
            var result = await sqlLiteBaseUserRepository.ReadAsync(baseUser.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task ReadBaseUserByUsername_WhenBaseUserNotExist_ShouldReturnNull()
        {
            // Arrange
            var baseUser = UserSeeder.SeedBaseUser();

            // Act
            var result = await sqlLiteBaseUserRepository.ReadByUsernameAsync(baseUser.Username);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task ReadBaseUserByUsername_WhenBaseUserAlreadyExist_ShouldSuccess()
        {
            // Arrange
            var baseUser = UserSeeder.SeedBaseUser();
            await sqlLiteBaseUserRepository.CreateAsync(baseUser);

            // Act
            var result = await sqlLiteBaseUserRepository.ReadByUsernameAsync(baseUser.Username);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(baseUser.Id));
        }

        [Test]
        public async Task UpdateBaseUser_WhenBaseUserNotExist_ShouldReturnNull()
        {
            // Arrange
            var baseUser = UserSeeder.SeedBaseUser();

            // Act
            var result = await sqlLiteBaseUserRepository.UpdateAsync(baseUser);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task UpdateBaseUser_WhenBaseUserAlreadyExist_ShouldSuccess()
        {
            // Arrange
            var baseUser = UserSeeder.SeedBaseUser();
            await sqlLiteBaseUserRepository.CreateAsync(baseUser);

            // Act
            var result = await sqlLiteBaseUserRepository.UpdateAsync(baseUser);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(baseUser.Id));
        }

        [Test]
        public async Task UpdateBaseUser_WhenModifyBaseUser_ShouldSuccess()
        {
            // Arrange
            var baseUser = UserSeeder.SeedBaseUser();
            await sqlLiteBaseUserRepository.CreateAsync(baseUser);
            baseUser.NickName = "Jax";

            // Act
            var result = await sqlLiteBaseUserRepository.UpdateAsync(baseUser);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(baseUser.Id));
        }

        [Test]
        public async Task DeleteBaseUser_WhenBaseUserNotExist_ShouldFail()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();

            // Act
            var result = await sqlLiteBaseUserRepository.DeleteAsync(userId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task DeleteBaseUser_WhenBaseUserAlreadyExist_ShouldSuccess()
        {
            // Arrange
            var baseUser = UserSeeder.SeedBaseUser();
            await sqlLiteBaseUserRepository.CreateAsync(baseUser);

            // Act
            var result = await sqlLiteBaseUserRepository.DeleteAsync(baseUser.Id);

            // Assert
            Assert.That(result, Is.True);
        }
    }
}
