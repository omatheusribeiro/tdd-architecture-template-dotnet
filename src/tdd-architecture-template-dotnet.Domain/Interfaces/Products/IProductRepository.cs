using tdd_architecture_template_dotnet.Domain.Entities.Products;

namespace tdd_architecture_template_dotnet.Domain.Interfaces.Products
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> Put(Product product);
        Task<Product> Post(Product product);
        Task<Product> Delete(Product product);
    }
}
