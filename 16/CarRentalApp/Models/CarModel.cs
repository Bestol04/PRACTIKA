using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarRentalApp.Models
{
    public class CarModel : INotifyPropertyChanged
    {
        private string _brand = string.Empty;
        private string _model = string.Empty;
        private decimal _pricePerDay;
        private bool _isAvailable;
        private string _imageUrl = string.Empty;

        public int Id { get; set; }

        public string Brand
        {
            get => _brand;
            set
            {
                _brand = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
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

        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                OnPropertyChanged();
            }
        }

        public string FullName => $"{Brand} {Model}";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}