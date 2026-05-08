using System.Windows;
using CarRentalApp.Services;
using CarRentalApp.ViewModels;

namespace CarRentalApp.Views
{
    public partial class ChatWindow : Window
    {
        public ChatWindow(AuthService authService, ChatService chatService)
        {
            InitializeComponent();
            DataContext = new ChatViewModel(authService, chatService);
        }
    }
}