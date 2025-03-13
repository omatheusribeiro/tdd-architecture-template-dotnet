using tdd_architecture_template_dotnet.Application.Models.Http;
using tdd_architecture_template_dotnet.Application.ViewModels.Products;

namespace tdd_architecture_template_dotnet.Application.Services.Products.Interfaces
{
    public interface IProductTypeService
    {
        Task<Result<ProductTypeViewModel>> Put(ProductTypeViewModel product);
        Task<Result<ProductTypeViewModel>> Post(ProductTypeViewModel product);
    }
}
