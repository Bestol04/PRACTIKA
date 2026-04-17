using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите a: ");
        int a = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите b: ");
        int b = Convert.ToInt32(Console.ReadLine());

        int sum = 0;

        for (int i = a; i <= b; i++)
        {
            sum += (i * i);
        }

        Console.WriteLine($"Сумма квадратов: {sum}");
    }
}