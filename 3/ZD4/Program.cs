using System;

public class Task4
{
    public static void Main()
    {
        int[,] house = new int[12, 4];
        Random random = new Random();

        for (int i = 0; i < 12; i++)
            for (int j = 0; j < 4; j++)
                house[i, j] = random.Next(1, 6);

        int maxFloor = 0;
        int maxSum = 0;

        for (int i = 0; i < 12; i++)
        {
            int sum = 0;
            for (int j = 0; j < 4; j++)
                sum += house[i, j];

            if (sum > maxSum)
            {
                maxSum = sum;
                maxFloor = i + 1;
            }
        }

        Console.WriteLine($"Этаж с максимумом жильцов: {maxFloor}");
    }
}