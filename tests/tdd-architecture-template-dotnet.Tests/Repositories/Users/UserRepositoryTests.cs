using Microsoft.EntityFrameworkCore;
using tdd_architecture_template_dotnet.Domain.Entities.Users;
using tdd_architecture_template_dotnet.Infrastructure.Data.Context;
using tdd_architecture_template_dotnet.Infrastructure.Repositories.Users;

namespace tdd_architecture_template_dotnet.Tests.Repositories.Users
{
    public class UserRepositoryTests
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
        public async Task GetAll_ShouldReturnAllUsers()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);
            {
                var address = new UserAddress { Street = "Rua das Flores", Number = 123, Complement = "", Neighborhood = "Test", City = "São Paulo", State = "SP", Country = "Brazil", ZipCode = "01000-000", UserId = 1 };
                var contact = new UserContact { Email = "test@test.com", PhoneNumber = "+00 (00) 00000-0000", UserId = 1 };

                await context.Users.AddAsync(new User { Id = 1, FirstName = "User", LastName = "Test", Document = "00.000.000/0000-00", Address = address, Contact = contact });
                await context.SaveChangesAsync();
            }

            // Act
            var result = await repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.True(result.Any() || !context.Users.Any());
        }

        [Fact]
        public async Task GetById_ShouldReturnUsers()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);

            var address = new UserAddress { Street = "Rua das Flores", Number = 123, Complement = "", Neighborhood = "Test", City = "São Paulo", State = "SP", Country = "Brazil", ZipCode = "01000-000", UserId = 1 };
            var contact = new UserContact { Email = "test@test.com", PhoneNumber = "+00 (00) 00000-0000", UserId = 1 };
            
            var user = new User { Id = 1, FirstName = "User", LastName = "Test", Document = "00.000.000/0000-00", Address = address, Contact = contact };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetById(user.Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Post_ShouldCreateUsers()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);

            var address = new UserAddress { Street = "Rua das Flores", Number = 123, Complement = "", Neighborhood = "Test", City = "São Paulo", State = "SP", Country = "Brazil", ZipCode = "01000-000" };
            var contact = new UserContact { Email = "test@test.com", PhoneNumber = "+00 (00) 00000-0000" };

            var user = new User { FirstName = "User", LastName = "Test", Document = "00.000.000/0000-00", Address = address, Contact = contact };

            // Act
            var result = await repository.Post(user);

            // Assert
            Assert.NotNull(result.CreationDate);
            Assert.Contains(await context.Users.ToListAsync(), s => s.Id == result.Id);
        }

        [Fact]
        public async Task Put_ShouldUpdateUsers()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);

            var user = new User { Id = 1, FirstName = "User", LastName = "Test", Document = "00.000.000/0000-00" };
            user.FirstName = "User";

            // Act
            var result = await repository.Put(user);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_ShouldRemoveUsers()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserRepository(context);

            var address = new UserAddress { Street = "Rua das Flores", Number = 123, Complement = "", Neighborhood = "Test", City = "São Paulo", State = "SP", Country = "Brazil", ZipCode = "01000-000", UserId = 1 };
            var contact = new UserContact { Email = "test@test.com", PhoneNumber = "+00 (00) 00000-0000", UserId = 1 };

            var user = new User { Id = 1, FirstName = "User", LastName = "Test", Document = "00.000.000/0000-00", Address = address, Contact = contact };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            await repository.Delete(user);

            // Act
            var deletedUsers = await context.Users.FindAsync(user.Id);

            // Assert

            Assert.Null(deletedUsers);
        }
    }
}
