using AutoFixture;
using AutoMapper;
using Moq;
using tdd_architecture_template_dotnet.Application.Services.Products;
using tdd_architecture_template_dotnet.Application.ViewModels.Products;
using tdd_architecture_template_dotnet.Domain.Entities.Products;
using tdd_architecture_template_dotnet.Domain.Interfaces.Products;
using tdd_architecture_template_dotnet.Infrastructure.Singletons.Cache.Interfaces;
using tdd_architecture_template_dotnet.Infrastructure.Singletons.Logger.Interfaces;

namespace tdd_architecture_template_dotnet.Tests.Services.Products
{
    public class ProductServiceTests 
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IProductTypeRepository> _productTypeRepositoryMock;
        private readonly Mock<ILoggerService> _loggerServiceMock;
        private readonly Mock<ICacheService> _cacheServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductService _productService;
        private readonly Fixture _fixture;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productTypeRepositoryMock = new Mock<IProductTypeRepository>();
            _loggerServiceMock = new Mock<ILoggerService>();
            _cacheServiceMock = new Mock<ICacheService>();
            _mapperMock = new Mock<IMapper>();
            _fixture = new Fixture();

            // Configura o Fixture para ignorar loops recursivos
            _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _productService = new ProductService(
                _productRepositoryMock.Object,
                _productTypeRepositoryMock.Object,
                _loggerServiceMock.Object,
                _cacheServiceMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task GetAll_WhenProductsExist_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Smartphone", ProductTypeId = 1 },
                new Product { Id = 2, Name = "Laptop", ProductTypeId = 1 }
            };

            var productsViewModel = new List<ProductViewModel>
            {
                new ProductViewModel { Id = 1, Name = "Smartphone", ProductTypeId = 1 },
                new ProductViewModel { Id = 2, Name = "Laptop", ProductTypeId = 1 }
            };

            _productRepositoryMock.Setup(x => x.GetAll())
                .ReturnsAsync(products);

            _mapperMock.Setup(x => x.Map<IEnumerable<ProductViewModel>>(products))
                .Returns(productsViewModel);

            // Act
            var result = await _productService.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(productsViewModel, result.Data);
            _productRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetAll_WhenNoProducts_ReturnsFail()
        {
            // Arrange
            _productRepositoryMock.Setup(x => x.GetAll())
                .ReturnsAsync((IEnumerable<Product>)null);

            // Act
            var result = await _productService.GetAll();

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Unable to identify products in the database.", result.Message);
        }

        [Fact]
        public async Task GetById_WhenProductExists_ReturnsProduct()
        {
            // Arrange
            var productId = 1;

            var product = new Product
            {
                Id = productId,
                Name = "Smartphone",
                ProductTypeId = 1
            };

            var productViewModel = new ProductViewModel
            {
                Id = productId,
                Name = "Smartphone",
                ProductTypeId = 1
            };

            _productRepositoryMock.Setup(x => x.GetById(productId))
                .ReturnsAsync(product);

            _mapperMock.Setup(x => x.Map<ProductViewModel>(product))
                .Returns(productViewModel);

            // Act
            var result = await _productService.GetById(productId);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(productViewModel.Id, result.Data.Id);
            Assert.Equal(productViewModel.Name, result.Data.Name);
            Assert.Equal(productViewModel.ProductTypeId, result.Data.ProductTypeId);
        }


        [Fact]
        public async Task GetById_WhenProductDoesNotExist_ReturnsFail()
        {
            // Arrange
            var productId = 999;

            _productRepositoryMock.Setup(x => x.GetById(productId))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _productService.GetById(productId);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task Put_WithValidProduct_ReturnsSuccess()
        {
            // Arrange
            var productViewModel = new ProductViewModel
            {
                Id = 1,
                Name = "Smartphone",
                ProductTypeId = 1
            };

            var productType = new ProductType
            {
                Id = 1,
                Name = "Eletronics"
            };

            var product = new Product
            {
                Id = 1,
                Name = "Smartphone",
                ProductTypeId = productType.Id
            };

            _productTypeRepositoryMock.Setup(x => x.GetById(productViewModel.ProductTypeId))
                .ReturnsAsync(productType);

            _mapperMock.Setup(x => x.Map<Product>(productViewModel))
                .Returns(product);

            _productRepositoryMock.Setup(x => x.Put(product))
                .ReturnsAsync(product);

            _mapperMock.Setup(x => x.Map<ProductViewModel>(product))
                .Returns(productViewModel);

            // Act
            var result = await _productService.Put(productViewModel);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(productViewModel.Id, result.Data.Id);
            Assert.Equal(productViewModel.Name, result.Data.Name);
            Assert.Equal(productViewModel.ProductTypeId, result.Data.ProductTypeId);
        }


        [Fact]
        public async Task Put_WithInvalidProductType_ReturnsFail()
        {
            // Arrange
            var productViewModel = _fixture.Create<ProductViewModel>();

            _productTypeRepositoryMock.Setup(x => x.GetById(productViewModel.ProductTypeId))
                .ReturnsAsync((ProductType)null);

            // Act
            var result = await _productService.Put(productViewModel);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("product type not found.", result.Message);
        }

        [Fact]
        public async Task Post_WithValidProduct_ReturnsSuccess()
        {
            // Arrange
            var productViewModel = new ProductViewModel
            {
                Id = 1,
                Name = "Smartphone",
                ProductTypeId = 1
            };

            var productType = new ProductType
            {
                Id = 1,
                Name = "Eletronics"
            };

            var product = new Product
            {
                Id = 1,
                Name = "Smartphone",
                ProductTypeId = productType.Id,
            };

            _productTypeRepositoryMock.Setup(x => x.GetById(productViewModel.ProductTypeId))
                .ReturnsAsync(productType);

            _mapperMock.Setup(x => x.Map<Product>(productViewModel))
                .Returns(product);

            _productRepositoryMock.Setup(x => x.Post(product))
                .ReturnsAsync(product);

            _mapperMock.Setup(x => x.Map<ProductViewModel>(product))
                .Returns(productViewModel);

            // Act
            var result = await _productService.Post(productViewModel);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(productViewModel.Id, result.Data.Id);
            Assert.Equal(productViewModel.Name, result.Data.Name);
            Assert.Equal(productViewModel.ProductTypeId, result.Data.ProductTypeId);
        }


        [Fact]
        public async Task Post_WithInvalidProductType_ReturnsFail()
        {
            // Arrange
            var productViewModel = _fixture.Create<ProductViewModel>();

            _productTypeRepositoryMock.Setup(x => x.GetById(productViewModel.ProductTypeId))
                .ReturnsAsync((ProductType)null);

            // Act
            var result = await _productService.Post(productViewModel);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("product type not found.", result.Message);
        }

        [Fact]
        public async Task Delete_WithInvalidProductType_ReturnsFail()
        {
            // Arrange
            var productViewModel = _fixture.Create<ProductViewModel>();

            _productTypeRepositoryMock.Setup(x => x.GetById(productViewModel.ProductTypeId))
                .ReturnsAsync((ProductType)null);

            // Act
            var result = await _productService.Delete(1);

            // Assert
            Assert.False(result.Success);
        }
    }
}
