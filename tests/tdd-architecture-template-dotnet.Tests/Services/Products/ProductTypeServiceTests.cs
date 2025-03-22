using AutoFixture;
using AutoMapper;
using Moq;
using tdd_architecture_template_dotnet.Application.Services.Products;
using tdd_architecture_template_dotnet.Application.ViewModels.Products;
using tdd_architecture_template_dotnet.Domain.Entities.Products;
using tdd_architecture_template_dotnet.Domain.Interfaces.Products;

namespace tdd_architecture_template_dotnet.Tests.Services.Products
{ 
    public class ProductTypeServiceTests
    {
        private readonly Mock<IProductTypeRepository> _productTypeRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ProductTypeService _productTypeService;
        private readonly Fixture _fixture;

        public ProductTypeServiceTests()
        {
            _productTypeRepositoryMock = new Mock<IProductTypeRepository>();
            _mapperMock = new Mock<IMapper>();
            _productTypeService = new ProductTypeService(_mapperMock.Object, _productTypeRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Put_WithValidProductType_ReturnsSuccess()
        {
            // Arrange
            var productTypeViewModel = new ProductTypeViewModel
            {
                Id = 1,
                Name = "Eletronics"
            };

            var productType = new ProductType
            {
                Id = 1,
                Name = "Eletronics"
            };

            _productTypeRepositoryMock.Setup(x => x.GetById(productTypeViewModel.Id))
                .ReturnsAsync(productType);

            _mapperMock.Setup(x => x.Map<ProductType>(productTypeViewModel))
                .Returns(productType);

            _productTypeRepositoryMock.Setup(x => x.Put(productType))
                .ReturnsAsync(productType);

            _mapperMock.Setup(x => x.Map<ProductTypeViewModel>(productType))
                .Returns(productTypeViewModel);

            // Act
            var result = await _productTypeService.Put(productTypeViewModel);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(productTypeViewModel.Id, result.Data.Id);
            Assert.Equal(productTypeViewModel.Name, result.Data.Name);
        }


        [Fact]
        public async Task Post_WithValidProductType_ReturnsSuccess()
        {
            // Arrange
            var productTypeViewModel = new ProductTypeViewModel
            {
                Id = 1,
                Name = "Eletronics"
            };

            var productType = new ProductType
            {
                Id = 1,
                Name = "Eletronics"
            };

            _mapperMock.Setup(x => x.Map<ProductType>(productTypeViewModel))
                .Returns(productType);

            _productTypeRepositoryMock.Setup(x => x.Post(productType))
                .ReturnsAsync(productType);

            _mapperMock.Setup(x => x.Map<ProductTypeViewModel>(productType))
                .Returns(productTypeViewModel);

            // Act
            var result = await _productTypeService.Post(productTypeViewModel);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(productTypeViewModel.Id, result.Data.Id);
            Assert.Equal(productTypeViewModel.Name, result.Data.Name);
        }

    }
}
