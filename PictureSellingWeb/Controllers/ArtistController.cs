using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PictureSellingWeb.Models;
using PictureSellingWeb.Services;

namespace ArtistSellingWeb.Controllers
{
    public class ArtistController : Controller
    {
        private PictureContext _context;
        public ArtistController(PictureContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var Artists = await _context.Artists.ToListAsync();
            return View(Artists);
        }

        [HttpGet]
        [Route("[controller]/[action]/{ArtistId}")]

        public async Task<IActionResult> Details(int ArtistId)
        {
            Artist model = await _context.Artists.FirstOrDefaultAsync(p => p.Id == ArtistId);
            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]

        public async Task<IActionResult> Create(Artist? Artist)
        {
            if (Artist != null)
            {
                _context.Artists.Add(Artist);
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
        [Route("[controller]/[action]/{ArtistId}")]

        public async Task<IActionResult> Edit(int ArtistId)
        {
            Artist model = await _context.Artists.FirstOrDefaultAsync(p => p.Id == ArtistId);
            return View(model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]

        public async Task<IActionResult> Edit(Artist? artist)
        {
            if (artist != null)
            {
                if (await _context.Artists.AnyAsync(p => p.Id == artist.Id))
                {
                    _context.Artists.Update(artist);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Delete(Artist? artist)
        {
            if (artist != null)
            {
                if (await _context.Artists.AnyAsync(p => p.Id == artist.Id))
                {
                    _context.Artists.Remove(artist);
                    _context.SaveChanges();
                }
            }
            TempData["status"] = "Artist Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}