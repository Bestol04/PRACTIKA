using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите трёхзначное число: ");
        int number = Convert.ToInt32(Console.ReadLine());

        int d1 = number / 100;
        int d2 = (number / 10) % 10;
        int d3 = number % 10;

        if ((d2 - d1) == (d3 - d2))
            Console.WriteLine("Это арифметическая прогрессия.");
        else
            Console.WriteLine("Это НЕ арифметическая прогрессия.");
    }
}