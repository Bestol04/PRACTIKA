using PhotoGallery.Models;
using PhotoGallery.ViewModels;

namespace PhotoGallery.Services
{
    public interface IImageService
    {
        List<Photo> GetAllImages();

        Photo GetImageById(int id);

        void AddImage(ImageViewModel model);
    }
}