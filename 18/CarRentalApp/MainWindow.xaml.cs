using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CarRentalApp
{
    public partial class MainWindow : Window
    {
        private List<Car> cars;
        private DispatcherTimer animationTimer;

        public MainWindow()
        {
            InitializeComponent();
            InitializeCars();
            LoadCars();
        }

        private void InitializeCars()
        {
            cars = new List<Car>
            {
                new Car { Model = "BMW X5", Year = 2023, PricePerDay = 55, IsAvailable = true, Color = "#1E88E5" },
                new Car { Model = "Mercedes-Benz E-Class", Year = 2023, PricePerDay = 60, IsAvailable = true, Color = "#43A047" },
                new Car { Model = "Audi A6", Year = 2022, PricePerDay = 40, IsAvailable = false, Color = "#E53935" },
                new Car { Model = "Toyota Camry", Year = 2023, PricePerDay = 32, IsAvailable = true, Color = "#FB8C00" },
                new Car { Model = "Lexus RX", Year = 2023, PricePerDay = 58, IsAvailable = true, Color = "#8E24AA" },
                new Car { Model = "Volkswagen Passat", Year = 2022, PricePerDay = 35, IsAvailable = false, Color = "#5E35B1" }
            };
        }

        private void LoadCars()
        {
            int delay = 0;

            foreach (var car in cars)
            {
                var card = CreateCarCard(car);
                CarsPanel.Children.Add(card);

                // Задержка анимации для каждой карточки
                var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(delay) };
                timer.Tick += (s, e) =>
                {
                    AnimateCardAppearance(card);
                    ((DispatcherTimer)s).Stop();
                };
                timer.Start();

                delay += 150; // Задержка между появлением карточек
            }
        }

        private Border CreateCarCard(Car car)
        {
            // Основной контейнер карточки
            var cardBorder = new Border
            {
                Style = (Style)FindResource("CarCardStyle"),
                Width = 250,
                Height = 320,
                RenderTransform = new ScaleTransform(1, 1),
                RenderTransformOrigin = new Point(0.5, 0.5),
                Opacity = 0
            };

            // Цветная рамка для свободных авто
            if (car.IsAvailable)
            {
                cardBorder.BorderThickness = new Thickness(3, 3, 3, 3);
                cardBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50"));

                // Запуск анимации подсветки
                var glowStoryboard = ((Storyboard)FindResource("GlowAnimation")).Clone();
                Storyboard.SetTarget(glowStoryboard, cardBorder);
                glowStoryboard.Begin();
            }
            else
            {
                cardBorder.BorderThickness = new Thickness(2, 2, 2, 2);
                cardBorder.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }

            var stackPanel = new StackPanel();

            // Иконка автомобиля
            var carIcon = CreateCarIcon(car.Color);
            stackPanel.Children.Add(carIcon);

            // Модель автомобиля
            var modelText = new TextBlock
            {
                Text = car.Model,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 15, 0, 5),
                TextWrapping = TextWrapping.Wrap
            };
            stackPanel.Children.Add(modelText);

            // Год
            var yearText = new TextBlock
            {
                Text = $"Год: {car.Year}",
                FontSize = 12,
                Foreground = Brushes.Gray,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10)
            };
            stackPanel.Children.Add(yearText);

            // Цена
            var priceText = new TextBlock
            {
                Text = $"{car.PricePerDay} $/день",
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2196F3")),
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 15)
            };
            stackPanel.Children.Add(priceText);

            // Статус
            var statusBorder = new Border
            {
                Background = new SolidColorBrush(car.IsAvailable ?
                    (Color)ColorConverter.ConvertFromString("#E8F5E9") :
                    (Color)ColorConverter.ConvertFromString("#FFEBEE")),
                CornerRadius = new CornerRadius(3),
                Padding = new Thickness(10, 5, 10, 5),
                Margin = new Thickness(0, 0, 0, 10)
            };

            var statusText = new TextBlock
            {
                Text = car.IsAvailable ? "✓ Доступен" : "✗ Забронирован",
                FontSize = 12,
                FontWeight = FontWeights.SemiBold,
                Foreground = new SolidColorBrush(car.IsAvailable ?
                    (Color)ColorConverter.ConvertFromString("#4CAF50") :
                    (Color)ColorConverter.ConvertFromString("#F44336")),
                TextAlignment = TextAlignment.Center
            };
            statusBorder.Child = statusText;
            stackPanel.Children.Add(statusBorder);

            // Кнопка бронирования
            if (car.IsAvailable)
            {
                var bookButton = new Button
                {
                    Content = "Забронировать",
                    Style = (Style)FindResource("BookButtonStyle"),
                    RenderTransform = new ScaleTransform(1, 1),
                    RenderTransformOrigin = new Point(0.5, 0.5)
                };

                bookButton.Click += (s, e) => BookCar_Click(s, e, car);
                bookButton.MouseEnter += BookButton_MouseEnter;
                bookButton.MouseLeave += BookButton_MouseLeave;

                stackPanel.Children.Add(bookButton);
            }

            cardBorder.Child = stackPanel;
            return cardBorder;
        }

        private Canvas CreateCarIcon(string colorHex)
        {
            var canvas = new Canvas
            {
                Width = 120,
                Height = 80,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 10, 0, 0)
            };

            var color = (Color)ColorConverter.ConvertFromString(colorHex);

            // Кузов автомобиля
            var body = new Rectangle
            {
                Width = 100,
                Height = 35,
                Fill = new SolidColorBrush(color),
                RadiusX = 5,
                RadiusY = 5
            };
            Canvas.SetLeft(body, 10);
            Canvas.SetTop(body, 30);
            canvas.Children.Add(body);

            // Крыша
            var roof = new Rectangle
            {
                Width = 50,
                Height = 25,
                Fill = new SolidColorBrush(color),
                RadiusX = 5,
                RadiusY = 5
            };
            Canvas.SetLeft(roof, 35);
            Canvas.SetTop(roof, 10);
            canvas.Children.Add(roof);

            // Переднее колесо
            var frontWheel = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Black,
                Stroke = Brushes.Gray,
                StrokeThickness = 2
            };
            Canvas.SetLeft(frontWheel, 75);
            Canvas.SetTop(frontWheel, 55);
            canvas.Children.Add(frontWheel);

            // Заднее колесо
            var rearWheel = new Ellipse
            {
                Width = 20,
                Height = 20,
                Fill = Brushes.Black,
                Stroke = Brushes.Gray,
                StrokeThickness = 2
            };
            Canvas.SetLeft(rearWheel, 25);
            Canvas.SetTop(rearWheel, 55);
            canvas.Children.Add(rearWheel);

            // Окна
            var window1 = new Rectangle
            {
                Width = 20,
                Height = 15,
                Fill = new SolidColorBrush(Color.FromArgb(100, 135, 206, 250)),
                RadiusX = 2,
                RadiusY = 2
            };
            Canvas.SetLeft(window1, 40);
            Canvas.SetTop(window1, 15);
            canvas.Children.Add(window1);

            var window2 = new Rectangle
            {
                Width = 20,
                Height = 15,
                Fill = new SolidColorBrush(Color.FromArgb(100, 135, 206, 250)),
                RadiusX = 2,
                RadiusY = 2
            };
            Canvas.SetLeft(window2, 65);
            Canvas.SetTop(window2, 15);
            canvas.Children.Add(window2);

            return canvas;
        }

        // Анимация появления карточки
        private void AnimateCardAppearance(Border card)
        {
            var storyboard = ((Storyboard)FindResource("CardAppearAnimation")).Clone();
            Storyboard.SetTarget(storyboard, card);
            storyboard.Begin();
        }

        // Обработка наведения на кнопку
        private void BookButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var button = sender as Button;
            var pulseStoryboard = ((Storyboard)FindResource("PulseAnimation")).Clone();
            Storyboard.SetTarget(pulseStoryboard, button);
            button.Tag = pulseStoryboard;
            pulseStoryboard.Begin();
        }

        private void BookButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var button = sender as Button;
            if (button.Tag is Storyboard storyboard)
            {
                storyboard.Stop();
                button.RenderTransform = new ScaleTransform(1, 1);
            }
        }

        // Обработка клика по кнопке бронирования
        private void BookCar_Click(object sender, RoutedEventArgs e, Car car)
        {
            var button = sender as Button;

            // Останавливаем анимацию пульсации
            if (button.Tag is Storyboard pulseStoryboard)
            {
                pulseStoryboard.Stop();
            }

            // Анимация вспышки
            var flashStoryboard = ((Storyboard)FindResource("FlashAnimation")).Clone();
            Storyboard.SetTarget(flashStoryboard, button);
            flashStoryboard.Begin();

            // Показ сообщения
            MessageBox.Show($"Автомобиль {car.Model} успешно забронирован!\n" +
                          $"Стоимость: {car.PricePerDay} ₽/день",
                          "Бронирование",
                          MessageBoxButton.OK,
                          MessageBoxImage.Information);

            // Обновление статуса
            car.IsAvailable = false;
            button.IsEnabled = false;
            button.Content = "Забронировано";
            button.Background = Brushes.Gray;
        }
    }

    // Модель данных автомобиля
    public class Car
    {
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string Color { get; set; }
    }
}