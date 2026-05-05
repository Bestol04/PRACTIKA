using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarRentalApp.Models
{
    public class Booking : INotifyPropertyChanged
    {
        private Car _selectedCar;
        private DateTime _startDate;
        private DateTime _endDate;
        private decimal _totalCost;

        public Car SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                OnPropertyChanged();
                CalculateTotalCost();
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
                CalculateTotalCost();
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
                CalculateTotalCost();
            }
        }

        public decimal TotalCost
        {
            get => _totalCost;
            private set
            {
                _totalCost = value;
                OnPropertyChanged();
            }
        }

        public int RentalDays => (EndDate - StartDate).Days;

        private void CalculateTotalCost()
        {
            if (SelectedCar != null && EndDate > StartDate)
            {
                TotalCost = SelectedCar.PricePerDay * RentalDays;
            }
            else
            {
                TotalCost = 0;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}