using System;

public class FunctionTask
{
    public static void Main()
    {
        double x = -1;

        double expValue = Math.Exp(x);

        double absValue = Math.Abs(x);

        double underRoot = expValue + 1 + absValue;

        double sqrtValue = Math.Sqrt(underRoot);

        double atanValue = Math.Atan(sqrtValue);

        double y = 7 * Math.Pow(atanValue, 2);

        Console.WriteLine($"y = {y:F4}");
    }
}