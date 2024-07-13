using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesSystemApp.Data;
using SalesSystemApp.Interfaces;
using SalesSystemApp.Models;
using SalesSystemApp.Services;
using SalesSystemApp.ViewModels;
using SalesSystemApp.ViewModels;

namespace SalesSystemApp.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;
        private readonly AppDbContext _context;

        public SaleController(ISaleService saleService, IProductService productService, AppDbContext context)
        {
            _saleService = saleService;
            _productService = productService;
            _context = context;
        }


        public async Task<IActionResult> Cart()
        {
            var products = await _productService.GetProductsAsync();
            var productViewModels = products.Select(p => new BuyingViewModel.ProductViewModel
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description
            }).ToList();

            var model = new BuyingViewModel
            {
                Products = productViewModels,
                UserEmail = "" 
            };

            return View(model);
        }


        // GET: Sale
        public async Task<IActionResult> Index()
        {
            return View(await _saleService.GetSalesAsync());
        }

   

        // GET: Sale/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _saleService.GetSaleByIdAsync(id.Value);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sale/Create
        public async Task<IActionResult> Create()
        {
            ViewData["UserId"] = new SelectList(await _saleService.GetUsersAsync(), "UserId", "Email");
            return View();
        }

        // POST: Sale/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleId,UserId,SaleDate,TotalBeforeTax,TotalAfterTax")] Sale sale)
        {

            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.Out.WriteLine(error.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                await _saleService.CreateSaleAsync(sale);
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(await _saleService.GetUsersAsync(), "UserId", "Email", sale.UserId);
            return View(sale);
        }

        // GET: Sale/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _saleService.GetSaleByIdAsync(id.Value);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(await _saleService.GetUsersAsync(), "UserId", "Email", sale.UserId);
            return View(sale);
        }

        // POST: Sale/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleId,UserId,SaleDate,TotalBeforeTax,TotalAfterTax")] Sale sale)
        {
            if (id != sale.SaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _saleService.UpdateSaleAsync(sale);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_saleService.SaleExists(sale.SaleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(await _saleService.GetUsersAsync(), "UserId", "Email", sale.UserId);
            return View(sale);
        }

        // GET: Sale/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _saleService.GetSaleByIdAsync(id.Value);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewSale(BuyingViewModel model)
        {
          
            foreach (var productInput in model.ProductsInput)
            {
                Console.WriteLine($"ProductId: {productInput.ProductId}, Quantity: {productInput.Quantity}");
            }




            if (!ModelState.IsValid)
            {
                Console.Out.WriteLine("invalid type");
                return BadRequest(ModelState);
            }

            try
            {
                await _saleService.CreateNewSaleAsync(model);
                return RedirectToAction("Index", "Home"); ;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            } 

        }


        // POST: Sale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _saleService.DeleteSaleAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult SearchSalesByEmail(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail))
            {
                return View(new SearchSalesViewModel());
            }

            var sales = _saleService.GetSalesByUserEmail(userEmail);
            var viewModel = new SearchSalesViewModel
            {
                UserEmail = userEmail,
                Sales = sales
            };

            return View(viewModel);
        }



    }

}

   



