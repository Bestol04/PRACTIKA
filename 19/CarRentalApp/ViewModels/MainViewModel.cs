using System.Collections.ObjectModel;
using System.Windows.Input;
using CarRentalApp.Models;
using CarRentalApp.Repositories;
using CarRentalApp.Data;
using CarRentalApp.Commands;

namespace CarRentalApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly AppDbContext _context;
        private readonly CarRepository _carRepository;

        private readonly RentalRepository _rentalRepository;

        public ObservableCollection<Car> Cars { get; set; }

        public Car SelectedCar { get; set; }

        public ICommand RentCarCommand { get; }

        public ICommand ReturnCarCommand { get; }

        public MainViewModel()
        {
            _context = new AppDbContext();

            _context.Database.EnsureCreated();

            _carRepository = new CarRepository(_context);

            _rentalRepository = new RentalRepository(_context);

            Cars = new ObservableCollection<Car>();

            LoadCars();

            RentCarCommand = new RelayCommand(async _ => await RentCar());

            ReturnCarCommand = new RelayCommand(async _ => await ReturnCar());
        }

        private async void LoadCars()
        {
            var cars = await _carRepository.GetAllCarsAsync();

            Cars.Clear();

            foreach (var car in cars)
            {
                Cars.Add(car);
            }
        }

        private async Task RentCar()
        {
            if (SelectedCar == null)
                return;

            SelectedCar.IsRented = true;

            Rental rental = new Rental()
            {
                CarId = SelectedCar.Id,
                ClientName = "Клиент",
                RentDate = DateTime.Now
            };

            await _rentalRepository.AddRentalAsync(rental);

            await _context.SaveChangesAsync();
        }

        private async Task ReturnCar()
        {
            if (SelectedCar == null)
                return;

            SelectedCar.IsRented = false;
            await _context.SaveChangesAsync();
        }
    }
}