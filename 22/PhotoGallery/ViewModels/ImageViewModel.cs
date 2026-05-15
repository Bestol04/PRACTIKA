using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.ViewModels
{
    public class ImageViewModel
    {
        [Required(ErrorMessage = "Введите название")]
        [StringLength(50, ErrorMessage = "Максимум 50 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите URL")]
        [Url(ErrorMessage = "Некорректный URL")]
        public string Url { get; set; }

        [StringLength(200, ErrorMessage = "Максимум 200 символов")]
        public string Description { get; set; }
    }
}