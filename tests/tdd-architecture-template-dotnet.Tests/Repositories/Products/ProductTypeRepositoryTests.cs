using Microsoft.EntityFrameworkCore;
using tdd_architecture_template_dotnet.Domain.Entities.Products;
using tdd_architecture_template_dotnet.Infrastructure.Data.Context;
using tdd_architecture_template_dotnet.Infrastructure.Repositories.Products;

namespace tdd_architecture_template_dotnet.Tests.Repositories.Products
{
    public class ProductTypeRepositoryTests
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
        public async Task GetAll_ShouldReturnAllProductTypes()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductTypeRepository(context);
            {
                await context.ProductTypes.AddAsync(new ProductType { Name = "ProductType1" });
                await context.SaveChangesAsync();
            }

            // Act
            var result = await repository.GetAll();

            // Assert
            Assert.NotNull(result); 
            Assert.IsType<List<ProductType>>(result);
            Assert.True(result.Any() || !context.ProductTypes.Any());
        }


        [Fact]
        public async Task GetById_ShouldReturnProductType()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductTypeRepository(context);

            var productType = new ProductType { Name = "ProductType1", Description = "Product Type One" };
            await context.ProductTypes.AddAsync(productType);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetById(productType.Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Post_ShouldCreateProductType()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductTypeRepository(context);

            var productType = new ProductType { Name = "ProductType1", Description = "Product Type One" };

            // Act
            var result = await repository.Post(productType);

            // Assert
            Assert.NotNull(result.CreationDate);
            Assert.Contains(await context.ProductTypes.ToListAsync(), s => s.Id == result.Id);
        }

        [Fact]
        public async Task Put_ShouldUpdateProductType()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductTypeRepository(context);

            var productType = new ProductType { Id = 1, Name = "ProductType1", Description = "Product Type One" };
            productType.Name = "ProductType2";

            // Act
            var result = await repository.Put(productType);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_ShouldRemoveProductType()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductTypeRepository(context);

            var productType = new ProductType { Name = "ProductType1", Description = "Product Type One" };
            await context.ProductTypes.AddAsync(productType);
            await context.SaveChangesAsync();
            await repository.Delete(productType);

            // Act
            var deletedProductType = await context.ProductTypes.FindAsync(productType.Id);

            // Assert

            Assert.Null(deletedProductType);
        }
    }
}