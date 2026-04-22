using System;

namespace Task1
{
    public class A
    {
        private int _a;
        private int _b;

        public A(int a, int b)
        {
            _a = a;
            _b = b;
        }

        public double CalculateExpression()
        {

            if (_a == 0 || _b == 0)
            {
                Console.WriteLine("Ошибка: деление на ноль невозможно.");
                return 0;
            }

            double part1 = 1.0 / Math.Pow(_a, 2);
            double part2 = 1.0 / Math.Pow(_b, 3);
            return part1 - part2;
        }

        public double CalculateCubeOfSum()
        {
            int sum = _a + _b;
            return Math.Pow(sum, 3);
        }

        public int AValue => _a;
        public int BValue => _b;
    }

    class Program
    {
        static void Main(string[] args)
        {
            A myObject = new A(2, 3);

            Console.WriteLine($"Числа: a = {myObject.AValue}, b = {myObject.BValue}");

            double result1 = myObject.CalculateExpression();
            Console.WriteLine($"Результат выражения (1/a^2 - 1/b^3): {result1:F4}");

            double result2 = myObject.CalculateCubeOfSum();
            Console.WriteLine($"Куб суммы (a + b)^3: {result2}");
        }
    }
}