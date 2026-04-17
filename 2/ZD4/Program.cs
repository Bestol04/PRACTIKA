using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите x: ");
        double x = Convert.ToDouble(Console.ReadLine());

        double y;

        if (Math.Abs(x - Math.PI) < 0.001)
        {
            y = Math.Pow(Math.Sin(x), 2);
        }
        else if (x >= 1 && x <= 5)
        {
            y = Math.Log(x) + Math.Pow(Math.Cos(Math.Pow(x, 2)), 2);
        }
        else
        {
            y = 0;
        }

        Console.WriteLine($"y = {y}");
    }
}