using Microsoft.EntityFrameworkCore;
using tdd_architecture_template_dotnet.Domain.Entities.Sales;
using tdd_architecture_template_dotnet.Infrastructure.Data.Context;
using tdd_architecture_template_dotnet.Infrastructure.Repositories.Sales;

namespace tdd_architecture_template_dotnet.Tests.Repositories.Sales
{
    public class SaleRepositoryTests
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
        public async Task GetById_ShouldReturnSale()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new SaleRepository(context);

            var sale = new Sale { TotalValue = 10, UserId = 1, ProductId = 1 };
            await context.Sales.AddAsync(sale);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetById(sale.Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Post_ShouldCreateSale()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new SaleRepository(context);

            var sale = new Sale { TotalValue = 10, UserId = 1, ProductId = 1 };

            // Act
            var result = await repository.Post(sale);

            // Assert
            Assert.NotNull(result.CreationDate);
            Assert.Contains(await context.Sales.ToListAsync(), s => s.Id == result.Id);
        }


        [Fact]
        public async Task Put_ShouldUpdateSale()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new SaleRepository(context);

            var sale = new Sale { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 };

            sale.TotalValue = 20;

            // Act
            var result = await repository.Put(sale);

            // Assert
            Assert.NotNull(result.ChangeDate);
            Assert.Equal(20, (await context.Sales.FindAsync(1)).TotalValue);
        }

        [Fact]
        public async Task Delete_ShouldRemoveSale()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new SaleRepository(context);

            var sale = new Sale { TotalValue = 10, UserId = 1, ProductId = 1 };
            await context.Sales.AddAsync(sale);
            await context.SaveChangesAsync();

            await repository.Delete(sale);

            // Act
            var deletedSale = await context.Sales.FindAsync(sale.Id);

            // Assert
            Assert.Null(deletedSale);
        }

    }
}