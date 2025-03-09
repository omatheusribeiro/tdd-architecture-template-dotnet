namespace tdd_architecture_template_dotnet.Infrastructure.Authentication
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpirationHours { get; set; } = 2;
    }
}
