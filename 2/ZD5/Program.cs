using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите трёхзначное число: ");
        int number = Convert.ToInt32(Console.ReadLine());

        int first = number / 100;
        int last = number % 10;

        if (first > last)
            Console.WriteLine("Первая цифра больше.");
        else if (last > first)
            Console.WriteLine("Последняя цифра больше.");
        else
            Console.WriteLine("Цифры равны.");
    }
}