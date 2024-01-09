using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PictureSellingWeb.Models;
using PictureSellingWeb.Services;

namespace PictureSellingWeb.Controllers
{
    public class PictureController : Controller
    {
        private PictureContext _context;

        public PictureController(PictureContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var pictures = await _context.Pictures.Where(p => p.Locked != true).Include(p=>p.Category).ToListAsync();
            return View(pictures);
        }

        [HttpGet]
        [Route("[controller]/[action]/{pictureId}")]
        public async Task<IActionResult> Details(int pictureId)
        {
            Picture data = await _context.Pictures.Include(p => p.Category).Where(p => p.Id == pictureId).FirstOrDefaultAsync();
            return View(data);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Create(Picture? picture)
        {
            if (picture != null)
            {
                _context.Pictures.Add(picture);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<Category> data = await _context.Category.ToListAsync();
            return View(data);
        }

        [HttpGet]
        [Route("[controller]/[action]/{pictureId}")]
        public async Task<IActionResult> Edit(int pictureId)
        {
            PictureEdit model = new();
            model.Picture = await _context.Pictures.Where(p => p.Id == pictureId).FirstOrDefaultAsync();
            model.Categories = await _context.Category.ToListAsync();
            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Edit(Picture? picture)
        {
            if (picture != null)
            {
                if (await _context.Pictures.AnyAsync(p => p.Id == picture.Id))
                {
                    _context.Pictures.Update(picture);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete(Picture? picture)
        {
            if (picture != null)
            {
                if (await _context.Pictures.AnyAsync(p => p.Id == picture.Id))
                {
                    _context.Pictures.Remove(picture);
                    _context.SaveChanges();
                }
            }
            TempData["status"] = "Picture Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}