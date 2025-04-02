using tdd_architecture_template_dotnet.Application.Models.Http;
using tdd_architecture_template_dotnet.Application.Services.Login.Interfaces;
using tdd_architecture_template_dotnet.Domain.Enums;
using tdd_architecture_template_dotnet.Domain.Interfaces.Users;
using tdd_architecture_template_dotnet.Infrastructure.Authentication;
using tdd_architecture_template_dotnet.Infrastructure.Singletons.Logger.Interfaces;

namespace tdd_architecture_template_dotnet.Application.Services.Login
{
    public class LoginService : ILoginService
    {
        private IUserContactRepository _userContactRepository;
        private ILoggerService _loggerService;
        private readonly TokenGenerator _tokenGenerator;

        public LoginService(IUserContactRepository userContactRepository, ILoggerService loggerService, TokenGenerator tokenGenerator)
        {
            _userContactRepository = userContactRepository;
            _loggerService = loggerService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Result<string>> GetLogin(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    _loggerService.LogInfo("Unable to identify email in the database.");
                    return Result<string>.Fail("Unable to identify email in the database.", (int)HttpStatus.BadRequest);
                };

                var result = await _userContactRepository.GetByEmail(email);

                if (result is null)
                {
                    _loggerService.LogInfo("Unable to identify email in the database.");
                    return Result<string>.Fail("Unable to identify email in the database.", (int)HttpStatus.BadRequest);
                }

                var token = _tokenGenerator.GenerateToken(result.Email);

                return Result<string>.Ok(token);

            }
            catch (Exception ex)
            {
                _loggerService.LogError("There was an error generating the token: " + ex.Message);
                return Result<string>.Fail("There was an error generating the token: " + ex.Message);
            }

        }
    }
}
