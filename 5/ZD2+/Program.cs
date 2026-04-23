using System;

public class Program
{
    public static void Main()
    {
        double circle = CalculateCircumference(5);
        double rectangle = CalculateCircumference(4, 6);

        Console.WriteLine($"Окружность круга: {circle:F2}");
        Console.WriteLine($"Периметр прямоугольника: {rectangle:F2}");
    }

    public static double CalculateCircumference(double radius)
    {
        return 2 * Math.PI * radius;
    }

    public static double CalculateCircumference(double length, double width)
    {
        return 2 * (length + width);
    }
}