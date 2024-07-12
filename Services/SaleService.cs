using Microsoft.EntityFrameworkCore;
using SalesSystemApp.Data;
using SalesSystemApp.Interfaces;
using SalesSystemApp.Models;

namespace SalesSystemApp.Services
{

    // Services/SaleService.cs
    public class SaleService : ISaleService
    {
        private readonly AppDbContext _context;

        public SaleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> GetSalesAsync()
        {
            return await _context.Sales.Include(s => s.User).ToListAsync();
        }

        public async Task<Sale> GetSaleByIdAsync(int id)
        {
            return await _context.Sales.Include(s => s.User).FirstOrDefaultAsync(m => m.SaleId == id);
        }

        public async Task CreateSaleAsync(Sale sale)
        {
            _context.Add(sale);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSaleAsync(Sale sale)
        {
            _context.Update(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSaleAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }

        public bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.SaleId == id);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
