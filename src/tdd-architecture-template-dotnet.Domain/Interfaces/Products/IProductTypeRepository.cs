using tdd_architecture_template_dotnet.Domain.Entities.Products;

namespace tdd_architecture_template_dotnet.Domain.Interfaces.Products
{
    public interface IProductTypeRepository
    {
        Task<IEnumerable<ProductType>> GetAll();
        Task<ProductType> GetById(int id);
        Task<ProductType> Put(ProductType product);
        Task<ProductType> Post(ProductType product);
        Task<ProductType> Delete(ProductType product);
    }
}
