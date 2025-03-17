using Microsoft.EntityFrameworkCore;
using tdd_architecture_template_dotnet.Domain.Entities.Products;
using tdd_architecture_template_dotnet.Infrastructure.Data.Context;
using tdd_architecture_template_dotnet.Infrastructure.Repositories.Products;

namespace tdd_architecture_template_dotnet.Tests.Repositories.Products
{
    public class ProductRepositoryTests
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
        public async Task GetAll_ShouldReturnAllProducts()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductRepository(context);
            {
                await context.Products.AddAsync(new Product { Name = "Product", Description = "Product One", Value = 10, ProductTypeId = 1});
                await context.SaveChangesAsync();
            }

            // Act
            var result = await repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.True(result.Any() || !context.Products.Any());
        }

        [Fact]
        public async Task GetById_ShouldReturnProduct()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductRepository(context);

            var product = new Product { Name = "Product", Description = "Product One", Value = 10, ProductTypeId = 1 };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetById(product.Id);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Post_ShouldCreateProduct()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductRepository(context);

            var product = new Product { Name = "Product", Description = "Product One", Value = 10, ProductTypeId = 1 };

            // Act
            var result = await repository.Post(product);

            // Assert
            Assert.NotNull(result.CreationDate);
            Assert.Contains(await context.Products.ToListAsync(), s => s.Id == result.Id);
        }

        [Fact]
        public async Task Put_ShouldUpdateProduct()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductRepository(context);

            var product = new Product { Id = 1, Name = "Product", Description = "Product One", Value = 10, ProductTypeId = 1 };
            product.Name = "Product";

            // Act
            var result = await repository.Put(product);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_ShouldRemoveProduct()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new ProductRepository(context);

            var product = new Product { Name = "Product", Description = "Product One", Value = 10, ProductTypeId = 1 };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            await repository.Delete(product);

            // Act
            var deletedProduct = await context.Products.FindAsync(product.Id);

            // Assert

            Assert.Null(deletedProduct);
        }
    }
}