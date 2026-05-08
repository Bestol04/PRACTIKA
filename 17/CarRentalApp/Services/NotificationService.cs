using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentalApp.Services
{
    public class NotificationService
    {
        private const string MappedFileName = "CarRentalNotifications";
        private const int BufferSize = 1024;
        private MemoryMappedFile? _mmf;
        private CancellationTokenSource? _cancellationTokenSource;

        public event EventHandler<string>? NotificationReceived;

        public void StartListening()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            Task.Run(async () =>
            {
                try
                {
                    _mmf = MemoryMappedFile.CreateOrOpen(MappedFileName, BufferSize);

                    while (!_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        using var accessor = _mmf.CreateViewAccessor();
                        byte[] buffer = new byte[BufferSize];
                        accessor.ReadArray(0, buffer, 0, buffer.Length);

                        string message = Encoding.UTF8.GetString(buffer).TrimEnd('\0');

                        if (!string.IsNullOrWhiteSpace(message))
                        {
                            NotificationReceived?.Invoke(this, message);

                            // Очистка после чтения
                            accessor.WriteArray(0, new byte[BufferSize], 0, BufferSize);
                        }

                        await Task.Delay(500);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка службы уведомлений: {ex.Message}");
                }
            }, _cancellationTokenSource.Token);
        }

        public void SendNotification(string message)
        {
            try
            {
                using var mmf = MemoryMappedFile.CreateOrOpen(MappedFileName, BufferSize);
                using var accessor = mmf.CreateViewAccessor();

                byte[] buffer = Encoding.UTF8.GetBytes(message);
                if (buffer.Length > BufferSize)
                {
                    buffer = buffer[..BufferSize];
                }

                accessor.WriteArray(0, buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки уведомления: {ex.Message}");
            }
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
            _mmf?.Dispose();
        }
    }
}