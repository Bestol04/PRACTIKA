using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Data;
using PhotoGallery.Models;

namespace PhotoGallery.Controllers
{
    public class ImageController : Controller
    {
        private readonly AppDbContext _context;

        public ImageController(AppDbContext context)
        {
            _context = context;
        }

        // READ
        public async Task<IActionResult> Index()
        {
            var images = await _context.ImageItems.ToListAsync();

            return View(images);
        }

        // CREATE GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        public async Task<IActionResult> Create(ImageItem image)
        {
            if (!ModelState.IsValid)
            {
                return View(image);
            }

            image.UploadedAt = DateTime.Now;

            _context.ImageItems.Add(image);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // EDIT GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var image = await _context.ImageItems.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // EDIT POST
        [HttpPost]
        public async Task<IActionResult> Edit(ImageItem image)
        {
            if (!ModelState.IsValid)
            {
                return View(image);
            }

            _context.ImageItems.Update(image);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // DELETE GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _context.ImageItems.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.ImageItems.FindAsync(id);

            if (image != null)
            {
                _context.ImageItems.Remove(image);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}