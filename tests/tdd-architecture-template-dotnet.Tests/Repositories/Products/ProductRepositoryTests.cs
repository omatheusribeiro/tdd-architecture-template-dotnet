using Microsoft.EntityFrameworkCore;
using Moq;
using tdd_architecture_template_dotnet.Domain.Entities.Products;
using tdd_architecture_template_dotnet.Infrastructure.Data.Context;
using tdd_architecture_template_dotnet.Infrastructure.Repositories.Products;

namespace tdd_architecture_template_dotnet.Tests.Repositories.Products
{
    public class ProductRepositoryTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly ProductRepository _repository;
        private readonly Mock<DbSet<Product>> _dbSetMock;

        public ProductRepositoryTests()
        {
            _contextMock = new Mock<ApplicationDbContext>();
            _dbSetMock = new Mock<DbSet<Product>>();
            _contextMock.Setup(x => x.Products).Returns(_dbSetMock.Object);
            _repository = new ProductRepository(_contextMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Produto 1" },
                new Product { Id = 2, Name = "Produto 2" }
            };

            _dbSetMock.Setup(x => x.ToListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(products, result);
        }

        [Fact]
        public async Task GetById_ShouldReturnProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Produto 1" };
            var queryable = new List<Product> { product }.AsQueryable();

            _dbSetMock.As<IQueryable<Product>>().Setup(x => x.Provider).Returns(queryable.Provider);
            _dbSetMock.As<IQueryable<Product>>().Setup(x => x.Expression).Returns(queryable.Expression);
            _dbSetMock.As<IQueryable<Product>>().Setup(x => x.ElementType).Returns(queryable.ElementType);
            _dbSetMock.As<IQueryable<Product>>().Setup(x => x.GetEnumerator()).Returns(queryable.GetEnumerator());

            // Act
            var result = await _repository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task Post_ShouldCreateProduct()
        {
            // Arrange
            var product = new Product { Name = "Novo Produto" };

            // Act
            var result = await _repository.Post(product);

            // Assert
            Assert.NotNull(result.CreationDate);
            _contextMock.Verify(x => x.Products.Add(It.IsAny<Product>()), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Put_ShouldUpdateProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Produto Atualizado" };

            // Act
            var result = await _repository.Put(product);

            // Assert
            Assert.NotNull(result.ChangeDate);
            _contextMock.Verify(x => x.Products.Update(It.IsAny<Product>()), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldRemoveProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Produto para Deletar" };

            // Act
            var result = await _repository.Delete(product);

            // Assert
            _contextMock.Verify(x => x.Products.Remove(It.IsAny<Product>()), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}