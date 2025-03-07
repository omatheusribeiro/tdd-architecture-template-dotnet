using tdd_architecture_template_dotnet.Domain.Entities.Base;

namespace tdd_architecture_template_dotnet.Domain.Entities.Products
{
    public class Product : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public int ProductTypeId { get; set; }
        public virtual ProductType Type { get; set; }
        public virtual ICollection<Sale> Sale { get; set; }

        public Product()
        {
            Sale = new HashSet<Sale>();
        }
    }
}
