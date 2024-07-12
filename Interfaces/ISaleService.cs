using SalesSystemApp.Models;

namespace SalesSystemApp.Interfaces
{
    public interface ISaleService
    {
        Task<List<Sale>> GetSalesAsync();
        Task<Sale> GetSaleByIdAsync(int id);
        Task CreateSaleAsync(Sale sale);
        Task UpdateSaleAsync(Sale sale);
        Task DeleteSaleAsync(int id);
        bool SaleExists(int id);
        Task<List<User>> GetUsersAsync();
    }
}
