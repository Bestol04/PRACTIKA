using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.Models
{
    public class ImageItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Path { get; set; }

        public DateTime UploadedAt { get; set; }
    }
}