using Microsoft.EntityFrameworkCore;
using Moq;
using tdd_architecture_template_dotnet.Domain.Entities.Products;
using tdd_architecture_template_dotnet.Infrastructure.Data.Context;
using tdd_architecture_template_dotnet.Infrastructure.Repositories.Products;
using Xunit;

namespace tdd_architecture_template_dotnet.Tests.Repositories.Products
{
    public class ProductTypeRepositoryTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly ProductTypeRepository _repository;
        private readonly Mock<DbSet<ProductType>> _dbSetMock;

        public ProductTypeRepositoryTests()
        {
            _contextMock = new Mock<ApplicationDbContext>();
            _dbSetMock = new Mock<DbSet<ProductType>>();
            _contextMock.Setup(x => x.ProductTypes).Returns(_dbSetMock.Object);
            _repository = new ProductTypeRepository(_contextMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllProductTypes()
        {
            // Arrange
            var productTypes = new List<ProductType>
            {
                new ProductType { Id = 1, Name = "Tipo 1" },
                new ProductType { Id = 2, Name = "Tipo 2" }
            };

            _dbSetMock.Setup(x => x.ToListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(productTypes);

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(productTypes, result);
        }

        [Fact]
        public async Task GetById_ShouldReturnProductType()
        {
            // Arrange
            var productType = new ProductType { Id = 1, Name = "Tipo 1" };
            var queryable = new List<ProductType> { productType }.AsQueryable();

            _dbSetMock.As<IQueryable<ProductType>>().Setup(x => x.Provider).Returns(queryable.Provider);
            _dbSetMock.As<IQueryable<ProductType>>().Setup(x => x.Expression).Returns(queryable.Expression);
            _dbSetMock.As<IQueryable<ProductType>>().Setup(x => x.ElementType).Returns(queryable.ElementType);
            _dbSetMock.As<IQueryable<ProductType>>().Setup(x => x.GetEnumerator()).Returns(queryable.GetEnumerator());

            // Act
            var result = await _repository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task Post_ShouldCreateProductType()
        {
            // Arrange
            var productType = new ProductType { Name = "Novo Tipo" };

            // Act
            var result = await _repository.Post(productType);

            // Assert
            Assert.NotNull(result.CreationDate);
            _contextMock.Verify(x => x.ProductTypes.Add(It.IsAny<ProductType>()), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Put_ShouldUpdateProductType()
        {
            // Arrange
            var productType = new ProductType { Id = 1, Name = "Tipo Atualizado" };

            // Act
            var result = await _repository.Put(productType);

            // Assert
            Assert.NotNull(result.ChangeDate);
            _contextMock.Verify(x => x.ProductTypes.Update(It.IsAny<ProductType>()), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldRemoveProductType()
        {
            // Arrange
            var productType = new ProductType { Id = 1, Name = "Tipo para Deletar" };

            // Act
            var result = await _repository.Delete(productType);

            // Assert
            _contextMock.Verify(x => x.ProductTypes.Remove(It.IsAny<ProductType>()), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}