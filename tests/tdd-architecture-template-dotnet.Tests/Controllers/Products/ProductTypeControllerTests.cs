using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using tdd_architecture_template_dotnet.Controllers.V1.Products;
using tdd_architecture_template_dotnet.Application.Services.Products.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Products;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Application.Services;
using tdd_architecture_template_dotnet.Application.Models.Http;

namespace tdd_architecture_template_dotnet.Tests.Controllers.Products
{
    public class ProductTypeControllerTests
    {
        private readonly Mock<IProductTypeService> _mockService;
        private readonly ProductTypeController _controller;

        public ProductTypeControllerTests()
        {
            _mockService = new Mock<IProductTypeService>();
            _controller = new ProductTypeController(_mockService.Object);
        }

        [Fact(DisplayName = "Should return Ok with product type list on success")]
        public async Task GetAll_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            // Arrange
            var productTypes = new List<ProductTypeViewModel> { new ProductTypeViewModel() };
            var result = Result<IEnumerable<ProductTypeViewModel>>.Ok(productTypes);
            _mockService.Setup(s => s.GetAll()).ReturnsAsync(result);

            // Act
            var response = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "Should return BadRequest when GetAll fails")]
        public async Task GetAll_ShouldReturnBadRequest_WhenServiceReturnsBadRequest()
        {
            // Arrange
            var result = Result<IEnumerable<ProductTypeViewModel>>.Fail("Failed", (int)HttpStatus.BadRequest);
            _mockService.Setup(s => s.GetAll()).ReturnsAsync(result);

            // Act
            var response = await _controller.GetAll();

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact(DisplayName = "Should return Ok with product type when found by id")]
        public async Task GetById_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            // Arrange
            var productType = new ProductTypeViewModel { Id = 1 };
            var result = Result<ProductTypeViewModel>.Ok(productType);
            _mockService.Setup(s => s.GetById(1)).ReturnsAsync(result);

            // Act
            var response = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "Should return BadRequest when GetById fails")]
        public async Task GetById_ShouldReturnBadRequest_WhenServiceReturnsBadRequest()
        {
            // Arrange
            var result = Result<ProductTypeViewModel>.Fail("Not found", (int)HttpStatus.BadRequest);
            _mockService.Setup(s => s.GetById(1)).ReturnsAsync(result);

            // Act
            var response = await _controller.GetById(1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact(DisplayName = "Should return Ok when Put is successful")]
        public async Task Put_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            // Arrange
            var model = new ProductTypeViewModel { Id = 1 };
            var result = Result<ProductTypeViewModel>.Ok(model);
            _mockService.Setup(s => s.Put(model)).ReturnsAsync(result);

            // Act
            var response = await _controller.Put(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "Should return BadRequest when Put fails")]
        public async Task Put_ShouldReturnBadRequest_WhenServiceReturnsBadRequest()
        {
            // Arrange
            var model = new ProductTypeViewModel();
            var result = Result<ProductTypeViewModel>.Fail("Validation error", (int)HttpStatus.BadRequest);
            _mockService.Setup(s => s.Put(model)).ReturnsAsync(result);

            // Act
            var response = await _controller.Put(model);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact(DisplayName = "Should return Ok when Post is successful")]
        public async Task Post_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            // Arrange
            var model = new ProductTypeViewModel { Id = 1 };
            var result = Result<ProductTypeViewModel>.Ok(model);
            _mockService.Setup(s => s.Post(model)).ReturnsAsync(result);

            // Act
            var response = await _controller.Post(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "Should return BadRequest when Post fails")]
        public async Task Post_ShouldReturnBadRequest_WhenServiceReturnsBadRequest()
        {
            // Arrange
            var model = new ProductTypeViewModel();
            var result = Result<ProductTypeViewModel>.Fail("Error", (int)HttpStatus.BadRequest);
            _mockService.Setup(s => s.Post(model)).ReturnsAsync(result);

            // Act
            var response = await _controller.Post(model);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
    }
}
