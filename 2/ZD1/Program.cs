using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите кг конфет: ");
        double candyKg = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введите кг печенья: ");
        double cookieKg = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введите кг яблок: ");
        double appleKg = Convert.ToDouble(Console.ReadLine());
        double priceCandy = 250.0;
        double priceCookie = 150.0;
        double priceApple = 100.0;
        double totalCost = (candyKg * priceCandy) +
                           (cookieKg * priceCookie) +
                           (appleKg * priceApple);
        Console.WriteLine($"Общая стоимость: {totalCost}");
    }
}