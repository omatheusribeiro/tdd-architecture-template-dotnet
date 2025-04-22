using Moq;
using Microsoft.AspNetCore.Mvc;
using tdd_architecture_template_dotnet.Controllers.V1.Users;
using tdd_architecture_template_dotnet.Application.Services.Users.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Users;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Application.Models.Http;

namespace tdd_architecture_template_dotnet.Tests.Controllers.Users
{
    public class UserAddressControllerTests
    {
        private readonly Mock<IUserAddressService> _userAddressServiceMock;
        private readonly UserAddressController _controller;

        public UserAddressControllerTests()
        {
            // Create mock for IUserAddressService
            _userAddressServiceMock = new Mock<IUserAddressService>();

            // Instantiate the controller with the mocked service
            _controller = new UserAddressController(_userAddressServiceMock.Object);
        }

        [Fact(DisplayName = "PutAddress returns OK when update is successful")]
        public async Task PutAddress_ReturnsOk_WhenSuccess()
        {
            // Arrange - create a valid UserAddressViewModel and expected successful result
            var input = new UserAddressViewModel {
                Id = 1,
                Street = "Rua das Flores",
                Number = 123,
                Complement = "",
                Neighborhood = "Test",
                City = "São Paulo",
                State = "SP",
                Country = "Brazil",
                ZipCode = "01000-000"
            };
            var expected = new Result<UserAddressViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = input
            };

            _userAddressServiceMock
                .Setup(s => s.Put(input))
                .ReturnsAsync(expected);

            // Act - call the controller's Put method
            var result = await _controller.Put(input);

            // Assert - verify that the response is OkObjectResult with expected data
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expected, okResult.Value);
        }

        [Fact(DisplayName = "PutAddress returns BadRequest when update fails")]
        public async Task PutAddress_ReturnsBadRequest_WhenBadRequest()
        {
            // Arrange - simulate a failed update with BadRequest status code
            var input = new UserAddressViewModel {
                Id = 1,
                Street = "Rua das Flores",
                Number = 123,
                Complement = "",
                Neighborhood = "Test",
                City = "São Paulo",
                State = "SP",
                Country = "Brazil",
                ZipCode = "01000-000"
            };
            var error = new Result<UserAddressViewModel>
            {
                StatusCode = (int)HttpStatus.BadRequest,
                Message = "Invalid address"
            };

            _userAddressServiceMock
                .Setup(s => s.Put(input))
                .ReturnsAsync(error);

            // Act - call the controller's Put method
            var result = await _controller.Put(input);

            // Assert - verify that the response is BadRequestObjectResult with error
            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(error, badRequest.Value);
        }
    }
}