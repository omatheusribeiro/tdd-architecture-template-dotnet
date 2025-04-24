using Moq;
using AutoMapper;
using tdd_architecture_template_dotnet.Application.Services.Products;
using tdd_architecture_template_dotnet.Application.ViewModels.Products;
using tdd_architecture_template_dotnet.Domain.Entities.Products;
using tdd_architecture_template_dotnet.Domain.Interfaces.Products;
using tdd_architecture_template_dotnet.Infrastructure.Singletons.Logger.Interfaces;
using tdd_architecture_template_dotnet.Domain.Enums;

namespace tdd_architecture_template_dotnet.Tests.Services.Products
{
    public class ProductTypeServiceTests
    {
        private readonly Mock<IProductTypeRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly ProductTypeService _service;

        public ProductTypeServiceTests()
        {
            _mockRepo = new Mock<IProductTypeRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILoggerService>();
            _service = new ProductTypeService(_mockMapper.Object, _mockRepo.Object, _mockLogger.Object);
        }

        [Fact(DisplayName = "Should return product list when data exists")]
        public async Task GetAll_ShouldReturnSuccess_WhenDataExists()
        {
            var entities = new List<ProductType> { new ProductType() };
            var viewModels = new List<ProductTypeViewModel> { new ProductTypeViewModel() };

            _mockRepo.Setup(r => r.GetAll()).ReturnsAsync(entities);
            _mockMapper.Setup(m => m.Map<IEnumerable<ProductTypeViewModel>>(entities)).Returns(viewModels);

            var result = await _service.GetAll();

            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact(DisplayName = "Should return BadRequest when no product types found")]
        public async Task GetAll_ShouldReturnFail_WhenNoData()
        {
            _mockRepo.Setup(r => r.GetAll()).ReturnsAsync((IEnumerable<ProductType>)null);

            var result = await _service.GetAll();

            Assert.False(result.Success);
            Assert.Equal((int)HttpStatus.BadRequest, result.StatusCode);
        }

        [Fact(DisplayName = "Should return error when GetAll throws exception")]
        public async Task GetAll_ShouldCatchException()
        {
            _mockRepo.Setup(r => r.GetAll()).ThrowsAsync(new Exception("Database down"));

            var result = await _service.GetAll();

            Assert.False(result.Success);
            Assert.Contains("error", result.Message.ToLower());
        }

        [Fact(DisplayName = "Should return product type by id")]
        public async Task GetById_ShouldReturnSuccess_WhenFound()
        {
            var entity = new ProductType { Id = 1 };
            var viewModel = new ProductTypeViewModel { Id = 1 };

            _mockRepo.Setup(r => r.GetById(1)).ReturnsAsync(entity);
            _mockMapper.Setup(m => m.Map<ProductTypeViewModel>(entity)).Returns(viewModel);

            var result = await _service.GetById(1);

            Assert.True(result.Success);
            Assert.Equal(1, result.Data.Id);
        }

        [Fact(DisplayName = "Should return BadRequest when product type not found by id")]
        public async Task GetById_ShouldReturnFail_WhenNotFound()
        {
            _mockRepo.Setup(r => r.GetById(1)).ReturnsAsync((ProductType)null);

            var result = await _service.GetById(1);

            Assert.False(result.Success);
            Assert.Equal((int)HttpStatus.BadRequest, result.StatusCode);
        }

        [Fact(DisplayName = "Should return error when GetById throws exception")]
        public async Task GetById_ShouldCatchException()
        {
            _mockRepo.Setup(r => r.GetById(1)).ThrowsAsync(new Exception("Failure"));

            var result = await _service.GetById(1);

            Assert.False(result.Success);
            Assert.Contains("error", result.Message.ToLower());
        }

        [Fact(DisplayName = "Should update product type successfully")]
        public async Task Put_ShouldReturnSuccess_WhenUpdated()
        {
            var viewModel = new ProductTypeViewModel { Id = 1 };
            var entity = new ProductType { Id = 1 };

            _mockRepo.Setup(r => r.GetById(1)).ReturnsAsync(entity);
            _mockMapper.Setup(m => m.Map<ProductType>(viewModel)).Returns(entity);
            _mockRepo.Setup(r => r.Put(entity)).ReturnsAsync(entity);
            _mockMapper.Setup(m => m.Map<ProductTypeViewModel>(entity)).Returns(viewModel);

            var result = await _service.Put(viewModel);

            Assert.True(result.Success);
            Assert.Equal(1, result.Data.Id);
        }

        [Fact(DisplayName = "Should return BadRequest when product to update not found")]
        public async Task Put_ShouldReturnFail_WhenNotFound()
        {
            _mockRepo.Setup(r => r.GetById(1)).ReturnsAsync((ProductType)null);

            var result = await _service.Put(new ProductTypeViewModel { Id = 1 });

            Assert.False(result.Success);
            Assert.Equal((int)HttpStatus.BadRequest, result.StatusCode);
        }

        [Fact(DisplayName = "Should return error when Put throws exception")]
        public async Task Put_ShouldCatchException()
        {
            _mockRepo.Setup(r => r.GetById(1)).ThrowsAsync(new Exception("Update error"));

            var result = await _service.Put(new ProductTypeViewModel { Id = 1 });

            Assert.False(result.Success);
            Assert.Contains("error", result.Message.ToLower());
        }

        [Fact(DisplayName = "Should create product type successfully")]
        public async Task Post_ShouldReturnSuccess_WhenCreated()
        {
            var viewModel = new ProductTypeViewModel();
            var entity = new ProductType();

            _mockMapper.Setup(m => m.Map<ProductType>(viewModel)).Returns(entity);
            _mockRepo.Setup(r => r.Post(entity)).ReturnsAsync(entity);
            _mockMapper.Setup(m => m.Map<ProductTypeViewModel>(entity)).Returns(viewModel);

            var result = await _service.Post(viewModel);

            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Should return error when Post throws exception")]
        public async Task Post_ShouldCatchException()
        {
            _mockMapper.Setup(m => m.Map<ProductType>(It.IsAny<ProductTypeViewModel>())).Throws(new Exception("Insert failed"));

            var result = await _service.Post(new ProductTypeViewModel());

            Assert.False(result.Success);
            Assert.Contains("error", result.Message.ToLower());
        }
    }
}
