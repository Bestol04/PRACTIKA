using System;

public class Program
{
    public static void Main()
    {
        int number1 = -5;
        int number2 = 0;

        DetermineSign(in number1, out string result1);
        Console.WriteLine(result1);

        DetermineSign(in number2, out string result2);
        Console.WriteLine(result2);
    }

    public static void DetermineSign(in double number, out string result)
    {
        if (number > 0)
            result = "Положительное";
        else if (number < 0)
            result = "Отрицательное";
        else
            result = "Ноль";
    }
    public static void DetermineSign(in int number, out string result)
    {
        if (number > 0)
            result = "Положительное";
        else if (number < 0)
            result = "Отрицательное";
        else
            result = "Ноль";
    }
}