using System;

public class BasicOperations
{
    public static void Main()
    {
        Console.Write("Введите первое число: ");
        int firstNumber = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите второе число: ");
        int secondNumber = Convert.ToInt32(Console.ReadLine());

        int sum = firstNumber + secondNumber;
        int difference = firstNumber - secondNumber;
        int product = firstNumber * secondNumber;

        Console.WriteLine($"Сумма: {sum}");
        Console.WriteLine($"Разность: {difference}");
        Console.WriteLine($"Произведение: {product}");
    }
}