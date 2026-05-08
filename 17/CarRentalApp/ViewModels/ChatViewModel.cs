using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CarRentalApp.Commands;
using CarRentalApp.Models;
using CarRentalApp.Services;

namespace CarRentalApp.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;
        private readonly ChatService _chatService;
        private ObservableCollection<ChatMessage> _messages;
        private ObservableCollection<UserModel> _onlineUsers;
        private UserModel? _selectedUser;
        private string _messageText = string.Empty;
        private string _statusMessage = string.Empty;

        public ObservableCollection<ChatMessage> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<UserModel> OnlineUsers
        {
            get => _onlineUsers;
            set
            {
                _onlineUsers = value;
                OnPropertyChanged();
            }
        }

        public UserModel? SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
                LoadMessages();
            }
        }

        public string MessageText
        {
            get => _messageText;
            set
            {
                _messageText = value;
                OnPropertyChanged();
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public string CurrentUserName => _authService.CurrentUser?.FullName ?? "Неизвестный";

        public ICommand SendMessageCommand { get; }

        public ChatViewModel(AuthService authService, ChatService chatService)
        {
            _authService = authService;
            _chatService = chatService;
            _messages = new ObservableCollection<ChatMessage>();
            _onlineUsers = new ObservableCollection<UserModel>();

            SendMessageCommand = new RelayCommand(async _ => await SendMessageAsync(), CanSendMessage);

            LoadOnlineUsers();
            LoadExistingMessages();

            // Подписка на новые сообщения
            _chatService.MessageReceived += OnMessageReceived;
        }

        private bool CanSendMessage(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(MessageText) && SelectedUser != null;
        }

        private async System.Threading.Tasks.Task SendMessageAsync()
        {
            if (SelectedUser == null || _authService.CurrentUser == null) return;

            var message = new ChatMessage
            {
                Sender = _authService.CurrentUser.Username,
                Receiver = SelectedUser.Username,
                Message = MessageText,
                Timestamp = DateTime.Now,
                SenderRole = _authService.CurrentUser.Role
            };

            await _chatService.SendMessageAsync(message);

            // Добавляем сообщение в локальный список
            Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(message);
            });

            MessageText = string.Empty;
            StatusMessage = "Сообщение отправлено";
        }

        private void OnMessageReceived(object? sender, ChatMessage message)
        {
            if (_authService.CurrentUser == null) return;

            // Показываем сообщения только для текущего диалога
            if ((message.Sender == SelectedUser?.Username && message.Receiver == _authService.CurrentUser.Username) ||
                (message.Receiver == SelectedUser?.Username && message.Sender == _authService.CurrentUser.Username))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Add(message);
                });
            }
        }

        private void LoadOnlineUsers()
        {
            var users = _authService.GetOnlineUsers()
                .Where(u => u.Username != _authService.CurrentUser?.Username)
                .ToList();

            OnlineUsers.Clear();
            foreach (var user in users)
            {
                OnlineUsers.Add(user);
            }
        }

        private void LoadExistingMessages()
        {
            var allMessages = _chatService.GetMessages();

            foreach (var msg in allMessages.OrderBy(m => m.Timestamp))
            {
                Messages.Add(msg);
            }
        }

        private void LoadMessages()
        {
            if (SelectedUser == null || _authService.CurrentUser == null) return;

            Messages.Clear();

            var allMessages = _chatService.GetMessages();
            var filteredMessages = allMessages
                .Where(m => (m.Sender == _authService.CurrentUser.Username && m.Receiver == SelectedUser.Username) ||
                           (m.Sender == SelectedUser.Username && m.Receiver == _authService.CurrentUser.Username))
                .OrderBy(m => m.Timestamp)
                .ToList();

            foreach (var msg in filteredMessages)
            {
                Messages.Add(msg);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}