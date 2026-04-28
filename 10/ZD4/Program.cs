using System;
using System.IO;

public class FileWatcher
{
    private FileSystemWatcher watcher;

    public FileWatcher(string path)
    {
        watcher = new FileSystemWatcher(path);
        watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;

        watcher.Created += OnChanged;
        watcher.Changed += OnChanged;
        watcher.Deleted += OnDeleted;
        watcher.Renamed += OnRenamed;

        watcher.EnableRaisingEvents = true;
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        if (File.Exists(e.FullPath))
        {
            long size = new FileInfo(e.FullPath).Length;

            if (size > 100 * 1024 * 1024)
            {
                Console.WriteLine($"Файл {e.Name} слишком большой!");
            }
        }
    }

    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"Файл удалён: {e.Name}");
    }

    private void OnRenamed(object sender, RenamedEventArgs e)
    {
        Console.WriteLine($"Файл переименован: {e.OldName} → {e.Name}");
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Отслеживание начато...");
        FileWatcher watcher = new FileWatcher(Directory.GetCurrentDirectory());

        Console.ReadLine();
    }
}