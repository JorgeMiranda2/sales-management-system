using SalesSystemApp.ViewModels;
using SalesSystemApp.Models;
using SalesSystemApp.ViewModels;

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

        Task CreateNewSaleAsync(BuyingViewModel viewModel);
        List<SaleDto> GetSalesByUserEmail(string userEmail);
    }
}
