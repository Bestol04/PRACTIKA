using System;

public class Task7
{
    public static void Main()
    {
        string input = "Я люблю программирование";

        string[] words = input.Split(' ');
        Array.Reverse(words);

        Console.WriteLine(string.Join(" ", words));
    }
}