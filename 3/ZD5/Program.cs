using System;

public class Task5
{
    public static void Main()
    {
        int[][] jaggedArray =
        {
            new int[] {1, 3, 5},
            new int[] {2, 4, 6, 8},
            new int[] {0, 9}
        };

        int target = 4;

        foreach (var row in jaggedArray)
        {
            Array.Sort(row);
            int index = Array.BinarySearch(row, target);
            if (index >= 0)
            {
                Console.WriteLine("Найден");
                return;
            }
        }

        Console.WriteLine("Не найден");
    }
}