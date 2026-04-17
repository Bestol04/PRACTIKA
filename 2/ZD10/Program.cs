using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите n: ");
        int n = Convert.ToInt32(Console.ReadLine());

        int steps = 0;

        while (n != 1)
        {
            if (n % 2 == 0)
                n /= 2;
            else
                n = 3 * n + 1;

            steps++;
        }

        Console.WriteLine($"Количество шагов: {steps}");
    }
}