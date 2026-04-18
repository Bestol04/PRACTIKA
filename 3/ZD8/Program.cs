using System;

public class Task8
{
    public static void Main()
    {
        string str1 = "Hello World";
        string str2 = "hello   world";

        string normalized1 = str1.Replace(" ", "").ToLower();
        string normalized2 = str2.Replace(" ", "").ToLower();

        Console.WriteLine(normalized1 == normalized2);
    }
}