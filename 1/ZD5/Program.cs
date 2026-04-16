using System;

public class DigitSwap
{
    public static void Main()
    {
        Console.Write("Введите четырехзначное число: ");
        int number = Convert.ToInt32(Console.ReadLine());

        if (number < 1000 || number > 9999)
        {
            Console.WriteLine("Ошибка: число должно быть четырехзначным!");
            return;
        }

        int firstPart = number / 100;
        int secondPart = number % 100;

        int result = secondPart * 100 + firstPart;

        Console.WriteLine($"Результат: {result}");
    }
}