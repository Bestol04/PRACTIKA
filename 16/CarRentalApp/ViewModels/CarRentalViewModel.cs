using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CarRentalApp.Commands;
using CarRentalApp.Models;
using CarRentalApp.Services;

namespace CarRentalApp.ViewModels
{
    public class CarRentalViewModel : INotifyPropertyChanged
    {
        private readonly RentalService _rentalService;
        private ObservableCollection<CarModel> _availableCars;
        private ObservableCollection<RentalModel> _activeRentals;
        private CarModel? _selectedCar;
        private RentalModel? _selectedRental;
        private string _clientName;
        private int _rentalDays;
        private string _statusMessage;
        private bool _isProcessing;

        public CarRentalViewModel()
        {
            _rentalService = new RentalService();

            _availableCars = new ObservableCollection<CarModel>();
            _activeRentals = new ObservableCollection<RentalModel>();
            _clientName = string.Empty;
            _statusMessage = string.Empty;

            InitializeCars();

            RentalDays = 1;
            StatusMessage = "Готов к работе";

            // Инициализация команд
            RentCarCommand = new RelayCommand(async _ => await RentCarAsync(), CanRentCar);
            ReturnCarCommand = new RelayCommand(async _ => await ReturnCarAsync(), CanReturnCar);
        }

        #region Properties

        public ObservableCollection<CarModel> AvailableCars
        {
            get => _availableCars;
            set
            {
                _availableCars = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<RentalModel> ActiveRentals
        {
            get => _activeRentals;
            set
            {
                _activeRentals = value;
                OnPropertyChanged();
            }
        }

        public CarModel? SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalRentalCost));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public RentalModel? SelectedRental
        {
            get => _selectedRental;
            set
            {
                _selectedRental = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string ClientName
        {
            get => _clientName;
            set
            {
                _clientName = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public int RentalDays
        {
            get => _rentalDays;
            set
            {
                _rentalDays = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalRentalCost));
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsProcessing
        {
            get => _isProcessing;
            set
            {
                _isProcessing = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public decimal TotalRentalCost
        {
            get
            {
                if (SelectedCar != null && RentalDays > 0)
                {
                    return SelectedCar.PricePerDay * RentalDays;
                }
                return 0;
            }
        }

        #endregion

        #region Commands

        public ICommand RentCarCommand { get; }
        public ICommand ReturnCarCommand { get; }

        private bool CanRentCar(object? parameter)
        {
            return !IsProcessing &&
                   SelectedCar != null &&
                   SelectedCar.IsAvailable &&
                   !string.IsNullOrWhiteSpace(ClientName) &&
                   RentalDays > 0;
        }

        private async System.Threading.Tasks.Task RentCarAsync()
        {
            if (SelectedCar == null) return;

            try
            {
                IsProcessing = true;
                StatusMessage = "⏳ Обрабатывается заявка на аренду...";

                var rental = await _rentalService.ProcessRentalAsync(SelectedCar, ClientName, RentalDays);

                ActiveRentals.Add(rental);
                StatusMessage = $"✅ Автомобиль {SelectedCar.FullName} арендован на {RentalDays} дн. Стоимость: {rental.TotalCost:C}";

                // Очистка формы
                ClientName = string.Empty;
                RentalDays = 1;
                SelectedCar = null;
            }
            catch (Exception ex)
            {
                StatusMessage = $"❌ Ошибка: {ex.Message}";
                MessageBox.Show($"Ошибка при аренде: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        private bool CanReturnCar(object? parameter)
        {
            return !IsProcessing &&
                   SelectedRental != null &&
                   SelectedRental.Status == RentalStatus.Active;
        }

        private async System.Threading.Tasks.Task ReturnCarAsync()
        {
            if (SelectedRental == null) return;

            try
            {
                IsProcessing = true;
                StatusMessage = "⏳ Обрабатывается возврат автомобиля...";

                await _rentalService.ProcessReturnAsync(SelectedRental);

                StatusMessage = $"✅ Автомобиль {SelectedRental.Car.FullName} возвращен";
                ActiveRentals.Remove(SelectedRental);
                SelectedRental = null;
            }
            catch (Exception ex)
            {
                StatusMessage = $"❌ Ошибка: {ex.Message}";
                MessageBox.Show($"Ошибка при возврате: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsProcessing = false;
            }
        }

        #endregion

        private void InitializeCars()
        {
            AvailableCars.Add(new CarModel { Id = 1, Brand = "Toyota", Model = "Camry", PricePerDay = 50, IsAvailable = true });
            AvailableCars.Add(new CarModel { Id = 2, Brand = "BMW", Model = "X5", PricePerDay = 120, IsAvailable = true });
            AvailableCars.Add(new CarModel { Id = 3, Brand = "Mercedes", Model = "E-Class", PricePerDay = 100, IsAvailable = true });
            AvailableCars.Add(new CarModel { Id = 4, Brand = "Audi", Model = "A6", PricePerDay = 90, IsAvailable = true });
            AvailableCars.Add(new CarModel { Id = 5, Brand = "Volkswagen", Model = "Polo", PricePerDay = 35, IsAvailable = true });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}