using AutoMapper;
using tdd_architecture_template_dotnet.Application.Models.Http;
using tdd_architecture_template_dotnet.Application.Services.Users.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Users;
using tdd_architecture_template_dotnet.Domain.Entities.Users;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Domain.Interfaces.Users;
using tdd_architecture_template_dotnet.Infrastructure.Singletons.Logger.Interfaces;

namespace tdd_architecture_template_dotnet.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserAddressRepository _userAddressRepository;
        private readonly IUserContactRepository _userContactRepository;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IUserAddressRepository userAddressRepository,
            IUserContactRepository contactRepository,
            ILoggerService loggerService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userAddressRepository = userAddressRepository;
            _userContactRepository = contactRepository;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UserViewModel>>> GetAll()
        {
            try
            {
                var users = await _userRepository.GetAll();

                if (users is null)
                {
                    _loggerService.LogInfo("Unable to identify users in the database.");
                    return Result<IEnumerable<UserViewModel>>.Fail("Unable to identify users in the database.", (int)HttpStatus.BadRequest);
                }

                var mapUsers = _mapper.Map<IEnumerable<UserViewModel>>(users);

                return Result<IEnumerable<UserViewModel>>.Ok(mapUsers);

            }
            catch (Exception ex)
            {
                _loggerService.LogError("There was an error listing users: " + ex.Message);
                return Result<IEnumerable<UserViewModel>>.Fail("There was an error listing users: " + ex.Message);
            }

        }

        public async Task<Result<UserViewModel>> GetById(int id)
        {
            try
            {
                var user = await _userRepository.GetById(id);

                if (user is null)
                {
                    _loggerService.LogInfo("User not found.");
                    return Result<UserViewModel>.Fail("User not found.", (int)HttpStatus.BadRequest);
                }

                var mapUser = _mapper.Map<UserViewModel>(user);

                return Result<UserViewModel>.Ok(mapUser);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("There was an error when searching for the user: " + ex.Message);
                return Result<UserViewModel>.Fail("There was an error when searching for the user: " + ex.Message);
            }

        }

        public async Task<Result<UserViewModel>> Put(UserViewModel user)
        {
            try
            {
                var mapUserModel = new UserViewModel();
                var mapAddressModel = new UserAddressViewModel();
                var mapContactModel = new UserContactViewModel();

                if (user is null)
                {
                    _loggerService.LogInfo("There is missing information to perform user change.");
                    return Result<UserViewModel>.Fail("There is missing information to perform user change.", (int)HttpStatus.BadRequest);
                }

                if (user.Address is null)
                {
                    _loggerService.LogInfo("There is missing information to perform user address change.");
                    return Result<UserViewModel>.Fail("There is missing information to perform user address change.", (int)HttpStatus.BadRequest);
                }

                if (user.Contact is null)
                {
                    _loggerService.LogInfo("There is missing information to perform user contact change.");
                    return Result<UserViewModel>.Fail("There is missing information to perform user contact change.", (int)HttpStatus.BadRequest);
                }

                var userEntity = await _userRepository.GetById(user.Id);

                if (userEntity is null)
                {
                    _loggerService.LogInfo("User not found.");
                    return Result<UserViewModel>.Fail("User not found.", (int)HttpStatus.BadRequest);
                }

                var mapUser = _mapper.Map<User>(user);

                var resultUser = await _userRepository.Put(mapUser);

                mapUserModel = _mapper.Map<UserViewModel>(resultUser);

                return Result<UserViewModel>.Ok(mapUserModel);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("There was an error editing the user: " + ex.Message);
                return Result<UserViewModel>.Fail("There was an error editing the user: " + ex.Message);
            }

        }

        public async Task<Result<UserViewModel>> Post(UserViewModel user)
        {
            try
            {
                if (user is null)
                {
                    _loggerService.LogInfo("There is missing information to perform user creation.");
                    return Result<UserViewModel>.Fail("There is missing information to perform user creation.", (int)HttpStatus.BadRequest);
                }

                if (user.Address is null)
                {
                    _loggerService.LogInfo("There is missing information to perform user address creation.");
                    return Result<UserViewModel>.Fail("There is missing information to perform user address creation.", (int)HttpStatus.BadRequest);
                }

                if (user.Contact is null)
                {
                    _loggerService.LogInfo("There is missing information to perform user contact creation.");
                    return Result<UserViewModel>.Fail("There is missing information to perform user address creation.", (int)HttpStatus.BadRequest);
                }

                var mapUser = _mapper.Map<User>(user);
                var mapAddress = _mapper.Map<UserAddress>(user.Address);
                var mapContact = _mapper.Map<UserContact>(user.Contact);

                var resultUser = await _userRepository.Post(mapUser);

                mapAddress.UserId = resultUser.Id;
                var resultAddress = await _userAddressRepository.Post(mapAddress);

                mapContact.UserId = resultUser.Id;
                var resultContact = await _userContactRepository.Post(mapContact);

                var mapUserModel = _mapper.Map<UserViewModel>(resultUser);

                return Result<UserViewModel>.Ok(mapUserModel);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("There was an error registering the user: " + ex.Message);
                return Result<UserViewModel>.Fail("There was an error registering the user: " + ex.Message);
            }

        }

        public async Task<Result<UserViewModel>> Delete(int userId)
        {
            try
            {
                if (userId is 0)
                {
                    _loggerService.LogInfo("There is missing information to delete the user.");
                    return Result<UserViewModel>.Fail("There is missing information to delete the user.", (int)HttpStatus.BadRequest);
                }

                var userEntity = await _userRepository.GetById(userId);

                if (userEntity is null)
                {
                    _loggerService.LogInfo("User not found.");
                    return Result<UserViewModel>.Fail("User not found.", (int)HttpStatus.BadRequest);
                }

                if (userEntity.Address is null)
                {
                    _loggerService.LogInfo("Address not found.");
                    return Result<UserViewModel>.Fail("Address not found.", (int)HttpStatus.BadRequest);
                }

                if (userEntity.Contact is null)
                {
                    _loggerService.LogInfo("Contact not found.");
                    return Result<UserViewModel>.Fail("Contact not found.", (int)HttpStatus.BadRequest);
                }

                var mapUser = _mapper.Map<User>(userEntity);

                var resultUser = await _userRepository.Delete(mapUser);

                var mapUserModel = _mapper.Map<UserViewModel>(resultUser);

                return Result<UserViewModel>.Ok(null);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("There was an error deleting the user: " + ex.Message);
                return Result<UserViewModel>.Fail("There was an error deleting the user: " + ex.Message);
            }

        }
    }
}
