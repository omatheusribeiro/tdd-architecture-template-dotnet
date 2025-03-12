using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace tdd_architecture_template_dotnet.Application.ViewModels.Products
{
    public class ProductTypeViewModel
    { 
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "It is necessary to enter the product name.")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "It is necessary to enter the product name.")]
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
    }
}
