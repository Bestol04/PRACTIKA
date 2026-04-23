using System;

public static class IntExtensions
{
    public static long Factorial(this int number)
    {
        if (number < 0)
            throw new ArgumentException("Факториал отрицательного числа не определён.");

        long result = 1;

        for (int i = 2; i <= number; i++)
        {
            result *= i;
        }

        return result;
    }
}

public class Program
{
    public static void Main()
    {
        int number = 5;
        long result = number.Factorial();

        Console.WriteLine($"Факториал {number} = {result}");
    }
}