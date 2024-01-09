using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PictureSellingWeb.Models;
using PictureSellingWeb.Services;

namespace CustomerSellingWeb.Controllers
{
    public class CustomerController : Controller
    {
        private PictureContext _context;

        public CustomerController(PictureContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var Customers = await _context.Customer.ToListAsync();
            return View(Customers);
        }

        [HttpGet]
        [Route("[controller]/[action]/{CustomerId}")]
        public async Task<IActionResult> Details(int CustomerId)
        {
            Customer model = await _context.Customer.FirstOrDefaultAsync(p => p.Id == CustomerId);
            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Create(Customer? Customer)
        {
            if (Customer != null)
            {
                _context.Customer.Add(Customer);
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
        [Route("[controller]/[action]/{CustomerId}")]
        public async Task<IActionResult> Edit(int CustomerId)
        {
            Customer model = await _context.Customer.FirstOrDefaultAsync(p => p.Id == CustomerId);
            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Edit(Customer? Customer)
        {
            if (Customer != null)
            {
                if (await _context.Customer.AnyAsync(p => p.Id == Customer.Id))
                {
                    _context.Customer.Update(Customer);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete(Customer? Customer)
        {
            if (Customer != null)
            {
                if (await _context.Customer.AnyAsync(p => p.Id == Customer.Id))
                {
                    _context.Customer.Remove(Customer);
                    _context.SaveChanges();
                }
            }
            TempData["status"] = "Customer Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}