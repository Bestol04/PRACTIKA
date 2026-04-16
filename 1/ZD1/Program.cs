using System;

public class PoundsToKilograms
{
    public static void Main()
    {
        Console.Write("Введите вес в фунтах: ");
        double pounds = Convert.ToDouble(Console.ReadLine());
        double kilograms = pounds * 0.4095;

        Console.WriteLine($"{pounds} фунт(а/ов) — это {kilograms:F2} кг.");
    }
}