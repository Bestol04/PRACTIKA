using PhotoGallery.Models;
using PhotoGallery.ViewModels;

namespace PhotoGallery.Services
{
    public class ImageService : IImageService
    {
        private static List<Photo> images = new List<Photo>()
        {
            new Photo
            {
                Id = 1,
                Title = "BMW",
                Url = "https://images.unsplash.com/photo-1555215695-3004980ad54e",
                DateUploaded = DateTime.Now
            }
        };

        public List<Photo> GetAllImages()
        {
            return images;
        }

        public Photo GetImageById(int id)
        {
            return images.FirstOrDefault(i => i.Id == id);
        }

        public void AddImage(ImageViewModel model)
        {
            Photo photo = new Photo()
            {
                Id = images.Count + 1,
                Title = model.Title,
                Url = model.Url,
                DateUploaded = DateTime.Now
            };

            images.Add(photo);
        }
    }
}