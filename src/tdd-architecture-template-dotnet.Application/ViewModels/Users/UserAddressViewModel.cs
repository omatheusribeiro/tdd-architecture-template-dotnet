using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace tdd_architecture_template_dotnet.Application.ViewModels.Users
{
    public class UserAddressViewModel
    { 
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "It is necessary to enter the address street.")]
        [JsonPropertyName("street")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "It is necessary to enter the address number.")]
        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("complement")]
        public string Complement { get; set; } = string.Empty;

        [Required(ErrorMessage = "It is necessary to provide the address neighborhood.")]
        [JsonPropertyName("neighborhood")]
        public string Neighborhood { get; set; } = string.Empty;

        [Required(ErrorMessage = "It is necessary to provide the address city.")]
        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "It is necessary to provide the address state.")]
        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "It is necessary to provide the address country.")]
        [JsonPropertyName("country")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "It is necessary to provide the address zip code.")]
        [JsonPropertyName("zipCode")]
        public string ZipCode { get; set; } = string.Empty;
    }
}
