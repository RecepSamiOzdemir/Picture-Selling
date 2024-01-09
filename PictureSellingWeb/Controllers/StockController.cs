using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using PictureSellingWeb.Models;
using PictureSellingWeb.Services;

namespace StockSellingWeb.Controllers
{
    public class StockController : Controller
    {
        private PictureContext _context;

        public StockController(PictureContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var Stocks = await _context.Stock.Include(p=>p.Picture).ToListAsync();
            return View(Stocks);
        }

        [HttpGet]
        [Route("[controller]/[action]/{StockId}")]
        public async Task<IActionResult> Details(int StockId)
        {
            Stock model = await _context.Stock.Include(p => p.Picture).FirstOrDefaultAsync(p => p.PictureId == StockId);
            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Create(Stock? Stock)
        {
            if (Stock != null)
            {
                _context.Stock.Add(Stock);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var pictures = (from t in _context.Pictures
                where !_context.Stock.Any(pv => pv.PictureId== t.Id)
                select t).ToList();
            return View(pictures);
        }

        [HttpGet]
        [Route("[controller]/[action]/{StockId}")]
        public async Task<IActionResult> Edit(int StockId)
        {
            Stock model = await _context.Stock.Include(p=>p.Picture).FirstOrDefaultAsync(p => p.PictureId == StockId);
            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Edit(Stock? Stock)
        {
            if (Stock != null)
            {
                if (await _context.Stock.AnyAsync(p => p.Id == Stock.Id))
                {
                    var data = await _context.Stock.Where(p => p.Id == Stock.Id).FirstOrDefaultAsync();
                    _context.Stock.Attach(data);
                    data.StokCount = Stock.StokCount;
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete(Stock? Stock)
        {
            if( Stock != null )
            {
                if (await _context.Stock.AnyAsync(p => p.Id == Stock.Id))
                {
                    _context.Stock.Remove(Stock);
                    _context.SaveChanges();
                }
            }
            TempData["status"] = "Stock Removed Successfully";
            return RedirectToAction("Index");
        }
    }
}