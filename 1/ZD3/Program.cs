using System;

public class FormulaCalculation
{
    public static void Main()
    {
        Console.Write("Введите угол α в градусах: ");
        double alphaDegrees = Convert.ToDouble(Console.ReadLine());

        double alpha = alphaDegrees * Math.PI / 180;

        double numerator = Math.Sin(2 * alpha) + Math.Sin(5 * alpha) - Math.Sin(3 * alpha);
        double denominator = Math.Cos(alpha) + 1 - 2 * Math.Pow(Math.Sin(2 * alpha), 2);

        double z1 = numerator / denominator;
        double z2 = 2 * Math.Sin(alpha);

        Console.WriteLine($"z1 = {z1:F4}");
        Console.WriteLine($"z2 = {z2:F4}");
    }
}