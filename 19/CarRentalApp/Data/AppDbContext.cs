using Microsoft.EntityFrameworkCore;
using CarRentalApp.Models;

namespace CarRentalApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=cars.db");
        }
    }
}