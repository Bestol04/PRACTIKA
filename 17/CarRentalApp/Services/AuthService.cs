using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CarRentalApp.Models;

namespace CarRentalApp.Services
{
    public class AuthService
    {
        private readonly JsonStorageService _storageService;
        private List<UserModel> _users;
        private UserModel? _currentUser;
        private const string UsersFileName = "users.json";

        public UserModel? CurrentUser => _currentUser;

        public AuthService(JsonStorageService storageService)
        {
            _storageService = storageService;
            _users = new List<UserModel>();
        }

        public async Task InitializeAsync()
        {
            var users = await _storageService.LoadDataAsync<List<UserModel>>(UsersFileName);
            _users = users ?? new List<UserModel>();

            // Создание администратора по умолчанию
            if (!_users.Any(u => u.Role == UserRole.Admin))
            {
                _users.Add(new UserModel
                {
                    Username = "admin",
                    Password = HashPassword("admin123"),
                    FullName = "Администратор",
                    Role = UserRole.Admin,
                    RegistrationDate = DateTime.Now
                });
                await SaveUsersAsync();
            }
        }

        public async Task ResetOnlineFlagsAsync()
        {
            var changed = false;
            foreach (var u in _users)
            {
                if (u.IsOnline) { u.IsOnline = false; changed = true; }
            }
            if (changed) await SaveUsersAsync();
        }

        public List<UserModel> GetAllUsers() => _users.ToList();

        public async Task<(bool Success, string Message)> RegisterAsync(string username, string password, string fullName, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "Имя пользователя и пароль не могут быть пустыми");
            }

            if (_users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                return (false, "Пользователь с таким именем уже существует");
            }

            var newUser = new UserModel
            {
                Username = username,
                Password = HashPassword(password),
                FullName = fullName,
                Role = role,
                RegistrationDate = DateTime.Now
            };

            _users.Add(newUser);
            await SaveUsersAsync();

            return (true, "Регистрация успешна!");
        }

        public async Task<(bool Success, string Message, UserModel? User)> LoginAsync(string username, string password)
        {
            var user = _users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == HashPassword(password));

            if (user == null)
            {
                return (false, "Неверное имя пользователя или пароль", null);
            }

            _currentUser = user;
            _currentUser.IsOnline = true;
            await SaveUsersAsync();

            return (true, "Вход выполнен успешно!", user);
        }

        public async Task LogoutAsync()
        {
            if (_currentUser != null)
            {
                _currentUser.IsOnline = false;
                await SaveUsersAsync();
                _currentUser = null;
            }
        }

        public List<UserModel> GetOnlineUsers()
        {
            return _users.Where(u => u.IsOnline).ToList();
        }

        public List<UserModel> GetUsersByRole(UserRole role)
        {
            return _users.Where(u => u.Role == role).ToList();
        }

        private async Task SaveUsersAsync()
        {
            await _storageService.SaveDataAsync(UsersFileName, _users);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}