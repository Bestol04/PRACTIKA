using System;
using System.Collections.Generic;

public interface INotifier<T>
{
    void Notify(T message);
}

public class EmailNotifier<T> : INotifier<T>
{
    public void Notify(T message)
    {
        Console.WriteLine($"Email отправлен: {message}");
    }
}

public class NotifierManager<T>
{
    private INotifier<T> notifier;

    public NotifierManager(INotifier<T> notifier)
    {
        this.notifier = notifier;
    }

    public void SendBulk(IEnumerable<T> messages)
    {
        foreach (T message in messages)
        {
            notifier.Notify(message);
        }
    }
}


public class Program
{
    public static void Main()
    {
        INotifier<string> emailNotifier = new EmailNotifier<string>();
        NotifierManager<string> manager = new NotifierManager<string>(emailNotifier);

        List<string> messages = new List<string>
        {
            "Сообщение 1",
            "Сообщение 2",
            "Сообщение 3"
        };

        manager.SendBulk(messages);
    }
}