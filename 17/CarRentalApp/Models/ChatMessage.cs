using System;

namespace CarRentalApp.Models
{
    public class ChatMessage
    {
        public string Sender { get; set; } = string.Empty;
        public string Receiver { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public UserRole SenderRole { get; set; }

        public string FormattedTime => Timestamp.ToString("HH:mm:ss");
        public string DisplayName => $"{Sender} ({SenderRole})";
    }
}