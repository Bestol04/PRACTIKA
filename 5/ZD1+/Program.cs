using System;

public class Program
{
    public static void Main()
    {
        int d1 = 18, m1 = 4, y1 = 2026;
        int d2 = 19, m2 = 4, y2 = 2026;
        int d3 = 20, m3 = 4, y3 = 2026;

        NextWeekday(ref d1, ref m1, ref y1);
        NextWeekday(ref d2, ref m2, ref y2);
        NextWeekday(ref d3, ref m3, ref y3);

        Console.WriteLine($"{d1}.{m1}.{y1}");
        Console.WriteLine($"{d2}.{m2}.{y2}");
        Console.WriteLine($"{d3}.{m3}.{y3}");
    }

    public static void NextWeekday(ref int day, ref int month, ref int year)
    {
        DateTime date = new DateTime(year, month, day);

        do
        {
            date = date.AddDays(1);
        }
        while (date.DayOfWeek == DayOfWeek.Saturday ||
               date.DayOfWeek == DayOfWeek.Sunday);

        day = date.Day;
        month = date.Month;
        year = date.Year;
    }
}