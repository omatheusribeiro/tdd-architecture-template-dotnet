using Microsoft.EntityFrameworkCore;
using tdd_architecture_template_dotnet.Domain.Entities.Users;
using tdd_architecture_template_dotnet.Infrastructure.Data.Context;
using tdd_architecture_template_dotnet.Infrastructure.Repositories.Users;

namespace tdd_architecture_template_dotnet.Tests.Repositories.Users
{
    public class UserAddressRepositoryTests
    {
        private ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllUserAddresses()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserAddresRepository(context);
            {
                await context.UserAddresses.AddAsync(new UserAddress { Street = "Rua das Flores", Number = 123, Complement = "", Neighborhood = "Test", City = "São Paulo", State = "SP", Country = "Brazil", ZipCode = "01000-000" });
                await context.SaveChangesAsync();
            }

            // Act
            var result = await repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserAddress>>(result);
            Assert.True(result.Any() || !context.UserAddresses.Any());
        }

        [Fact]
        public async Task GetById_ShouldReturnUserAddress()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserAddresRepository(context);

            var userAddress = new UserAddress { Street = "Rua das Flores", Number = 123, Complement = "", Neighborhood = "Test", City = "São Paulo", State = "SP", Country = "Brazil", ZipCode = "01000-000" };
            await context.UserAddresses.AddAsync(userAddress);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetById(userAddress.Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Post_ShouldCreateUserAddress()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserAddresRepository(context);

            var userAddress = new UserAddress { Street = "Rua das Flores", Number = 123, Complement = "", Neighborhood = "Test", City = "São Paulo", State = "SP", Country = "Brazil", ZipCode = "01000-000" };

            // Act
            var result = await repository.Post(userAddress);

            // Assert
            Assert.NotNull(result.CreationDate);
            Assert.Contains(await context.UserAddresses.ToListAsync(), s => s.Id == result.Id);
        }

        [Fact]
        public async Task Put_ShouldUpdateUserAddress()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserAddresRepository(context);

            var userAddress = new UserAddress { Id = 1, Street = "Rua das Flores", Number = 123, Complement = "", Neighborhood = "Test", City = "São Paulo", State = "SP", Country = "Brazil", ZipCode = "01000-000" };
            userAddress.Street = "Rua das Flores";

            // Act
            var result = await repository.Put(userAddress);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_ShouldRemoveUserAddress()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserAddresRepository(context);

            var userAddress = new UserAddress { Street = "Rua das Flores", Number = 123, Complement = "", Neighborhood = "Test", City = "São Paulo", State = "SP", Country = "Brazil", ZipCode = "01000-000" };
            await context.UserAddresses.AddAsync(userAddress);
            await context.SaveChangesAsync();
            await repository.Delete(userAddress);

            // Act
            var deletedUserAddress = await context.UserAddresses.FindAsync(userAddress.Id);

            // Assert

            Assert.Null(deletedUserAddress);
        }
    }
}
