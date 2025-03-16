using AutoFixture;
using AutoMapper;
using Moq;
using tdd_architecture_template_dotnet.Application.Services.Sales;
using tdd_architecture_template_dotnet.Application.ViewModels.Sales;
using tdd_architecture_template_dotnet.Domain.Entities.Products;
using tdd_architecture_template_dotnet.Domain.Entities.Sales;
using tdd_architecture_template_dotnet.Domain.Entities.Users;
using tdd_architecture_template_dotnet.Domain.Interfaces.Products;
using tdd_architecture_template_dotnet.Domain.Interfaces.Sales;
using tdd_architecture_template_dotnet.Domain.Interfaces.Users;

namespace tdd_architecture_template_dotnet.Tests.Services.Sales
{
    public class SaleServiceTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly SaleService _saleService;
        private readonly Fixture _fixture;

        public SaleServiceTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _saleService = new SaleService(
                _saleRepositoryMock.Object,
                _productRepositoryMock.Object,
                _userRepositoryMock.Object,
                _mapperMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAll_ReturnsAllSales()
        {
            // Arrange
            var sales = new List<Sale>
            {
                new Sale { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 },
                new Sale { Id = 2, TotalValue = 10, UserId = 1, ProductId = 1 }
            };

            var salesViewModel = new List<SaleViewModel>
            {
                new SaleViewModel { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 },
                new SaleViewModel { Id = 2, TotalValue = 10, UserId = 1, ProductId = 1 }
            };

            _saleRepositoryMock.Setup(x => x.GetAll())
                .ReturnsAsync(sales);

            _mapperMock.Setup(x => x.Map<IEnumerable<SaleViewModel>>(sales))
                .Returns(salesViewModel);

            // Act
            var result = await _saleService.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(salesViewModel.Count, result.Data.Count());
            Assert.Equal(salesViewModel[0].Id, result.Data.ElementAt(0).Id);
            Assert.Equal(salesViewModel[1].Id, result.Data.ElementAt(1).Id);
        }


        [Fact]
        public async Task Post_WithValidSale_ReturnsSuccess()
        {
            // Arrange
            var saleViewModel = new SaleViewModel
            {
                Id = 1,
                ProductId = 10,
                UserId = 5,
                TotalValue = 150
            };

            var sale = new Sale
            {
                Id = 1,
                ProductId = 10,
                UserId = 5,
                TotalValue = 150
            };

            var product = new Product
            {
                Id = 10,
                Name = "Smartphone",
                Value = 1500,
                ProductTypeId = 1
            };

            var user = new User
            {
                Id = 5,
                FirstName = "João Silva"
            };

            _productRepositoryMock.Setup(x => x.GetById(saleViewModel.ProductId))
                .ReturnsAsync(product);

            _userRepositoryMock.Setup(x => x.GetById(saleViewModel.UserId))
                .ReturnsAsync(user);

            _mapperMock.Setup(x => x.Map<Sale>(saleViewModel))
                .Returns(sale);

            _saleRepositoryMock.Setup(x => x.Post(sale))
                .ReturnsAsync(sale);

            _mapperMock.Setup(x => x.Map<SaleViewModel>(sale))
                .Returns(saleViewModel);

            // Act
            var result = await _saleService.Post(saleViewModel);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(saleViewModel.Id, result.Data.Id);
            Assert.Equal(saleViewModel.ProductId, result.Data.ProductId);
            Assert.Equal(saleViewModel.UserId, result.Data.UserId);
            Assert.Equal(saleViewModel.TotalValue, result.Data.TotalValue);
        }

    }
}
