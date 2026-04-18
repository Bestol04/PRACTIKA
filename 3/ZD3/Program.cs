using System;

public class Task3
{
    public static void Main()
    {
        Console.Write("Введите N (<10): ");
        int n = int.Parse(Console.ReadLine());

        Console.Write("Введите a: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Введите b: ");
        int b = int.Parse(Console.ReadLine());

        int[,] matrix = new int[n, n];
        Random random = new Random();

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                matrix[i, j] = random.Next(a, b + 1);

        // Вывод
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                Console.Write(matrix[i, j] + "\t");
            Console.WriteLine();
        }

        Console.Write("Введите M: ");
        int m = int.Parse(Console.ReadLine());

        int sum = 0, count = 0;

        foreach (int value in matrix)
        {
            if (value < m)
            {
                sum += value;
                count++;
            }
        }

        Console.WriteLine("Среднее: " + (count > 0 ? (double)sum / count : 0));

        for (int j = 0; j < n; j++)
        {
            int colSum = 0;
            for (int i = 0; i < n; i++)
                if (matrix[i, j] > 0)
                    colSum += matrix[i, j];

            Console.WriteLine($"Сумма столбца {j}: {colSum}");
        }
    }
}