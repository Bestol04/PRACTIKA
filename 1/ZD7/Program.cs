using System;

public class TrapezoidArea
{
    public static void Main()
    {
        Console.Write("Введите сторону a: ");
        double sideA = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите сторону b: ");
        double sideB = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите сторону c: ");
        double sideC = Convert.ToDouble(Console.ReadLine());

        Console.Write("Введите сторону d: ");
        double sideD = Convert.ToDouble(Console.ReadLine());

        double semiPerimeter = (sideA + sideB + sideC + sideD) / 2;

        double baseDifference = Math.Abs(sideA - sideB);

        if (baseDifference == 0)
        {
            Console.WriteLine("Ошибка: основания не должны быть равны!");
            return;
        }

        double sqrtExpression = (semiPerimeter - sideA) *
                                (semiPerimeter - sideB) *
                                (semiPerimeter - sideA - sideC) *
                                (semiPerimeter - sideA - sideD);

        if (sqrtExpression < 0)
        {
            Console.WriteLine("Ошибка: невозможно вычислить корень (некорректные стороны).");
            return;
        }

        double area = (sideA + sideB) / (4 * baseDifference) * Math.Sqrt(sqrtExpression);

        Console.WriteLine($"Площадь трапеции: {area:F4}");
    }
}