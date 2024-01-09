using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PictureSellingWeb.Models;
using PictureSellingWeb.Services;

namespace CategorySellingWeb.Controllers
{
    public class CategoryController : Controller
    {
        private PictureContext _context;

        public CategoryController(PictureContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var Categorys = await _context.Category.ToListAsync();
            return View(Categorys);
        }

        [HttpGet]
        [Route("[controller]/[action]/{CategoryId}")]
        public async Task<IActionResult> Details(int CategoryId)
        {
            Category model = await _context.Category.FirstOrDefaultAsync(p => p.Id == CategoryId);
            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Create(Category? Category)
        {
            if (Category != null)
            {
                _context.Category.Add(Category);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpGet]
        [Route("[controller]/[action]/{CategoryId}")]
        public async Task<IActionResult> Edit(int CategoryId)
        {
            Category model = await _context.Category.FirstOrDefaultAsync(p => p.Id == CategoryId);
            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Edit(Category? Category)
        {
            if (Category != null)
            {
                if (await _context.Category.AnyAsync(p => p.Id == Category.Id))
                {
                    _context.Category.Update(Category);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete(Category? Category)
        {
            if (Category != null)
            {
                if (await _context.Category.AnyAsync(p => p.Id == Category.Id))
                {
                    _context.Category.Remove(Category);
                    _context.SaveChanges();
                }
            }
            TempData["status"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }        
    }
}