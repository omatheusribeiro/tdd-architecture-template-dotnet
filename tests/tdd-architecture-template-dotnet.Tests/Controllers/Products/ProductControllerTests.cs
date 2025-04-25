using Moq;
using Microsoft.AspNetCore.Mvc;
using tdd_architecture_template_dotnet.Controllers.V1.Products;
using tdd_architecture_template_dotnet.Application.Services.Products.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Products;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Application.Models.Http;

namespace tdd_architecture_template_dotnet.Tests.Controllers.Products
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _controller = new ProductController(_productServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WhenSuccessful()
        {
            var expected = new Result<IEnumerable<ProductViewModel>>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = new List<ProductViewModel> { new ProductViewModel { Id = 1, Name = "Smartphone", ProductTypeId = 1 } }
            };

            _productServiceMock.Setup(s => s.GetAll()).ReturnsAsync(expected);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task GetAll_ReturnsBadRequest_WhenError()
        {
            var expected = new Result<IEnumerable<ProductViewModel>>
            {
                StatusCode = (int)HttpStatus.BadRequest,
                Message = "Error"
            };

            _productServiceMock.Setup(s => s.GetAll()).ReturnsAsync(expected);

            var result = await _controller.GetAll();

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expected, badRequest.Value);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenSuccessful()
        {
            var expected = new Result<ProductViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = new ProductViewModel { Id = 1, Name = "Smartphone", ProductTypeId = 1 }
            };

            _productServiceMock.Setup(s => s.GetById(1)).ReturnsAsync(expected);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task Post_ReturnsOk_WhenSuccessful()
        {
            var input = new ProductViewModel { Id = 1, Name = "Smartphone", ProductTypeId = 1 };
            var expected = new Result<ProductViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = input
            };

            _productServiceMock.Setup(s => s.Post(input)).ReturnsAsync(expected);

            var result = await _controller.Post(input);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenErro()
        {
            var input = new ProductViewModel { Id = 1, Name = "Smartphone", ProductTypeId = 1 };
            var expected = new Result<ProductViewModel>
            {
                StatusCode = (int)HttpStatus.BadRequest,
                Message = "Failed to update"
            };

            _productServiceMock.Setup(s => s.Put(input)).ReturnsAsync(expected);

            var result = await _controller.Put(input);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(expected, badRequest.Value);
        }

        [Fact]
        public async Task Delete_ReturnsOk_WhenSuccessful()
        {
            var input = new ProductViewModel { Id = 1, Name = "Smartphone", ProductTypeId = 1 };
            var expected = new Result<ProductViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = input
            };

            _productServiceMock.Setup(s => s.Delete(1)).ReturnsAsync(expected);

            var result = await _controller.Delete(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expected, okResult.Value);
        }
    }
}