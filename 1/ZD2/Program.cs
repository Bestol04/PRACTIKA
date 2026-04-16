using System;

public class DigitMultiplication
{
    public static void Main()
    {
        Console.Write("Введите трехзначное число: ");
        int number = Convert.ToInt32(Console.ReadLine());

        int tens = (number / 10) % 10;
        int units = number % 10;

        int result = tens * units;

        Console.WriteLine($"Произведение второй и последней цифры: {result}");
    }
}