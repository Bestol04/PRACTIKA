using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalApp.Models;

namespace CarRentalApp.Services
{
    public class RentalService
    {
        private readonly List<RentalModel> _rentals;
        private int _nextRentalId = 1;

        public RentalService()
        {
            _rentals = new List<RentalModel>();
        }

        // Асинхронная обработка заявки на аренду
        public async Task<RentalModel> ProcessRentalAsync(CarModel car, string clientName, int days)
        {
            // Создаем новую аренду со статусом "Обрабатывается"
            var rental = new RentalModel
            {
                Id = _nextRentalId++,
                Car = car,
                ClientName = clientName,
                RentalDate = DateTime.Now,
                DaysRented = days,
                Status = RentalStatus.Processing
            };

            _rentals.Add(rental);

            // Имитация обработки заявки (проверка документов, оплаты и т.д.)
            await Task.Delay(4000);

            // Обновляем статус и делаем автомобиль недоступным
            rental.Status = RentalStatus.Active;
            car.IsAvailable = false;

            return rental;
        }

        // Асинхронный возврат автомобиля
        public async Task<RentalModel> ProcessReturnAsync(RentalModel rental)
        {
            rental.Status = RentalStatus.Processing;

            // Имитация обработки возврата (проверка состояния автомобиля)
            await Task.Delay(3000);

            rental.ReturnDate = DateTime.Now;
            rental.Status = RentalStatus.Completed;
            rental.Car.IsAvailable = true;

            return rental;
        }

        // Получение всех аренд
        public List<RentalModel> GetAllRentals()
        {
            return _rentals.ToList();
        }

        // Получение активных аренд
        public List<RentalModel> GetActiveRentals()
        {
            return _rentals.Where(r => r.Status == RentalStatus.Active).ToList();
        }

        // Отмена аренды
        public async Task CancelRentalAsync(RentalModel rental)
        {
            await Task.Delay(1000);
            rental.Status = RentalStatus.Cancelled;
            rental.Car.IsAvailable = true;
        }
    }
}