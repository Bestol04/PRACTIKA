using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalApp.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        public string ClientName { get; set; }

        public DateTime RentDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}