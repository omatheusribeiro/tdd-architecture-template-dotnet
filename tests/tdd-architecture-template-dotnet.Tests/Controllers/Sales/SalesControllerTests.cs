using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tdd_architecture_template_dotnet.Application.Services.Sales.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Sales;
using tdd_architecture_template_dotnet.Controllers.V1.Sales;

namespace tdd_architecture_template_dotnet.Tests.Controllers.Sales
{
    public class SaleControllerTests
    {
        private readonly Mock<ISaleService> _saleServiceMock;
        private readonly SaleController _controller;

        public SaleControllerTests()
        {
            _saleServiceMock = new Mock<ISaleService>();
            _controller = new SaleController(_saleServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WhenServiceReturnsValidResponse()
        {
            _saleServiceMock.Setup(s => s.GetAll());

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAll_ReturnsBadRequest_WhenServiceReturnsBadRequest()
        {
            _saleServiceMock.Setup(s => s.GetAll());

            var result = await _controller.GetAll();

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetById_ReturnsOk_WhenServiceReturnsValidResponse()
        {
            _saleServiceMock.Setup(s => s.GetById(It.IsAny<int>()));

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetById_ReturnsBadRequest_WhenServiceReturnsBadRequest()
        {
            _saleServiceMock.Setup(s => s.GetById(It.IsAny<int>()));

            var result = await _controller.GetById(1);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Post_ReturnsOk_WhenServiceReturnsValidResponse()
        {
            _saleServiceMock.Setup(s => s.Post(It.IsAny<SaleViewModel>()));

            var result = await _controller.Post(new SaleViewModel());

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenServiceReturnsBadRequest()
        {
            _saleServiceMock.Setup(s => s.Post(It.IsAny<SaleViewModel>()));

            var result = await _controller.Post(new SaleViewModel());

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
