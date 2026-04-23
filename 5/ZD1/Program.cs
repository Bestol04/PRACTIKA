using System;

public class Program
{
    public static void Main()
    {
        Console.Write("Введите год: ");
        int year = int.Parse(Console.ReadLine());

        bool isLeapYear = IsLeapYear(year);

        Console.WriteLine(isLeapYear
            ? "Год является високосным."
            : "Год не является високосным.");
    }
    public static bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) ||
               (year % 400 == 0);
    }
}