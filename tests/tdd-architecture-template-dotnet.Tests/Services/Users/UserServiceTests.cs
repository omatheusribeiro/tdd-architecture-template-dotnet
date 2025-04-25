using AutoFixture;
using AutoMapper;
using Moq;
using tdd_architecture_template_dotnet.Application.Services.Users;
using tdd_architecture_template_dotnet.Application.ViewModels.Users;
using tdd_architecture_template_dotnet.Domain.Entities.Users;
using tdd_architecture_template_dotnet.Domain.Interfaces.Users;
using tdd_architecture_template_dotnet.Infrastructure.Singletons.Logger.Interfaces;

namespace tdd_architecture_template_dotnet.Tests.Services.Users
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserAddressRepository> _userAddressRepositoryMock;
        private readonly Mock<IUserContactRepository> _userContactRepositoryMock;
        private readonly Mock<ILoggerService> _loggerServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserService _userService;
        private readonly Fixture _fixture;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userAddressRepositoryMock = new Mock<IUserAddressRepository>();
            _userContactRepositoryMock = new Mock<IUserContactRepository>();
            _loggerServiceMock = new Mock<ILoggerService>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(
                _userRepositoryMock.Object,
                _userAddressRepositoryMock.Object,
                _userContactRepositoryMock.Object,
                _loggerServiceMock.Object,
                _mapperMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAll_WhenUsersExist_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Document = "12345678900" }
            };

            var usersViewModel = new List<UserViewModel>
            {
                new UserViewModel {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Document = "12345678900" }
            };

            _userRepositoryMock.Setup(x => x.GetAll())
                .ReturnsAsync(users);

            _mapperMock.Setup(x => x.Map<IEnumerable<UserViewModel>>(users))
                .Returns(usersViewModel);

            // Act
            var result = await _userService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(usersViewModel.Count, result.Data.Count());
            Assert.Equal(usersViewModel, result.Data);

            _userRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            _mapperMock.Verify(x => x.Map<IEnumerable<UserViewModel>>(users), Times.Once);
        }


        [Fact]
        public async Task GetById_WhenUserExists_ReturnsUser()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, FirstName = "John", LastName = "Doe", Document = "12345678900" };
            var userViewModel = new UserViewModel { Id = userId, FirstName = "John", LastName = "Doe", Document = "12345678900" };

            _userRepositoryMock.Setup(x => x.GetById(userId))
                .ReturnsAsync(user);

            _mapperMock.Setup(x => x.Map<UserViewModel>(user))
                .Returns(userViewModel);

            // Act
            var result = await _userService.GetById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(userViewModel.Id, result.Data.Id);
            Assert.Equal(userViewModel.FirstName, result.Data.FirstName);

            _userRepositoryMock.Verify(x => x.GetById(userId), Times.Once);
            _mapperMock.Verify(x => x.Map<UserViewModel>(user), Times.Once);
        }


        [Fact]
        public async Task Put_WithValidUser_ReturnsSuccess()
        {
            // Arrange
            var userViewModel = new UserViewModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Document = "12345678900",
                Address = new UserAddressViewModel
                {
                    Id = 1,
                    Street = "Rua das Flores",
                    Number = 123,
                    Complement = "",
                    Neighborhood = "Test",
                    City = "São Paulo",
                    State = "SP",
                    Country = "Brazil",
                    ZipCode = "01000-000"
                },
                Contact = new UserContactViewModel
                {
                    Id = 1,
                    Email = "user@email.com",
                    PhoneNumber = "(11) 99999-9999"
                }
            };

            var user = new User { Id = 1, FirstName = "John", LastName = "Doe", Document = "12345678900" };
            var address = new UserAddress
            {
                Id = 1,
                Street = "Rua das Flores",
                Number = 123,
                Complement = "",
                Neighborhood = "Test",
                City = "São Paulo",
                State = "SP",
                Country = "Brazil",
                ZipCode = "01000-000",
                UserId = 1
            };
            var contact = new UserContact
            {
                Id = 1,
                Email = "user@email.com",
                PhoneNumber = "(11) 99999-9999",
                UserId = 1
            };

            _userRepositoryMock.Setup(x => x.GetById(userViewModel.Id)).ReturnsAsync(user);
            _userAddressRepositoryMock.Setup(x => x.GetById(userViewModel.Address.Id)).ReturnsAsync(address);
            _userContactRepositoryMock.Setup(x => x.GetById(userViewModel.Contact.Id)).ReturnsAsync(contact);

            _mapperMock.Setup(x => x.Map<User>(userViewModel)).Returns(user);
            _mapperMock.Setup(x => x.Map<UserAddress>(userViewModel.Address)).Returns(address);
            _mapperMock.Setup(x => x.Map<UserContact>(userViewModel.Contact)).Returns(contact);

            _userRepositoryMock.Setup(x => x.Put(user)).ReturnsAsync(user);
            _userAddressRepositoryMock.Setup(x => x.Put(address)).ReturnsAsync(address);
            _userContactRepositoryMock.Setup(x => x.Put(contact)).ReturnsAsync(contact);

            _mapperMock.Setup(x => x.Map<UserViewModel>(user)).Returns(userViewModel);
            _mapperMock.Setup(x => x.Map<UserAddressViewModel>(address)).Returns(userViewModel.Address);
            _mapperMock.Setup(x => x.Map<UserContactViewModel>(contact)).Returns(userViewModel.Contact);

            // Act
            var result = await _userService.Put(userViewModel);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(userViewModel.Id, result.Data.Id);
            Assert.Equal(userViewModel.FirstName, result.Data.FirstName);
            Assert.Equal(userViewModel.Address.Id, result.Data.Address.Id);
            Assert.Equal(userViewModel.Contact.Id, result.Data.Contact.Id);

            _userRepositoryMock.Verify(x => x.GetById(userViewModel.Id), Times.Once);
            _userRepositoryMock.Verify(x => x.Put(user), Times.Once);
            _mapperMock.Verify(x => x.Map<User>(userViewModel), Times.Once);
        }


        [Fact]
        public async Task Post_WithValidUser_ReturnsSuccess()
        {
            // Arrange
            var userViewModel = new UserViewModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Document = "12345678900",
                Address = new UserAddressViewModel
                {
                    Id = 1,
                    Street = "Rua das Flores",
                    Number = 123,
                    Complement = "",
                    Neighborhood = "Test",
                    City = "São Paulo",
                    State = "SP",
                    Country = "Brazil",
                    ZipCode = "01000-000"
                },
                Contact = new UserContactViewModel
                {
                    Id = 1,
                    Email = "user@email.com",
                    PhoneNumber = "(11) 99999-9999"
                }
            };

            var user = new User { Id = 1, FirstName = "John", LastName = "Doe", Document = "12345678900" };
            var address = new UserAddress
            {
                Id = 1,
                Street = "Rua das Flores",
                Number = 123,
                Complement = "",
                Neighborhood = "Test",
                City = "São Paulo",
                State = "SP",
                Country = "Brazil",
                ZipCode = "01000-000",
                UserId = 1
            };
            var contact = new UserContact
            {
                Id = 1,
                Email = "user@email.com",
                PhoneNumber = "(11) 99999-9999",
                UserId = 1
            };

            _mapperMock.Setup(x => x.Map<User>(userViewModel)).Returns(user);
            _mapperMock.Setup(x => x.Map<UserAddress>(userViewModel.Address)).Returns(address);
            _mapperMock.Setup(x => x.Map<UserContact>(userViewModel.Contact)).Returns(contact);

            _userRepositoryMock.Setup(x => x.Post(user)).ReturnsAsync(user);
            _userAddressRepositoryMock.Setup(x => x.Post(address)).ReturnsAsync(address);
            _userContactRepositoryMock.Setup(x => x.Post(contact)).ReturnsAsync(contact);

            _mapperMock.Setup(x => x.Map<UserViewModel>(user)).Returns(userViewModel);
            _mapperMock.Setup(x => x.Map<UserAddressViewModel>(address)).Returns(userViewModel.Address);
            _mapperMock.Setup(x => x.Map<UserContactViewModel>(contact)).Returns(userViewModel.Contact);

            // Act
            var result = await _userService.Post(userViewModel);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(userViewModel.Id, result.Data.Id);
            Assert.Equal(userViewModel.FirstName, result.Data.FirstName);
            Assert.Equal(userViewModel.Address.Id, result.Data.Address.Id);
            Assert.Equal(userViewModel.Contact.Id, result.Data.Contact.Id);

            _userRepositoryMock.Verify(x => x.Post(user), Times.Once);
            _userAddressRepositoryMock.Verify(x => x.Post(address), Times.Once);
            _userContactRepositoryMock.Verify(x => x.Post(contact), Times.Once);
            _mapperMock.Verify(x => x.Map<User>(userViewModel), Times.Once);
            _mapperMock.Verify(x => x.Map<UserAddress>(userViewModel.Address), Times.Once);
            _mapperMock.Verify(x => x.Map<UserContact>(userViewModel.Contact), Times.Once);
        }


        [Fact]
        public async Task Delete_WithValidUser_ReturnsSuccess()
        {
            // Arrange
            var userViewModel = new UserViewModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Document = "12345678900",
                Address = new UserAddressViewModel
                {
                    Id = 1,
                    Street = "Rua das Flores",
                    Number = 123,
                    Complement = "",
                    Neighborhood = "Test",
                    City = "São Paulo",
                    State = "SP",
                    Country = "Brazil",
                    ZipCode = "01000-000"
                },
                Contact = new UserContactViewModel
                {
                    Id = 1,
                    Email = "user@email.com",
                    PhoneNumber = "(11) 99999-9999"
                }
            };

            var user = new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Document = "12345678900"
            };
            var address = new UserAddress
            {
                Id = 1,
                Street = "Rua das Flores",
                Number = 123,
                Complement = "",
                Neighborhood = "Test",
                City = "São Paulo",
                State = "SP",
                Country = "Brazil",
                ZipCode = "01000-000",
                UserId = 1
            };
            var contact = new UserContact
            {
                Id = 1,
                Email = "user@email.com",
                PhoneNumber = "(11) 99999-9999",
                UserId = 1
            };

            _userRepositoryMock.Setup(x => x.GetById(userViewModel.Id))
                .ReturnsAsync(user);

            _userAddressRepositoryMock.Setup(x => x.GetById(userViewModel.Address.Id))
                .ReturnsAsync(address);

            _userContactRepositoryMock.Setup(x => x.GetById(userViewModel.Contact.Id))
                .ReturnsAsync(contact);

            _userRepositoryMock.Setup(x => x.Delete(user))
                .ReturnsAsync(user);

            _userAddressRepositoryMock.Setup(x => x.Delete(address))
                .ReturnsAsync(address);

            _userContactRepositoryMock.Setup(x => x.Delete(contact))
                .ReturnsAsync(contact);

            _mapperMock.Setup(x => x.Map<UserViewModel>(user))
                .Returns(userViewModel);

            // Act
            var result = await _userService.Delete(1);

            // Assert
            Assert.NotNull(result);
        }


        [Fact]
        public async Task Post_WithNullUser_ReturnsFail()
        {
            // Act
            var result = await _userService.Post(null);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("There is missing information to perform user creation.", result.Message);
        }

        [Fact]
        public async Task Post_WithNullAddress_ReturnsFail()
        {
            // Arrange
            var userViewModel = _fixture.Create<UserViewModel>();
            userViewModel.Address = null;

            // Act
            var result = await _userService.Post(userViewModel);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("There is missing information to perform user address creation.", result.Message);
        }

        [Fact]
        public async Task Post_WithNullContact_ReturnsFail()
        {
            // Arrange
            var userViewModel = _fixture.Create<UserViewModel>();
            userViewModel.Contact = null;

            // Act
            var result = await _userService.Post(userViewModel);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("There is missing information to perform user address creation.", result.Message);
        }

        [Fact]
        public async Task Delete_WithNullUser_ReturnsFail()
        {
            // Act
            var result = await _userService.Delete(0);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("There is missing information to delete the user.", result.Message);
        }

        [Fact]
        public async Task Delete_WhenUserNotFound_ReturnsFail()
        {
            // Act
            var result = await _userService.Delete(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("User not found.", result.Message);
        }
    }
}
