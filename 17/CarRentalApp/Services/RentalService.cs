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
        private readonly JsonStorageService _storageService;
        private readonly NotificationService _notificationService;
        private int _nextRentalId = 1;
        private const string RentalsFileName = "rentals.json";

        public RentalService(JsonStorageService storageService, NotificationService notificationService)
        {
            _rentals = new List<RentalModel>();
            _storageService = storageService;
            _notificationService = notificationService;
        }

        public async Task InitializeAsync()
        {
            var rentals = await _storageService.LoadDataAsync<List<RentalModel>>(RentalsFileName);
            if (rentals != null)
            {
                _rentals.Clear();
                _rentals.AddRange(rentals);
                _nextRentalId = rentals.Any() ? rentals.Max(r => r.Id) + 1 : 1;
            }
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
            await SaveRentalsAsync();

            // Имитация обработки заявки (проверка документов, оплаты и т.д.)
            await Task.Delay(4000);

            // Обновляем статус и делаем автомобиль недоступным
            rental.Status = RentalStatus.Active;
            car.IsAvailable = false;
            await SaveRentalsAsync();

            // Отправляем уведомление
            _notificationService.SendNotification(
                $"Новая аренда: {car.FullName} для {clientName} на {days} дн. Стоимость: {rental.TotalCost:C}");

            return rental;
        }

        // Асинхронный возврат автомобиля
        public async Task<RentalModel> ProcessReturnAsync(RentalModel rental)
        {
            rental.Status = RentalStatus.Processing;
            await SaveRentalsAsync();

            // Имитация обработки возврата (проверка состояния автомобиля)
            await Task.Delay(3000);

            rental.ReturnDate = DateTime.Now;
            rental.Status = RentalStatus.Completed;
            rental.Car!.IsAvailable = true;
            await SaveRentalsAsync();

            // Отправляем уведомление
            _notificationService.SendNotification(
                $"Возврат: {rental.Car.FullName} от {rental.ClientName}");

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

        // Получение аренд клиента
        public List<RentalModel> GetRentalsByClient(string clientName)
        {
            return _rentals.Where(r => r.ClientName.Equals(clientName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Отмена аренды
        public async Task CancelRentalAsync(RentalModel rental)
        {
            await Task.Delay(1000);
            rental.Status = RentalStatus.Cancelled;
            rental.Car!.IsAvailable = true;
            await SaveRentalsAsync();

            _notificationService.SendNotification(
                $"Отмена аренды: {rental.Car.FullName}");
        }

        private async Task SaveRentalsAsync()
        {
            await _storageService.SaveDataAsync(RentalsFileName, _rentals);
        }
    }
}