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
    public class UserContactServiceTests
    {
        private readonly Mock<IUserContactRepository> _userContactRepositoryMock;
        private readonly Mock<ILoggerService> _loggerServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserContactService _userContactService;
        private readonly Fixture _fixture;

        public UserContactServiceTests()
        {
            _userContactRepositoryMock = new Mock<IUserContactRepository>();
            _loggerServiceMock = new Mock<ILoggerService>();
            _mapperMock = new Mock<IMapper>();
            _userContactService = new UserContactService(_mapperMock.Object, _userContactRepositoryMock.Object, _loggerServiceMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Put_WithValidContact_ReturnsSuccess()
        {
            // Arrange
            var contactViewModel = new UserContactViewModel
            {
                Id = 1,
                Email = "user@email.com",
                PhoneNumber = "(11) 99999-9999"
            };

            var contact = new UserContact
            {
                Id = 1,
                Email = "user@email.com",
                PhoneNumber = "(11) 99999-9999",
                UserId = 1
            };

            _userContactRepositoryMock.Setup(x => x.GetById(contactViewModel.Id))
                .ReturnsAsync(contact);

            _mapperMock.Setup(x => x.Map<UserContact>(contactViewModel))
                .Returns(contact);

            _userContactRepositoryMock.Setup(x => x.Put(contact))
                .ReturnsAsync(contact);

            _mapperMock.Setup(x => x.Map<UserContactViewModel>(contact))
                .Returns(contactViewModel);

            // Act
            var result = await _userContactService.Put(contactViewModel);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(contactViewModel, result.Data);

            // Verifica se os métodos foram chamados corretamente
            _userContactRepositoryMock.Verify(x => x.GetById(contactViewModel.Id), Times.Once);
            _userContactRepositoryMock.Verify(x => x.Put(contact), Times.Once);
            _mapperMock.Verify(x => x.Map<UserContact>(contactViewModel), Times.Once);
            _mapperMock.Verify(x => x.Map<UserContactViewModel>(contact), Times.Once);
        }


        [Fact]
        public async Task Put_WithNonExistentContact_ReturnsFail()
        {
            // Arrange
            var contactViewModel = _fixture.Create<UserContactViewModel>();

            _userContactRepositoryMock.Setup(x => x.GetById(contactViewModel.Id))
                .ReturnsAsync((UserContact)null);

            // Act
            var result = await _userContactService.Put(contactViewModel);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("contact not found.", result.Message);
            _userContactRepositoryMock.Verify(x => x.Put(It.IsAny<UserContact>()), Times.Never);
        }

        [Fact]
        public async Task Put_WhenExceptionOccurs_ReturnsFail()
        {
            // Arrange
            var contactViewModel = new UserContactViewModel
            {
                Id = 1,
                Email = "user@email.com",
                PhoneNumber = "(11) 99999-9999"
            };

            var contact = new UserContact
            {
                Id = 1,
                Email = "user@email.com",
                PhoneNumber = "(11) 99999-9999",
                UserId = 1
            };

            var exceptionMessage = "Database error";

            _userContactRepositoryMock.Setup(x => x.GetById(contactViewModel.Id))
                .ReturnsAsync(contact);

            _mapperMock.Setup(x => x.Map<UserContact>(contactViewModel))
                .Returns(contact);

            _userContactRepositoryMock.Setup(x => x.Put(contact))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _userContactService.Put(contactViewModel);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal($"There was an error editing the user contact: {exceptionMessage}", result.Message);

            _userContactRepositoryMock.Verify(x => x.GetById(contactViewModel.Id), Times.Once);
            _userContactRepositoryMock.Verify(x => x.Put(contact), Times.Once);
            _mapperMock.Verify(x => x.Map<UserContact>(contactViewModel), Times.Once);
        }


        [Fact]
        public async Task Put_WithNullContact_ReturnsFail()
        {
            // Arrange
            UserContactViewModel contactViewModel = null;

            // Act
            var result = await _userContactService.Put(contactViewModel);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Object reference not set to an instance of an object", result.Message);
            _userContactRepositoryMock.Verify(x => x.Put(It.IsAny<UserContact>()), Times.Never);
        }

        [Fact]
        public async Task Put_VerifyMappingCalls()
        {
            // Arrange
            var contactViewModel = new UserContactViewModel
            {
                Id = 1,
                Email = "user@email.com",
                PhoneNumber = "(11) 99999-9999"
            };

            var contact = new UserContact
            {
                Id = 1,
                Email = "user@email.com",
                PhoneNumber = "(11) 99999-9999",
                UserId = 1
            };

            _userContactRepositoryMock.Setup(x => x.GetById(contactViewModel.Id))
                .ReturnsAsync(contact);

            _mapperMock.Setup(x => x.Map<UserContact>(contactViewModel))
                .Returns(contact);

            _userContactRepositoryMock.Setup(x => x.Put(contact))
                .ReturnsAsync(contact);

            _mapperMock.Setup(x => x.Map<UserContactViewModel>(contact))
                .Returns(contactViewModel);

            // Act
            var result = await _userContactService.Put(contactViewModel);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(contactViewModel.Id, result.Data.Id);
            Assert.Equal(contactViewModel.Email, result.Data.Email);
            Assert.Equal(contactViewModel.PhoneNumber, result.Data.PhoneNumber);

            _userContactRepositoryMock.Verify(x => x.GetById(contactViewModel.Id), Times.Once);
            _userContactRepositoryMock.Verify(x => x.Put(contact), Times.Once);
            _mapperMock.Verify(x => x.Map<UserContact>(contactViewModel), Times.Once);
            _mapperMock.Verify(x => x.Map<UserContactViewModel>(contact), Times.Once);
        }


        [Fact]
        public async Task Put_VerifyRepositorySequence()
        {
            // Arrange
            var contactViewModel = new UserContactViewModel
            {
                Id = 1,
                Email = "user@email.com",
                PhoneNumber = "(11) 99999-9999"
            };

            var contact = new UserContact
            {
                Id = 1,
                Email = "user@email.com",
                PhoneNumber = "(11) 99999-9999",
                UserId = 1
            };

            var sequence = new List<string>();

            _userContactRepositoryMock.Setup(x => x.GetById(contactViewModel.Id))
                .Callback(() => sequence.Add("GetById"))
                .ReturnsAsync(contact);

            _mapperMock.Setup(x => x.Map<UserContact>(contactViewModel))
                .Returns(contact);

            _userContactRepositoryMock.Setup(x => x.Put(contact))
                .Callback(() => sequence.Add("Put"))
                .ReturnsAsync(contact);

            // Act
            var result = await _userContactService.Put(contactViewModel);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);

            Assert.Equal(new[] { "GetById", "Put" }, sequence);

            _userContactRepositoryMock.Verify(x => x.GetById(contactViewModel.Id), Times.Once);
            _userContactRepositoryMock.Verify(x => x.Put(contact), Times.Once);
        }

    }
}
