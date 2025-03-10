using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace tdd_architecture_template_dotnet.Application.ViewModels.Products
{
    public class ProductViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "It is necessary to enter the product name.")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "It is necessary to enter the product name.")]
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "It is necessary to provide the product value.")]
        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "It is necessary to provide the product type id.")]
        [JsonPropertyName("productTypeId")]
        public int ProductTypeId { get; set; }
    }
}
