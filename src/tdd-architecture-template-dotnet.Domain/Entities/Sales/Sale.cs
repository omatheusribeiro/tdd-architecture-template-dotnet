using tdd_architecture_template_dotnet.Domain.Entities.Base;
using tdd_architecture_template_dotnet.Domain.Entities.Products;

namespace tdd_architecture_template_dotnet.Domain.Entities.Sales
{
    public class Sale : EntityBase
    {
        public decimal TotalValue { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
