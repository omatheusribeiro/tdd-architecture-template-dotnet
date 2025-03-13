using AutoMapper;
using tdd_architecture_template_dotnet.Application.Models.Http;
using tdd_architecture_template_dotnet.Application.Services.Users.Interfaces;
using tdd_architecture_template_dotnet.Application.ViewModels.Users;
using tdd_architecture_template_dotnet.Domain.Entities.Users;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Domain.Interfaces.Users;

namespace tdd_architecture_template_dotnet.Application.Services.Users
{
    public class UserContactService : IUserContactService
    {
        private readonly IUserContactRepository _userContactRepository;
        private readonly IMapper _mapper;

        public UserContactService(IMapper mapper, IUserContactRepository userContactRepository)
        {
            _userContactRepository = userContactRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserContactViewModel>> Put(UserContactViewModel contact)
        {
            try
            {
                var userContact = await _userContactRepository.GetById(contact.Id);

                if (userContact is null)
                    return Result<UserContactViewModel>.Fail("contact not found.", (int)HttpStatus.BadRequest);

                var mapContact = _mapper.Map<UserContact>(contact);

                var result = await _userContactRepository.Put(mapContact);

                var mapContactModel = _mapper.Map<UserContactViewModel>(result);

                return Result<UserContactViewModel>.Ok(mapContactModel);
            }
            catch (Exception ex)
            {
                return Result<UserContactViewModel>.Fail("There was an error editing the user contact: " + ex.Message, (int)HttpStatus.BadRequest);
            }

        }
    }
}
