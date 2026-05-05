using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CarRentalApp.Commands;
using CarRentalApp.Models;

namespace CarRentalApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Car> _allCars;
        private ObservableCollection<Car> _filteredCars;
        private Car _selectedCar;
        private string _selectedClassFilter;
        private int _minSeats;
        private Booking _currentBooking;

        public MainViewModel()
        {
            InitializeCars();
            InitializeFilters();
            InitializeCommands();

            CurrentBooking = new Booking
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1)
            };
        }

        #region Properties

        public ObservableCollection<Car> FilteredCars
        {
            get => _filteredCars;
            set
            {
                _filteredCars = value;
                OnPropertyChanged();
            }
        }

        public Car SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                OnPropertyChanged();
                CurrentBooking.SelectedCar = value;
            }
        }

        public Booking CurrentBooking
        {
            get => _currentBooking;
            set
            {
                _currentBooking = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> CarClasses { get; set; }

        public string SelectedClassFilter
        {
            get => _selectedClassFilter;
            set
            {
                _selectedClassFilter = value;
                OnPropertyChanged();
                ApplyFilters();
            }
        }

        public int MinSeats
        {
            get => _minSeats;
            set
            {
                _minSeats = value;
                OnPropertyChanged();
                ApplyFilters();
            }
        }

        #endregion

        #region Commands

        public ICommand BookCarCommand { get; private set; }
        public ICommand ResetFiltersCommand { get; private set; }

        #endregion

        #region Methods

        private void InitializeCars()
        {
            _allCars = new ObservableCollection<Car>
            {
                new Car { Id = 1, Brand = "Toyota", Model = "Camry", CarClass = "Эконом", PricePerDay = 2500, IsAvailable = true, Seats = 5 },
                new Car { Id = 2, Brand = "BMW", Model = "X5", CarClass = "Премиум", PricePerDay = 7500, IsAvailable = true, Seats = 7 },
                new Car { Id = 3, Brand = "Mercedes", Model = "E-Class", CarClass = "Бизнес", PricePerDay = 5500, IsAvailable = true, Seats = 5 },
                new Car { Id = 4, Brand = "Hyundai", Model = "Solaris", CarClass = "Эконом", PricePerDay = 1800, IsAvailable = true, Seats = 5 },
                new Car { Id = 5, Brand = "Audi", Model = "A8", CarClass = "Премиум", PricePerDay = 8500, IsAvailable = false, Seats = 5 },
                new Car { Id = 6, Brand = "Volkswagen", Model = "Polo", CarClass = "Эконом", PricePerDay = 2000, IsAvailable = true, Seats = 5 },
                new Car { Id = 7, Brand = "Land Rover", Model = "Range Rover", CarClass = "Премиум", PricePerDay = 12000, IsAvailable = true, Seats = 7 },
                new Car { Id = 8, Brand = "Skoda", Model = "Octavia", CarClass = "Бизнес", PricePerDay = 3000, IsAvailable = true, Seats = 5 }
            };

            FilteredCars = new ObservableCollection<Car>(_allCars.Where(c => c.IsAvailable));
        }

        private void InitializeFilters()
        {
            CarClasses = new ObservableCollection<string>
            {
                "Все",
                "Эконом",
                "Бизнес",
                "Премиум"
            };

            SelectedClassFilter = "Все";
            MinSeats = 0;
        }

        private void InitializeCommands()
        {
            BookCarCommand = new RelayCommand(
                execute: _ => BookCar(),
                canExecute: _ => CanBookCar()
            );

            ResetFiltersCommand = new RelayCommand(
                execute: _ => ResetFilters()
            );
        }

        private void ApplyFilters()
        {
            var filtered = _allCars.Where(c => c.IsAvailable);

            if (SelectedClassFilter != "Все" && !string.IsNullOrEmpty(SelectedClassFilter))
            {
                filtered = filtered.Where(c => c.CarClass == SelectedClassFilter);
            }

            if (MinSeats > 0)
            {
                filtered = filtered.Where(c => c.Seats >= MinSeats);
            }

            FilteredCars = new ObservableCollection<Car>(filtered);
        }

        private bool CanBookCar()
        {
            return SelectedCar != null
                   && SelectedCar.IsAvailable
                   && CurrentBooking.StartDate < CurrentBooking.EndDate
                   && CurrentBooking.StartDate >= DateTime.Today;
        }

        private void BookCar()
        {
            var result = MessageBox.Show(
                $"Забронировать автомобиль?\n\n" +
                $"Автомобиль: {SelectedCar.FullName}\n" +
                $"Класс: {SelectedCar.CarClass}\n" +
                $"Период: {CurrentBooking.StartDate:dd.MM.yyyy} - {CurrentBooking.EndDate:dd.MM.yyyy}\n" +
                $"Дней: {CurrentBooking.RentalDays}\n" +
                $"Стоимость: {CurrentBooking.TotalCost:N0} руб.",
                "Подтверждение бронирования",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                SelectedCar.IsAvailable = false;

                MessageBox.Show(
                    "Бронирование успешно оформлено!",
                    "Успех",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                ApplyFilters();
                SelectedCar = null;
            }
        }

        private void ResetFilters()
        {
            SelectedClassFilter = "Все";
            MinSeats = 0;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}