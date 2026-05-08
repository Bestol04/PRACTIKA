using System.Windows;
using System.Windows.Threading;
using CarRentalApp.Services;
using CarRentalApp.Views;

namespace CarRentalApp
{
    public partial class App : Application
    {
        public static JsonStorageService Storage { get; private set; } = null!;
        public static AuthService Auth { get; private set; } = null!;
        public static NotificationService Notifications { get; private set; } = null!;
        public static RentalService Rentals { get; private set; } = null!;
        public static ChatService Chat { get; private set; } = null!;

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                Storage = new JsonStorageService();
                Auth = new AuthService(Storage);
                await Auth.InitializeAsync();
                await Auth.ResetOnlineFlagsAsync();

                Notifications = new NotificationService();
                Notifications.StartListening();

                Rentals = new RentalService(Storage, Notifications);
                await Rentals.InitializeAsync();

                Chat = new ChatService();
                _ = Chat.StartServerAsync();

                var loginWindow = new LoginWindow();
                loginWindow.Show();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка запуска: {ex.Message}\n\n{ex.StackTrace}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DispatcherUnhandledException += (s, args) =>
            {
                MessageBox.Show($"Произошла ошибка: {args.Exception.Message}\n\n{args.Exception.StackTrace}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                args.Handled = true;
            };
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                Chat?.Stop();
                Notifications?.Stop();
                if (Auth != null)
                    Auth.LogoutAsync().GetAwaiter().GetResult();
            }
            catch { }
            base.OnExit(e);
        }
    }
}
