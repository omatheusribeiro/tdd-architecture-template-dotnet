using tdd_architecture_template_dotnet.Domain.Entities.Sales;

namespace tdd_architecture_template_dotnet.Domain.Interfaces.Sales
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAll();
        Task<Sale> GetById(int id);
        Task<Sale> Put(Sale sale);
        Task<Sale> Post(Sale sale);
        Task<Sale> Delete(Sale sale);
    }
}
