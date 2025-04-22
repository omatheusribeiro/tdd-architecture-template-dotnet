using Moq;
using Microsoft.AspNetCore.Mvc;
using tdd_architecture_template_dotnet.Controllers.V1.Users;
using tdd_architecture_template_dotnet.Application.Services.Users.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Users;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Application.Models.Http;

namespace tdd_architecture_template_dotnet.Tests.Controllers.Users
{
    public class UserContactControllerTests
    {
        private readonly Mock<IUserContactService> _userContactServiceMock;
        private readonly UserContactController _controller;

        public UserContactControllerTests()
        {
            // Arrange: create a mock of the service and inject into the controller
            _userContactServiceMock = new Mock<IUserContactService>();
            _controller = new UserContactController(_userContactServiceMock.Object);
        }

        [Fact(DisplayName = "PutContact returns OK when the service successfully updates contact info")]
        public async Task PutContact_ReturnsOk_WhenSuccess()
        {
            // Arrange: valid contact and expected successful result
            var contact = new UserContactViewModel {
                Id = 1,
                Email = "user@email.com",
                PhoneNumber = "(11) 99999-9999"
            };
            var expectedResult = new Result<UserContactViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = contact
            };

            _userContactServiceMock
                .Setup(service => service.Put(contact))
                .ReturnsAsync(expectedResult);

            // Act: invoke the controller method
            var result = await _controller.Put(contact);

            // Assert: response should be OkObjectResult with the expected result
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(expectedResult, okResult.Value);
        }

        [Fact(DisplayName = "PutContact returns BadRequest when the service indicates a failure")]
        public async Task PutContact_ReturnsBadRequest_WhenFailure()
        {
            // Arrange: simulate failure from the service
            var contact = new UserContactViewModel {
                Id = 1,
                Email = "user@email.com",
                PhoneNumber = "(11) 99999-9999"
            };
            var errorResult = new Result<UserContactViewModel>
            {
                StatusCode = (int)HttpStatus.BadRequest,
                Message = "Invalid contact info"
            };

            _userContactServiceMock
                .Setup(service => service.Put(contact))
                .ReturnsAsync(errorResult);

            // Act: call the controller's method
            var result = await _controller.Put(contact);

            // Assert: should return BadRequest with the error result
            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(errorResult, badRequest.Value);
        }
    }
}