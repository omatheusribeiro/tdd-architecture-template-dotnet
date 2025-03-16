using AutoFixture;
using AutoMapper;
using Moq;
using tdd_architecture_template_dotnet.Application.Services.Users;
using tdd_architecture_template_dotnet.Application.ViewModels.Users;
using tdd_architecture_template_dotnet.Domain.Entities.Users;
using tdd_architecture_template_dotnet.Domain.Interfaces.Users;

namespace tdd_architecture_template_dotnet.Tests.Services.Users
{
    public class UserAddressServiceTests
    {
        private readonly Mock<IUserAddressRepository> _userAddressRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserAddresService _userAddressService;
        private readonly Fixture _fixture;

        public UserAddressServiceTests()
        {
            _userAddressRepositoryMock = new Mock<IUserAddressRepository>();
            _mapperMock = new Mock<IMapper>();
            _userAddressService = new UserAddresService(_mapperMock.Object, _userAddressRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Put_WithValidAddress_ReturnsSuccess()
        {
            // Arrange
            var addressViewModel = new UserAddressViewModel
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
                ZipCode = "01000-000"
            };

            _userAddressRepositoryMock.Setup(x => x.GetById(addressViewModel.Id))
                .ReturnsAsync(address);

            _mapperMock.Setup(x => x.Map<UserAddress>(addressViewModel))
                .Returns(address);

            _userAddressRepositoryMock.Setup(x => x.Put(address))
                .ReturnsAsync(address);

            _mapperMock.Setup(x => x.Map<UserAddressViewModel>(address))
                .Returns(addressViewModel);

            // Act
            var result = await _userAddressService.Put(addressViewModel);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(addressViewModel.Id, result.Data.Id);
            Assert.Equal(addressViewModel.Street, result.Data.Street);
            Assert.Equal(addressViewModel.Number, result.Data.Number);
            Assert.Equal(addressViewModel.Complement, result.Data.Complement);
            Assert.Equal(addressViewModel.Neighborhood, result.Data.Neighborhood);
            Assert.Equal(addressViewModel.City, result.Data.City);
            Assert.Equal(addressViewModel.State, result.Data.State);
            Assert.Equal(addressViewModel.Country, result.Data.Country);
            Assert.Equal(addressViewModel.ZipCode, result.Data.ZipCode);

            _userAddressRepositoryMock.Verify(x => x.GetById(addressViewModel.Id), Times.Once);
            _userAddressRepositoryMock.Verify(x => x.Put(address), Times.Once);
        }


        [Fact]
        public async Task Put_WithNonExistentAddress_ReturnsFail()
        {
            // Arrange
            var addressViewModel = _fixture.Create<UserAddressViewModel>();

            _userAddressRepositoryMock.Setup(x => x.GetById(addressViewModel.Id))
                .ReturnsAsync((UserAddress)null);

            // Act
            var result = await _userAddressService.Put(addressViewModel);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("address not found.", result.Message);
            _userAddressRepositoryMock.Verify(x => x.Put(It.IsAny<UserAddress>()), Times.Never);
        }

        [Fact]
        public async Task Put_WhenExceptionOccurs_ReturnsFail()
        {
            // Arrange
            var addressViewModel = new UserAddressViewModel
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
                ZipCode = "01000-000"
            };

            var exceptionMessage = "Database error";

            _userAddressRepositoryMock.Setup(x => x.GetById(addressViewModel.Id))
                .ReturnsAsync(address);

            _mapperMock.Setup(x => x.Map<UserAddress>(addressViewModel))
                .Returns(address);

            _userAddressRepositoryMock.Setup(x => x.Put(address))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _userAddressService.Put(addressViewModel);

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(result.Message);
            Assert.Contains(exceptionMessage, result.Message);
        }

        [Fact]
        public async Task Put_WithNullAddress_ReturnsFail()
        {
            // Arrange
            UserAddressViewModel addressViewModel = null;

            // Act
            var result = await _userAddressService.Put(addressViewModel);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Object reference not set to an instance of an object", result.Message);
            _userAddressRepositoryMock.Verify(x => x.Put(It.IsAny<UserAddress>()), Times.Never);
        }

        [Fact]
        public async Task Put_VerifyMappingCalls()
        {
            // Arrange
            var addressViewModel = new UserAddressViewModel
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
                ZipCode = "01000-000"
            };

            _userAddressRepositoryMock.Setup(x => x.GetById(addressViewModel.Id))
                .ReturnsAsync(address);

            _mapperMock.Setup(x => x.Map<UserAddress>(addressViewModel))
                .Returns(address);

            _userAddressRepositoryMock.Setup(x => x.Put(address))
                .ReturnsAsync(address);

            _mapperMock.Setup(x => x.Map<UserAddressViewModel>(address))
                .Returns(addressViewModel);

            // Act
            await _userAddressService.Put(addressViewModel);

            // Assert
            _mapperMock.Verify(x => x.Map<UserAddress>(addressViewModel), Times.Once);
            _mapperMock.Verify(x => x.Map<UserAddressViewModel>(address), Times.Once);
        }

    }
}
