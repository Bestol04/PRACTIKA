using System;

public class ServerShutdownManager
{
    public event EventHandler ServerShuttingDown;

    public void Shutdown()
    {
        Console.WriteLine("Сервер завершает работу...");
        ServerShuttingDown?.Invoke(this, EventArgs.Empty);
    }
}

public class BackupService
{
    public void OnServerShutdown(object sender, EventArgs e)
    {
        Console.WriteLine("Создание резервной копии...");
    }
}

public class AlertSystem
{
    public void OnServerShutdown(object sender, EventArgs e)
    {
        Console.WriteLine("Оповещение администратора...");
    }
}

public class ShutdownMonitor
{
    public ShutdownMonitor(ServerShutdownManager manager,
                           BackupService backup,
                           AlertSystem alert)
    {
        manager.ServerShuttingDown += backup.OnServerShutdown;
        manager.ServerShuttingDown += alert.OnServerShutdown;
    }
}

public class Program
{
    public static void Main()
    {
        ServerShutdownManager manager = new ServerShutdownManager();
        BackupService backup = new BackupService();
        AlertSystem alert = new AlertSystem();

        ShutdownMonitor monitor = new ShutdownMonitor(manager, backup, alert);

        manager.Shutdown();
    }
}