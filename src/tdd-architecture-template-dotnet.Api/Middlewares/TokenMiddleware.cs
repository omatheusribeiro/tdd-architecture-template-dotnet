using tdd_architecture_template_dotnet.Infrastructure.Authentication;

namespace tdd_architecture_template_dotnet.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenValidation _tokenValidation;
        private readonly ILogger<TokenMiddleware> _logger;

        public TokenMiddleware(RequestDelegate next, TokenValidation tokenValidation, ILogger<TokenMiddleware> logger)
        {
            _next = next;
            _tokenValidation = tokenValidation;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (!string.IsNullOrEmpty(token))
                {
                    var principal = _tokenValidation.ValidateToken(token);

                    if (principal != null)
                    {
                        context.User = principal;
                    }
                    else
                    {
                        _logger.LogWarning("Invalid or expired token.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing JWT token.");
            }

            await _next(context);
        }
    }
}
