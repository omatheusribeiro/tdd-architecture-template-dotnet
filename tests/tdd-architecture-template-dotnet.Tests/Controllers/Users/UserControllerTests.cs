using Moq;
using Microsoft.AspNetCore.Mvc;
using tdd_architecture_template_dotnet.Controllers.V1.Users;
using tdd_architecture_template_dotnet.Application.Services.Users.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Users;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Application.Models.Http;
using tdd_architecture_template_dotnet.Domain.Entities.Users;

namespace tdd_architecture_template_dotnet.Tests.Controllers.Users
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            // Arrange: create service mock and inject it into the controller
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Fact(DisplayName = "GetAll returns OK when users are successfully retrieved")]
        public async Task GetAll_ReturnsOk_WhenSuccess()
        {
            var result = new Result<IEnumerable<UserViewModel>>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = new List<UserViewModel> { new UserViewModel { Id = 1, FirstName = "John", LastName = "Doe", Document = "12345678900" } }
            };

            _userServiceMock.Setup(s => s.GetAll()).ReturnsAsync(result);

            var response = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(result, okResult.Value);
        }

        [Fact(DisplayName = "GetAll returns BadRequest when the service fails")]
        public async Task GetAll_ReturnsBadRequest_WhenFailure()
        {
            var result = new Result<IEnumerable<UserViewModel>>
            {
                StatusCode = (int)HttpStatus.BadRequest,
                Message = "Failed to retrieve users"
            };

            _userServiceMock.Setup(s => s.GetAll()).ReturnsAsync(result);

            var response = await _controller.GetAll();

            var badRequest = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(result, badRequest.Value);
        }

        [Fact(DisplayName = "GetById returns OK when the user exists")]
        public async Task GetById_ReturnsOk_WhenUserExists()
        {
            var result = new Result<UserViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = new UserViewModel { Id = 1, FirstName = "John", LastName = "Doe", Document = "12345678900" }
            };

            _userServiceMock.Setup(s => s.GetById(1)).ReturnsAsync(result);

            var response = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(result, okResult.Value);
        }

        [Fact(DisplayName = "GetById returns BadRequest when the user is not found")]
        public async Task GetById_ReturnsBadRequest_WhenUserNotFound()
        {
            var result = new Result<UserViewModel>
            {
                StatusCode = (int)HttpStatus.BadRequest,
                Message = "User not found"
            };

            _userServiceMock.Setup(s => s.GetById(999)).ReturnsAsync(result);

            var response = await _controller.GetById(999);

            var badRequest = Assert.IsType<BadRequestObjectResult>(response);
            Assert.Equal(result, badRequest.Value);
        }

        [Fact(DisplayName = "PostUser returns OK when user is successfully created")]
        public async Task PostUser_ReturnsOk_WhenSuccess()
        {
            var user = new UserViewModel { Id = 1, FirstName = "John", LastName = "Doe", Document = "12345678900" };
            var result = new Result<UserViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = user
            };

            _userServiceMock.Setup(s => s.Post(user)).ReturnsAsync(result);

            var response = await _controller.Post(user);

            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(result, okResult.Value);
        }

        [Fact(DisplayName = "PostUser returns BadRequest when creation fails")]
        public async Task PostUser_ReturnsBadRequest_WhenFailure()
        {
            var user = new UserViewModel { Id = 1, FirstName = "John", LastName = "Doe", Document = "12345678900" };
            var result = new Result<UserViewModel>
            {
                StatusCode = (int)HttpStatus.BadRequest,
                Message = "Invalid data"
            };

            _userServiceMock.Setup(s => s.Post(user)).ReturnsAsync(result);

            var response = await _controller.Post(user);

            var badRequest = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(result, badRequest.Value);
        }

        [Fact(DisplayName = "PutUser returns OK when update is successful")]
        public async Task PutUser_ReturnsOk_WhenSuccess()
        {
            var user = new UserViewModel { Id = 1, FirstName = "John", LastName = "Doe", Document = "12345678900" };
            var result = new Result<UserViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = user
            };

            _userServiceMock.Setup(s => s.Put(user)).ReturnsAsync(result);

            var response = await _controller.Put(user);

            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(result, okResult.Value);
        }

        [Fact(DisplayName = "PutUser returns BadRequest when update fails")]
        public async Task PutUser_ReturnsBadRequest_WhenFailure()
        {
            var user = new UserViewModel { Id = 1, FirstName = "John", LastName = "Doe", Document = "12345678900" };
            var result = new Result<UserViewModel>
            {
                StatusCode = (int)HttpStatus.BadRequest,
                Message = "Update failed"
            };

            _userServiceMock.Setup(s => s.Put(user)).ReturnsAsync(result);

            var response = await _controller.Put(user);

            var badRequest = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(result, badRequest.Value);
        }

        [Fact(DisplayName = "DeleteUser returns OK when user is deleted successfully")]
        public async Task DeleteUser_ReturnsOk_WhenSuccess()
        {
            var user = new UserViewModel { Id = 1, FirstName = "John", LastName = "Doe", Document = "12345678900" };
            var result = new Result<UserViewModel>
            {
                StatusCode = (int)HttpStatus.Ok,
                Data = user
            };

            _userServiceMock.Setup(s => s.Delete(1)).ReturnsAsync(result);

            var response = await _controller.Delete(1);

            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(result, okResult.Value);
        }

        [Fact(DisplayName = "DeleteUser returns BadRequest when deletion fails")]
        public async Task DeleteUser_ReturnsBadRequest_WhenFailure()
        {
            var user = new UserViewModel { Id = 1, FirstName = "John", LastName = "Doe", Document = "12345678900" };
            var result = new Result<UserViewModel>
            {
                StatusCode = (int)HttpStatus.BadRequest,
                Message = "Cannot delete"
            };

            _userServiceMock.Setup(s => s.Delete(1)).ReturnsAsync(result);

            var response = await _controller.Delete(1);

            var badRequest = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(result, badRequest.Value);
        }
    }
}