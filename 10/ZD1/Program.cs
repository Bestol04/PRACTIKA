using System;
using System.IO;
using System.Linq;

public class FileManager
{
    public void CreateAndWrite(string path, string content)
    {
        File.WriteAllText(path, content);
    }

    public void ReadFile(string path)
    {
        Console.WriteLine(File.ReadAllText(path));
    }

    public void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Console.WriteLine("Файл удалён.");
        }
        else
        {
            Console.WriteLine("Файл не существует.");
        }
    }

    public void CopyFile(string source, string destination)
    {
        File.Copy(source, destination, true);
    }

    public void MoveFile(string source, string destination)
    {
        File.Move(source, destination, true);
    }

    public void RenameFile(string source, string newName)
    {
        string directory = Path.GetDirectoryName(source);
        string newPath = Path.Combine(directory, newName);
        File.Move(source, newPath, true);
    }

    public void DeleteByExtension(string folder, string extension)
    {
        var files = Directory.GetFiles(folder, $"*.{extension}");
        foreach (var file in files)
            File.Delete(file);
    }

    public void ListFiles(string folder)
    {
        var files = Directory.GetFiles(folder);
        foreach (var file in files)
            Console.WriteLine(file);
    }

    public void CompareFiles(string file1, string file2)
    {
        long size1 = new FileInfo(file1).Length;
        long size2 = new FileInfo(file2).Length;

        Console.WriteLine(size1 == size2
            ? "Файлы одинакового размера."
            : "Файлы разного размера.");
    }

    public void MakeReadOnly(string path)
    {
        File.SetAttributes(path, FileAttributes.ReadOnly);
    }

    public void CheckAccess(string path)
    {
        FileAttributes attributes = File.GetAttributes(path);
        Console.WriteLine(attributes.HasFlag(FileAttributes.ReadOnly)
            ? "Только чтение"
            : "Можно записывать");
    }
}

public class FileInfoProvider
{
    public void PrintInfo(string path)
    {
        FileInfo info = new FileInfo(path);

        Console.WriteLine($"Размер: {info.Length} байт");
        Console.WriteLine($"Создан: {info.CreationTime}");
        Console.WriteLine($"Изменён: {info.LastWriteTime}");
    }
}

public class Program
{
    public static void Main()
    {
        string filePath = "korobov.nd";
        string copyPath = "copy_korobov.nd";
        string folder = Directory.GetCurrentDirectory();

        FileManager manager = new FileManager();
        FileInfoProvider infoProvider = new FileInfoProvider();

        manager.CreateAndWrite(filePath, "Тестовый текст.");
        manager.ReadFile(filePath);

        infoProvider.PrintInfo(filePath);

        manager.CopyFile(filePath, copyPath);
        manager.CompareFiles(filePath, copyPath);

        manager.RenameFile(copyPath, "korobov.nd1");

        manager.MakeReadOnly(filePath);
        manager.CheckAccess(filePath);

        manager.ListFiles(folder);

        manager.DeleteByExtension(folder, "nd");
    }
}