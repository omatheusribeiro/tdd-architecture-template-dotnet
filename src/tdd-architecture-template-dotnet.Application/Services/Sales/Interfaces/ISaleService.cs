using tdd_architecture_template_dotnet.Application.Models.Http;
using tdd_architecture_template_dotnet.Application.ViewModels.Sales;

namespace tdd_architecture_template_dotnet.Application.Services.Sales.Interfaces
{
    public interface ISaleService
    {
        Task<Result<IEnumerable<SaleViewModel>>> GetAll();
        Task<Result<SaleViewModel>> GetById(int id);
        Task<Result<SaleViewModel>> Put(SaleViewModel sale);
        Task<Result<SaleViewModel>> Post(SaleViewModel sale);
        Task<Result<SaleViewModel>> Delete(int saleId);
    }
}
