using AutoMapper;
using tdd_architecture_template_dotnet.Application.Models.Http;
using tdd_architecture_template_dotnet.Application.Services.Users.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Users;
using tdd_architecture_template_dotnet.Domain.Entities.Users;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Domain.Interfaces.Users;

namespace tdd_architecture_template_dotnet.Application.Services.Users
{
    public class UserAddresService : IUserAddressService
    {
        private readonly IUserAddressRepository _userAddressRepository;
        private readonly IMapper _mapper;

        public UserAddresService(IMapper mapper, IUserAddressRepository userAddressRepository)
        {
            _userAddressRepository = userAddressRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserAddressViewModel>> Put(UserAddressViewModel address)
        {
            try
            {
                var userAddress = await _userAddressRepository.GetById(address.Id);

                if (userAddress is null)
                    return Result<UserAddressViewModel>.Fail("address not found.", (int)HttpStatus.BadRequest);

                var mapAddress = _mapper.Map<UserAddress>(address);

                var result = await _userAddressRepository.Put(mapAddress);

                var mapAddressModel = _mapper.Map<UserAddressViewModel>(result);

                return Result<UserAddressViewModel>.Ok(mapAddressModel);
            }
            catch (Exception ex)
            {
                return Result<UserAddressViewModel>.Fail("There was an error editing the user address: " + ex.Message, (int)HttpStatus.BadRequest);
            }

        }
    }
}
