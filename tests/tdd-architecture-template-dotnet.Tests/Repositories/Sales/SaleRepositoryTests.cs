using Microsoft.EntityFrameworkCore;
using Moq;
using tdd_architecture_template_dotnet.Application.ViewModels.Sales;
using tdd_architecture_template_dotnet.Domain.Entities.Sales;
using tdd_architecture_template_dotnet.Infrastructure.Data.Context;
using tdd_architecture_template_dotnet.Infrastructure.Repositories.Sales;

namespace tdd_architecture_template_dotnet.Tests.Repositories.Sales
{
    public class SaleRepositoryTests
    {
        private readonly Mock<ApplicationDbContext> _contextMock;
        private readonly SaleRepository _repository;
        private readonly Mock<DbSet<Sale>> _dbSetMock;

        public SaleRepositoryTests()
        {
            _contextMock = new Mock<ApplicationDbContext>();
            _dbSetMock = new Mock<DbSet<Sale>>();
            _contextMock.Setup(x => x.Sales).Returns(_dbSetMock.Object);
            _repository = new SaleRepository(_contextMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllSales()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new Sale { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 },
                new Sale { Id = 2, TotalValue = 10, UserId = 1, ProductId = 1 }
            };

            _dbSetMock.Setup(x => x.ToListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(sales);

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(sales, result);
        }

        [Fact]
        public async Task GetById_ShouldReturnSale()
        {
            // Arrange
            var sale = new Sale { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 };
            var queryable = new List<Sale> { sale }.AsQueryable();

            _dbSetMock.As<IQueryable<Sale>>().Setup(x => x.Provider).Returns(queryable.Provider);
            _dbSetMock.As<IQueryable<Sale>>().Setup(x => x.Expression).Returns(queryable.Expression);
            _dbSetMock.As<IQueryable<Sale>>().Setup(x => x.ElementType).Returns(queryable.ElementType);
            _dbSetMock.As<IQueryable<Sale>>().Setup(x => x.GetEnumerator()).Returns(queryable.GetEnumerator());

            // Act
            var result = await _repository.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(100.00m, result.TotalValue);
        }

        [Fact]
        public async Task Post_ShouldCreateSale()
        {
            // Arrange
            var sale = new Sale { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 };

            // Act
            var result = await _repository.Post(sale);

            // Assert
            Assert.NotNull(result.CreationDate);
            _contextMock.Verify(x => x.Sales.Add(It.IsAny<Sale>()), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Put_ShouldUpdateSale()
        {
            // Arrange
            var sale = new Sale { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 };

            // Act
            var result = await _repository.Put(sale);

            // Assert
            Assert.NotNull(result.ChangeDate);
            _contextMock.Verify(x => x.Sales.Update(It.IsAny<Sale>()), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldRemoveSale()
        {
            // Arrange
            var sale = new Sale { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 };

            // Act
            var result = await _repository.Delete(sale);

            // Assert
            _contextMock.Verify(x => x.Sales.Remove(It.IsAny<Sale>()), Times.Once);
            _contextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Post_ShouldSetCreationDateToUtcNow()
        {
            // Arrange
            var sale = new Sale { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 };
            var beforeTest = DateTime.UtcNow;

            // Act
            var result = await _repository.Post(sale);

            // Assert
            Assert.NotNull(result.CreationDate);
            Assert.True(result.CreationDate >= beforeTest);
            Assert.True(result.CreationDate <= DateTime.UtcNow);
        }

        [Fact]
        public async Task Put_ShouldSetChangeDateToUtcNow()
        {
            // Arrange
            var sale = new Sale { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 };
            var beforeTest = DateTime.UtcNow;

            // Act
            var result = await _repository.Put(sale);

            // Assert
            Assert.NotNull(result.ChangeDate);
            Assert.True(result.ChangeDate >= beforeTest);
            Assert.True(result.ChangeDate <= DateTime.UtcNow);
        }
    }
}