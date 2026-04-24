using System;

public abstract class User
{
    public string Name { get; }

    protected User(string name)
    {
        Name = name;
    }

    public abstract string GetPermissions();
}

public class Admin : User
{
    public Admin(string name) : base(name) { }

    public override string GetPermissions()
    {
        return "Полный доступ к системе.";
    }
}

public class Moderator : User
{
    public Moderator(string name) : base(name) { }

    public override string GetPermissions()
    {
        return "Может редактировать и удалять контент.";
    }
}

public class Guest : User
{
    public Guest(string name) : base(name) { }

    public override string GetPermissions()
    {
        return "Только просмотр контента.";
    }
}

public class Program
{
    public static void Main()
    {
        User[] users =
        {
            new Admin("Алексей"),
            new Moderator("Мария"),
            new Guest("Иван")
        };

        foreach (User user in users)
        {
            Console.WriteLine($"{user.Name}: {user.GetPermissions()}");
        }
    }
}