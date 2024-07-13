using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using SalesSystemApp.Data;
using SalesSystemApp.ViewModels;
using SalesSystemApp.Interfaces;
using SalesSystemApp.Models;
using SalesSystemApp.Config;

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

        public async Task CreateNewSaleAsync(BuyingViewModel viewModel)
        {
            try
            {
                Console.Out.WriteLine("Entering in the service");

                // Validate the user and get the id
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == viewModel.UserEmail);
                if (user == null)
                {
                    Console.Out.WriteLine("User not found");
                    throw new Exception("User not found");
                }

                int userId = user.UserId;
                decimal totalBeforeTax = 0;

                // Crear la venta
                var sale = new Sale
                {
                    UserId = userId,
                    SaleDate = DateTime.Now,
                    TotalBeforeTax = 0, // inicialmente 0
                    TotalAfterTax = 0   // inicialmente 0
                };

                _context.Sales.Add(sale);
                await _context.SaveChangesAsync(); // guardar la venta en el contexto

                // Procesar los detalles de la venta
                foreach (var productItem in viewModel.ProductsInput)
                {
                    var product = await _context.Products.FindAsync(productItem.ProductId);
                    if (product == null)
                    {
                        throw new Exception($"Product with ID {productItem.ProductId} not found");
                    }

                    decimal itemTotal = product.Price * productItem.Quantity;
                    Console.Out.WriteLine($"Product: {productItem.ProductId}, Price: {product.Price}, Quantity: {productItem.Quantity} total: {itemTotal}" );

                    totalBeforeTax += itemTotal; // acumular el total antes de impuestos

                    var saleDetail = new SaleDetail
                    {
                        SaleId = sale.SaleId,
                        ProductId = product.ProductId,
                        Quantity = productItem.Quantity,
                        Price = itemTotal // precio total del producto
                    };

                    _context.SaleDetails.Add(saleDetail);
                }

                // Calcular el total después de impuestos
                sale.TotalBeforeTax = totalBeforeTax;
                sale.TotalAfterTax = totalBeforeTax * Constants.IvaRate; // aplicar el impuesto sobre el total antes de impuestos

                await _context.SaveChangesAsync(); // guardar nuevamente para reflejar los cambios en la base de datos
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine($"Error creating sale: {ex.Message}");
                throw; // lanzar la excepción para que el controlador o la capa superior puedan manejarla
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

        public List<SaleDto> GetSalesByUserEmail(string userEmail)
        {

            var sales = _context.Sales
                .Where(s => s.User.Email == userEmail) //Searching all coincidences
                .Select(s => new SaleDto  //transforming in saleDto
                {
                    SaleId = s.SaleId,
                    SaleDate = s.SaleDate,
                    TotalBeforeTax = s.TotalBeforeTax,
                    TotalAfterTax = s.TotalAfterTax,
                    SaleDetails = s.SaleDetails.Select(d => new SaleDetailDto //transforming in saleDetailDto
                    {
                        ProductName = d.Product.Name,
                        Quantity = d.Quantity,
                        Price = d.Price
                    }).ToList()
                }).ToList();

            return sales;
        }

    }
}
