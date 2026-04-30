using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace CarRentalApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Car> cars = new();

        public MainWindow()
        {
            InitializeComponent();
            LoadCars();
        }

        private void LoadCars()
        {
            cars.Add(new Car { Name = "Toyota Camry", PricePerDay = 3000 });
            cars.Add(new Car { Name = "BMW X5", PricePerDay = 7000 });
            cars.Add(new Car { Name = "Kia Rio", PricePerDay = 2500 });

            CarListBox.ItemsSource = cars;
        }

        private void CarListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePrice();
        }

        private void DaysSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (DaysText != null)
            {
                int days = (int)DaysSlider.Value;
                string word = days == 1 ? "день" : days < 5 ? "дня" : "дней";
                DaysText.Text = $"{days} {word}";
                UpdatePrice();
            }
        }

        private void UpdatePrice()
        {
            if (CarListBox.SelectedItem is Car selectedCar && DaysSlider != null && TotalPriceText != null)
            {
                int days = (int)DaysSlider.Value;
                decimal total = selectedCar.PricePerDay * days;
                TotalPriceText.Text = $"{total} руб.";
            }
        }

        private void RentButton_Click(object sender, RoutedEventArgs e)
        {
            if (CarListBox.SelectedItem is Car selectedCar)
            {
                if (selectedCar.IsRented)
                {
                    MessageBox.Show("Автомобиль уже арендован.");
                    return;
                }

                selectedCar.IsRented = true;
                MessageBox.Show("Автомобиль успешно арендован!");
            }
            else
            {
                MessageBox.Show("Выберите автомобиль.");
            }
        }
    }
}