using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите a: ");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите b: ");
        double b = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите количество шагов m: ");
        int m = Convert.ToInt32(Console.ReadLine());

        double h = (b - a) / m;
        double x = a;

        for (int i = 0; i <= m; i++)
        {
            double y = x * Math.Exp(-x);
            Console.WriteLine($"x: {x:F2}, y: {y:F4}");
            x += h;
        }
    }
}