using System;

public class FunctionCalculation
{
    public static void Main()
    {
        double x = -1;

        double expValue = Math.Exp(x);

        double absoluteValue = Math.Abs(x);

        double sqrtExpression = Math.Sqrt(expValue + 1 + absoluteValue);

        double atanValue = Math.Atan(sqrtExpression);

        double y = 7 * Math.Pow(atanValue, 2);

        Console.WriteLine($"y = {y:F4}");
    }
}