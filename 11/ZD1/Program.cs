using System;

public sealed class UserIDGenerator
{
    private static readonly UserIDGenerator instance = new UserIDGenerator();

    private int currentId;

    private UserIDGenerator()
    {
        currentId = 0;
    }

    public static UserIDGenerator Instance
    {
        get { return instance; }
    }

    public int GenerateID()
    {
        currentId++;
        return currentId;
    }
}

public class Program
{
    public static void Main()
    {
        UserIDGenerator generator = UserIDGenerator.Instance;

        Console.WriteLine(generator.GenerateID());
        Console.WriteLine(generator.GenerateID());
        Console.WriteLine(generator.GenerateID());
    }
}