using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace tdd_architecture_template_dotnet.Application.ViewModels.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "It is necessary to enter the user first name.")]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "It is necessary to enter the user second name.")]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "It is necessary to provide the user document.")]
        [JsonPropertyName("document")]
        public string Document { get; set; } = string.Empty;

        [JsonPropertyName("address")]
        public UserAddressViewModel Address { get; set; } = new UserAddressViewModel();


        [JsonPropertyName("contact")]
        public UserContactViewModel Contact { get; set; } = new UserContactViewModel();
    }
}
