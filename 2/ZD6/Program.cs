using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите номер человека: ");
        int personNumber = Convert.ToInt32(Console.ReadLine());

        switch (personNumber)
        {
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
                Console.WriteLine("Человек грязный.");
                break;

            case 3:
            case 9:
            case 12:
                Console.WriteLine("Человек исцарапанный.");
                break;

            default:
                Console.WriteLine("Человек в порядке.");
                break;
        }
    }
}