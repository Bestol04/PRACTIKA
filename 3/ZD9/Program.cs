using System;
using System.Text;

public class Task9
{
    public static void Main()
    {
        string input = "Hello! 123 @#World";

        StringBuilder result = new StringBuilder();

        foreach (char c in input)
        {
            if (char.IsLetterOrDigit(c))
                result.Append(c);
        }

        Console.WriteLine(result.ToString());
    }
}