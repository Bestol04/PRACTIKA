using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Models;

namespace PhotoGallery.Controllers
{
    public class GalleryController : Controller
    {
        private static List<Photo> photos = new List<Photo>()
        {
            new Photo
            {
                Id = 1,
                Title = "BMW",
                Url = "https://images.unsplash.com/photo-1555215695-3004980ad54e",
                DateUploaded = DateTime.Now
            },

            new Photo
            {
                Id = 2,
                Title = "Audi",
                Url = "https://images.unsplash.com/photo-1503376780353-7e6692767b70",
                DateUploaded = DateTime.Now
            }
        };

        // /Gallery
        public IActionResult Index()
        {
            return View(photos);
        }

        // /Gallery/Show/1
        [Route("Gallery/Show/{id}")]
        public IActionResult Show(int id)
        {
            var photo = photos.FirstOrDefault(p => p.Id == id);

            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Photo photo)
        {
            photo.Id = photos.Count + 1;

            photo.DateUploaded = DateTime.Now;

            photos.Add(photo);

            return RedirectToAction("Index");
        }
    }
}