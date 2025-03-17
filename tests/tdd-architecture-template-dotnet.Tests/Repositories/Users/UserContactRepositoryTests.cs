using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tdd_architecture_template_dotnet.Domain.Entities.Users;
using tdd_architecture_template_dotnet.Infrastructure.Data.Context;
using tdd_architecture_template_dotnet.Infrastructure.Repositories.Users;

namespace tdd_architecture_template_dotnet.Tests.Repositories.Users
{
    public class UserContactRepositoryTests
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
        public async Task GetAll_ShouldReturnAllUserContacts()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserContactRepository(context);
            {
                await context.UserContacts.AddAsync(new UserContact { Email = "test@test.com", PhoneNumber = "+00 (00) 00000-0000", UserId = 1 });
                await context.SaveChangesAsync();
            }

            // Act
            var result = await repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserContact>>(result);
            Assert.True(result.Any() || !context.UserContacts.Any());
        }

        [Fact]
        public async Task GetById_ShouldReturnUserContacts()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserContactRepository(context);

            var userContacts = new UserContact { Email = "test@test.com", PhoneNumber = "+00 (00) 00000-0000", UserId = 1 };
            await context.UserContacts.AddAsync(userContacts);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetById(userContacts.Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Post_ShouldCreateUserContacts()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserContactRepository(context);

            var userContacts = new UserContact { Email = "test@test.com", PhoneNumber = "+00 (00) 00000-0000", UserId = 1 };

            // Act
            var result = await repository.Post(userContacts);

            // Assert
            Assert.NotNull(result.CreationDate);
            Assert.Contains(await context.UserContacts.ToListAsync(), s => s.Id == result.Id);
        }

        [Fact]
        public async Task Put_ShouldUpdateUserContacts()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserContactRepository(context);

            var userContacts = new UserContact { Email = "test@test.com", PhoneNumber = "+00 (00) 00000-0000", UserId = 1 };
            userContacts.Email = "test@test.com";

            // Act
            var result = await repository.Put(userContacts);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_ShouldRemoveUserContacts()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new UserContactRepository(context);

            var userContacts = new UserContact { Email = "test@test.com", PhoneNumber = "+00 (00) 00000-0000", UserId = 1 };
            await context.UserContacts.AddAsync(userContacts);
            await context.SaveChangesAsync();
            await repository.Delete(userContacts);

            // Act
            var deletedUserContacts = await context.UserContacts.FindAsync(userContacts.Id);

            // Assert

            Assert.Null(deletedUserContacts);
        }
    }
}
