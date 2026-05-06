using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarRentalApp.Models
{
    public class RentalModel : INotifyPropertyChanged
    {
        private int _id;
        private CarModel? _car;
        private string _clientName = string.Empty;
        private DateTime _rentalDate;
        private DateTime? _returnDate;
        private int _daysRented;
        private decimal _totalCost;
        private RentalStatus _status;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public CarModel? Car
        {
            get => _car;
            set
            {
                _car = value;
                OnPropertyChanged();
            }
        }

        public string ClientName
        {
            get => _clientName;
            set
            {
                _clientName = value;
                OnPropertyChanged();
            }
        }

        public DateTime RentalDate
        {
            get => _rentalDate;
            set
            {
                _rentalDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime? ReturnDate
        {
            get => _returnDate;
            set
            {
                _returnDate = value;
                OnPropertyChanged();
            }
        }

        public int DaysRented
        {
            get => _daysRented;
            set
            {
                _daysRented = value;
                OnPropertyChanged();
                CalculateTotalCost();
            }
        }

        public decimal TotalCost
        {
            get => _totalCost;
            set
            {
                _totalCost = value;
                OnPropertyChanged();
            }
        }

        public RentalStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        private void CalculateTotalCost()
        {
            if (Car != null && DaysRented > 0)
            {
                TotalCost = Car.PricePerDay * DaysRented;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum RentalStatus
    {
        Processing,
        Active,
        Completed,
        Cancelled
    }
}