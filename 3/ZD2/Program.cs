using System;

public class Task2
{
    public static void Main()
    {
        int[] numbers = { 1, 3, 5, 2, -4, 6, -1 };

        bool hasEvenAfterOdd = false;

        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i - 1] % 2 != 0 && numbers[i] % 2 == 0)
            {
                hasEvenAfterOdd = true;
                break;
            }
        }

        Console.WriteLine("Результат:");

        if (!hasEvenAfterOdd)
        {
            for (int i = numbers.Length - 1; i >= 0; i--)
                if (numbers[i] < 0)
                    Console.Write(numbers[i] + " ");
        }
        else
        {
            for (int i = numbers.Length - 1; i >= 0; i--)
                if (numbers[i] > 0)
                    Console.Write(numbers[i] + " ");
        }

        Console.WriteLine();

        Array.Sort(numbers);

        Console.Write("Введите k: ");
        int k = int.Parse(Console.ReadLine());

        int index = Array.BinarySearch(numbers, k);

        Console.WriteLine(index >= 0 ? $"Найден на позиции {index}" : "Не найден");
    }
}