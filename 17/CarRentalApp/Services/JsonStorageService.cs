using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarRentalApp.Services
{
    public class JsonStorageService
    {
        private readonly string _dataFolder;

        public JsonStorageService()
        {
            _dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(_dataFolder))
            {
                Directory.CreateDirectory(_dataFolder);
            }
        }

        public async Task<T?> LoadDataAsync<T>(string fileName) where T : class
        {
            try
            {
                string filePath = Path.Combine(_dataFolder, fileName);
                if (!File.Exists(filePath))
                {
                    return null;
                }

                string json = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки данных: {ex.Message}");
                return null;
            }
        }

        public async Task SaveDataAsync<T>(string fileName, T data) where T : class
        {
            try
            {
                string filePath = Path.Combine(_dataFolder, fileName);
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                string json = JsonSerializer.Serialize(data, options);
                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения данных: {ex.Message}");
            }
        }

        public string GetFilePath(string fileName)
        {
            return Path.Combine(_dataFolder, fileName);
        }
    }
}