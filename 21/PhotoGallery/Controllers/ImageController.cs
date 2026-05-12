using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Services;
using PhotoGallery.ViewModels;

namespace PhotoGallery.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        // DI
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            var images = _imageService.GetAllImages();

            return View(images);
        }

        [Route("Image/Show/{id}")]
        public IActionResult Show(int id)
        {
            var image = _imageService.GetImageById(id);

            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ImageViewModel model)
        {
            // Валидация
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _imageService.AddImage(model);

            // TempData
            TempData["Message"] = "Изображение добавлено";

            return RedirectToAction("Index");
        }
    }
}