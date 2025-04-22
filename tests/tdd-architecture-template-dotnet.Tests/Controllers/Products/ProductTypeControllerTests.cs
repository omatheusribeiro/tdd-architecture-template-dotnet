using Moq;
using Microsoft.AspNetCore.Mvc;
using tdd_architecture_template_dotnet.Controllers.V1.Products;
using tdd_architecture_template_dotnet.Application.Services.Products.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Products;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Application.Models.Http;

namespace tdd_architecture_template_dotnet.Tests.Controllers.Products
{
    public class ProductTypeControllerTests
    {
        private readonly Mock<IProductTypeService> _productTypeServiceMock;
        private readonly ProductTypeController _controller;

        public ProductTypeControllerTests()
        {
            // Create mock for the service
            _productTypeServiceMock = new Mock<IProductTypeService>();

            // Inject the mock into the controller
            _controller = new ProductTypeController(_productTypeServiceMock.Object);
        }

        [Fact(DisplayName = "PutProductType returns OK when the service succeeds")]
        public async Task PutProductType_ReturnsOk_WhenSuccess()
        {
            // Arrange
            var input = new ProductTypeViewModel { Name = "Electronics" };
            var expectedResult = new Result<ProductTypeViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = input
            };

            _productTypeServiceMock
                .Setup(service => service.Put(input))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.Put(input);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedResult, okResult.Value);
        }

        [Fact(DisplayName = "PutProductType returns BadRequest when service returns error")]
        public async Task PutProductType_ReturnsBadRequest_WhenBadRequest()
        {
            // Arrange
            var input = new ProductTypeViewModel { Name = "Invalid" };
            var errorResult = new Result<ProductTypeViewModel>
            {
                StatusCode = (int)HttpStatus.BadRequest,
                Message = "Invalid data"
            };

            _productTypeServiceMock
                .Setup(service => service.Put(input))
                .ReturnsAsync(errorResult);

            // Act
            var result = await _controller.Put(input);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(errorResult, badRequest.Value);
        }

        [Fact(DisplayName = "PostProductType returns OK when the service succeeds")]
        public async Task PostProductType_ReturnsOk_WhenSuccess()
        {
            // Arrange
            var input = new ProductTypeViewModel { Name = "Books" };
            var expectedResult = new Result<ProductTypeViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = input
            };

            _productTypeServiceMock
                .Setup(service => service.Post(input))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.Post(input);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedResult, okResult.Value);
        }

        [Fact(DisplayName = "PostProductType returns BadRequest when service returns error")]
        public async Task PostProductType_ReturnsBadRequest_WhenBadRequest()
        {
            // Arrange
            var input = new ProductTypeViewModel { Name = "" };
            var errorResult = new Result<ProductTypeViewModel>
            {
                StatusCode = (int)HttpStatus.BadRequest,
                Message = "Name is required"
            };

            _productTypeServiceMock
                .Setup(service => service.Post(input))
                .ReturnsAsync(errorResult);

            // Act
            var result = await _controller.Post(input);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(errorResult, badRequest.Value);
        }
    }
}