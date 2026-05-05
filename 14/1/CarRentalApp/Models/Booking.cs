using System;

namespace CarRentalApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public Customer Customer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        public int Days => (EndDate - StartDate).Days;
    }
}