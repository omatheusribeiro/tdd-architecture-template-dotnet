using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace tdd_architecture_template_dotnet.Application.ViewModels.Users
{
    public class UserContactViewModel
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "It is necessary to enter the contact email.")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "It is necessary to enter the contact phone number.")]
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
