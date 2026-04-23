using System;

public class Program
{
    public static void Main()
    {
        int[] numbers = { 1, 2, 3, 2, 4, 2 };
        int valueToFind = 2;

        int count = CountOccurrences(numbers, valueToFind);

        Console.WriteLine($"Количество вхождений: {count}");
    }

    public static int CountOccurrences(int[] array, int value)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));

        return CountOccurrencesRecursive(array, value, 0);
    }

    private static int CountOccurrencesRecursive(int[] array, int value, int index)
    {
        if (index >= array.Length)
            return 0;

        int currentCount = array[index] == value ? 1 : 0;

        return currentCount + CountOccurrencesRecursive(array, value, index + 1);
    }
}