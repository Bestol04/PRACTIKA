using System.Windows;
using CarRentalApp.ViewModels;

namespace CarRentalApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }
    }
}