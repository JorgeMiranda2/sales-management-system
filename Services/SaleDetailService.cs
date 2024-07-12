using Microsoft.EntityFrameworkCore;
using SalesSystemApp.Data;
using SalesSystemApp.Interfaces;
using SalesSystemApp.Models;

namespace SalesSystemApp.Services
{
    public class SaleDetailService : ISaleDetailService
    {
        private readonly AppDbContext _context;

        public SaleDetailService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SaleDetail>> GetSaleDetailsAsync()
        {
            return await _context.SaleDetails
                .Include(s => s.Product)
                .Include(s => s.Sale)
                .ToListAsync();
        }

        public async Task<SaleDetail> GetSaleDetailByIdAsync(int id)
        {
            return await _context.SaleDetails
                .Include(s => s.Product)
                .Include(s => s.Sale)
                .FirstOrDefaultAsync(m => m.SaleDetailId == id);
        }

        public async Task CreateSaleDetailAsync(SaleDetail saleDetail)
        {
            _context.Add(saleDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSaleDetailAsync(SaleDetail saleDetail)
        {
            _context.Update(saleDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSaleDetailAsync(int id)
        {
            var saleDetail = await _context.SaleDetails.FindAsync(id);
            if (saleDetail != null)
            {
                _context.SaleDetails.Remove(saleDetail);
                await _context.SaveChangesAsync();
            }
        }

        public bool SaleDetailExists(int id)
        {
            return _context.SaleDetails.Any(e => e.SaleDetailId == id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<List<Sale>> GetSalesAsync()
        {
            return await _context.Sales.ToListAsync();
        }
    }
}
