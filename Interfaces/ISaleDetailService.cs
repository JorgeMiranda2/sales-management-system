using SalesSystemApp.Models;

namespace SalesSystemApp.Interfaces
{
    public interface ISaleDetailService
    {
        Task<List<SaleDetail>> GetSaleDetailsAsync();
        Task<SaleDetail> GetSaleDetailByIdAsync(int id);
        Task CreateSaleDetailAsync(SaleDetail saleDetail);
        Task UpdateSaleDetailAsync(SaleDetail saleDetail);
        Task DeleteSaleDetailAsync(int id);
        bool SaleDetailExists(int id);
        Task<List<Product>> GetProductsAsync();
        Task<List<Sale>> GetSalesAsync();
    }
}
