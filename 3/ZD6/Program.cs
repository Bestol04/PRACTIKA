using System;
using System.Text;

public class Task6
{
    public static void Main()
    {
        string input = "aaabbbccdaa";
        StringBuilder result = new StringBuilder();

        result.Append(input[0]);

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] != input[i - 1])
                result.Append(input[i]);
        }

        Console.WriteLine(result.ToString());
    }
}