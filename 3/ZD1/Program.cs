using System;

public class Task1
{
    public static void Main()
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6 };

        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] % 2 == 0)
            {
                numbers[i] = 0;
            }
        }

        Console.WriteLine(string.Join(" ", numbers));
    }
}