using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите a: ");
        int a = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите b: ");
        int b = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Кратные 3 (for):");
        for (int i = a; i <= b; i++)
        {
            if (i % 3 == 0)
                Console.Write(i + " ");
        }

        Console.WriteLine("\nКратные 3 (while):");
        int i2 = a;
        while (i2 <= b)
        {
            if (i2 % 3 == 0)
                Console.Write(i2 + " ");
            i2++;
        }

        Console.WriteLine("\nКратные 3 (do-while):");
        int i3 = a;
        do
        {
            if (i3 % 3 == 0)
                Console.Write(i3 + " ");
            i3++;
        } while (i3 <= b);
    }
}