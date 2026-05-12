using System.ComponentModel.DataAnnotations;

namespace CarRentalApp.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public bool IsRented { get; set; }
    }
}