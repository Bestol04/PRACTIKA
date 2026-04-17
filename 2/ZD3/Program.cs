using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите n: ");
        int n = Convert.ToInt32(Console.ReadLine());

        int sum = 0;

        for (int i = 1; i <= n; i++)
        {
            int term = 2 * i - 1;
            sum += term;
            Console.WriteLine($"Шаг {i}: сумма = {sum}");
        }
    }
}