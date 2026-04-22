using System;

namespace Task2
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public override string ToString() => $"{Name} ({Age} лет)";
    }

    public static class ArrayHelper
    {
        public static void ReverseArray(Person[] people)
        {
            if (people == null) return;

            int n = people.Length;
            for (int i = 0; i < n / 2; i++)
            {
                Person temp = people[i];
                people[i] = people[n - 1 - i];
                people[n - 1 - i] = temp;
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Person[] group = {
                new Person("Иван", 20),
                new Person("Анна", 25),
                new Person("Олег", 30)
            };

            Console.WriteLine("До реверса:");
            foreach (var p in group) Console.WriteLine(p);
            ArrayHelper.ReverseArray(group);

            Console.WriteLine("\nПосле реверса:");
            foreach (var p in group) Console.WriteLine(p);
        }
    }
}