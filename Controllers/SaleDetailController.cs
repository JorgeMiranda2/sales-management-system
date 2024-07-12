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

namespace SalesSystemApp.Controllers
{
    public class SaleDetailController : Controller
    {
        private readonly ISaleDetailService _saleDetailService;

        public SaleDetailController(ISaleDetailService saleDetailService)
        {
            _saleDetailService = saleDetailService;
        }

        // GET: SaleDetail
        public async Task<IActionResult> Index()
        {
            var saleDetails = await _saleDetailService.GetSaleDetailsAsync();
            return View(saleDetails);
        }

        // GET: SaleDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _saleDetailService.GetSaleDetailByIdAsync(id.Value);
            if (saleDetail == null)
            {
                return NotFound();
            }

            return View(saleDetail);
        }

        // GET: SaleDetail/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProductId"] = new SelectList(await _saleDetailService.GetProductsAsync(), "ProductId", "Description");
            ViewData["SaleId"] = new SelectList(await _saleDetailService.GetSalesAsync(), "SaleId", "SaleId");
            return View();
        }

        // POST: SaleDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleDetailId,SaleId,ProductId,Quantity,Price")] SaleDetail saleDetail)
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
                await _saleDetailService.CreateSaleDetailAsync(saleDetail);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(await _saleDetailService.GetProductsAsync(), "ProductId", "Description", saleDetail.ProductId);
            ViewData["SaleId"] = new SelectList(await _saleDetailService.GetSalesAsync(), "SaleId", "SaleId", saleDetail.SaleId);
            return View(saleDetail);
        }

        // GET: SaleDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _saleDetailService.GetSaleDetailByIdAsync(id.Value);
            if (saleDetail == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(await _saleDetailService.GetProductsAsync(), "ProductId", "Description", saleDetail.ProductId);
            ViewData["SaleId"] = new SelectList(await _saleDetailService.GetSalesAsync(), "SaleId", "SaleId", saleDetail.SaleId);
            return View(saleDetail);
        }

        // POST: SaleDetail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaleDetailId,SaleId,ProductId,Quantity,Price")] SaleDetail saleDetail)
        {
            if (id != saleDetail.SaleDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _saleDetailService.UpdateSaleDetailAsync(saleDetail);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_saleDetailService.SaleDetailExists(saleDetail.SaleDetailId))
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
            ViewData["ProductId"] = new SelectList(await _saleDetailService.GetProductsAsync(), "ProductId", "Description", saleDetail.ProductId);
            ViewData["SaleId"] = new SelectList(await _saleDetailService.GetSalesAsync(), "SaleId", "SaleId", saleDetail.SaleId);
            return View(saleDetail);
        }

        // GET: SaleDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _saleDetailService.GetSaleDetailByIdAsync(id.Value);
            if (saleDetail == null)
            {
                return NotFound();
            }

            return View(saleDetail);
        }

        // POST: SaleDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _saleDetailService.DeleteSaleDetailAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
