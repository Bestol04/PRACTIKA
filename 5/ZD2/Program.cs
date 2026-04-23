using System;

public class Program
{
    public static void Main()
    {
        double a1 = 1, b1 = 2, c1 = 3;
        double a2 = 4, b2 = 5, c2 = 6;

        ShiftRight3(ref a1, ref b1, ref c1);
        ShiftRight3(ref a2, ref b2, ref c2);

        Console.WriteLine($"Первый набор: {a1}, {b1}, {c1}");
        Console.WriteLine($"Второй набор: {a2}, {b2}, {c2}");
    }

    public static void ShiftRight3(ref double first, ref double second, ref double third)
    {
        double temp = third;

        third = second;
        second = first;
        first = temp;
    }
}