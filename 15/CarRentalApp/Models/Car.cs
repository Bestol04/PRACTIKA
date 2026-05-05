using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarRentalApp.Models
{
    public class Car : INotifyPropertyChanged
    {
        private string _brand;
        private string _model;
        private string _carClass;
        private decimal _pricePerDay;
        private bool _isAvailable;
        private int _seats;

        public int Id { get; set; }

        public string Brand
        {
            get => _brand;
            set
            {
                _brand = value;
                OnPropertyChanged();
            }
        }

        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public string CarClass
        {
            get => _carClass;
            set
            {
                _carClass = value;
                OnPropertyChanged();
            }
        }

        public decimal PricePerDay
        {
            get => _pricePerDay;
            set
            {
                _pricePerDay = value;
                OnPropertyChanged();
            }
        }

        public bool IsAvailable
        {
            get => _isAvailable;
            set
            {
                _isAvailable = value;
                OnPropertyChanged();
            }
        }

        public int Seats
        {
            get => _seats;
            set
            {
                _seats = value;
                OnPropertyChanged();
            }
        }

        public string FullName => $"{Brand} {Model}";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}