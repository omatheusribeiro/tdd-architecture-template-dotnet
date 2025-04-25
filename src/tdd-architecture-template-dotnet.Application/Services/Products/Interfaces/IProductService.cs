using tdd_architecture_template_dotnet.Application.Models.Http;
using tdd_architecture_template_dotnet.Application.ViewModels.Products;

namespace tdd_architecture_template_dotnet.Application.Services.Products.Interfaces
{
    public interface IProductService
    {
        Task<Result<IEnumerable<ProductViewModel>>> GetAll();
        Task<Result<ProductViewModel>> GetById(int id);
        Task<Result<ProductViewModel>> Put(ProductViewModel product);
        Task<Result<ProductViewModel>> Post(ProductViewModel product);
        Task<Result<ProductViewModel>> Delete(int productId);
    }
}
