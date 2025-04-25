using Moq;
using Microsoft.AspNetCore.Mvc;
using tdd_architecture_template_dotnet.Controllers.V1.Sales;
using tdd_architecture_template_dotnet.Application.Services.Sales.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Sales;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Application.Models.Http;

namespace tdd_architecture_template_dotnet.Tests.Controllers.Sales
{
    public class SaleControllerTests
    {
        private readonly Mock<ISaleService> _saleServiceMock;
        private readonly SaleController _controller;

        public SaleControllerTests()
        {
            // Arrange the mock service and inject into the controller
            _saleServiceMock = new Mock<ISaleService>();
            _controller = new SaleController(_saleServiceMock.Object);
        }

        [Fact(DisplayName = "GetAllSales returns OK when the service succeeds")]
        public async Task GetAllSales_ReturnsOk_WhenSuccessful()
        {
            var expected = new Result<IEnumerable<SaleViewModel>>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = new List<SaleViewModel> { new SaleViewModel { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 } }
            };

            _saleServiceMock.Setup(s => s.GetAll()).ReturnsAsync(expected);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact(DisplayName = "GetAllSales returns BadRequest when response is null or error")]
        public async Task GetAllSales_ReturnsBadRequest_WhenNullOrBadRequest()
        {
            Result<IEnumerable<SaleViewModel>> resultData = null;
            _saleServiceMock.Setup(s => s.GetAll()).ReturnsAsync(resultData);

            var result = await _controller.GetAll();

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "GetSaleById returns OK when the service succeeds")]
        public async Task GetSaleById_ReturnsOk_WhenSuccessful()
        {
            var expected = new Result<SaleViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = new SaleViewModel { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 }
            };

            _saleServiceMock.Setup(s => s.GetById(1)).ReturnsAsync(expected);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact(DisplayName = "GetSaleById returns BadRequest when not found or error")]
        public async Task GetSaleById_ReturnsBadRequest_WhenNullOrBadRequest()
        {
            Result<SaleViewModel> resultData = null;
            _saleServiceMock.Setup(s => s.GetById(99)).ReturnsAsync(resultData);

            var result = await _controller.GetById(99);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "PostSale returns OK when the service succeeds")]
        public async Task PostSale_ReturnsOk_WhenSuccessful()
        {
            var input = new SaleViewModel { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 };
            var expected = new Result<SaleViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = input
            };

            _saleServiceMock.Setup(s => s.Post(input)).ReturnsAsync(expected);

            var result = await _controller.Post(input);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact(DisplayName = "PostSale returns BadRequest when the service fails")]
        public async Task PostSale_ReturnsBadRequest_WhenFailure()
        {
            var input = new SaleViewModel { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 };
            var error = new Result<SaleViewModel>
            {
                StatusCode = (int)HttpStatus.BadRequest,
                Message = "Invalid data"
            };

            _saleServiceMock.Setup(s => s.Post(input)).ReturnsAsync(error);

            var result = await _controller.Post(input);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(error, badRequest.Value);
        }

        [Fact(DisplayName = "PutSale returns OK when update is successful")]
        public async Task PutSale_ReturnsOk_WhenSuccessful()
        {
            var input = new SaleViewModel { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 };
            var expected = new Result<SaleViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = input
            };

            _saleServiceMock.Setup(s => s.Put(input)).ReturnsAsync(expected);

            var result = await _controller.Put(input);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact(DisplayName = "PutSale returns BadRequest when update fails")]
        public async Task PutSale_ReturnsBadRequest_WhenFailure()
        {
            var input = new SaleViewModel { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 };
            var error = new Result<SaleViewModel>
            {
                StatusCode = (int)HttpStatus.BadRequest,
                Message = "Failed to update"
            };

            _saleServiceMock.Setup(s => s.Put(input)).ReturnsAsync(error);

            var result = await _controller.Put(input);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(error, badRequest.Value);
        }

        [Fact(DisplayName = "DeleteSale returns OK when delete is successful")]
        public async Task DeleteSale_ReturnsOk_WhenSuccessful()
        {
            var input = new SaleViewModel { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 };
            var expected = new Result<SaleViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = input
            };

            _saleServiceMock.Setup(s => s.Delete(1)).ReturnsAsync(expected);

            var result = await _controller.Delete(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact(DisplayName = "DeleteSale returns BadRequest when delete fails")]
        public async Task DeleteSale_ReturnsBadRequest_WhenFailure()
        {
            var input = new SaleViewModel { Id = 1, TotalValue = 10, UserId = 1, ProductId = 1 };
            var error = new Result<SaleViewModel>
            {
                StatusCode = (int)HttpStatus.BadRequest,
                Message = "Delete error"
            };

            _saleServiceMock.Setup(s => s.Delete(1)).ReturnsAsync(error);

            var result = await _controller.Delete(1);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(error, badRequest.Value);
        }
    }
}