using System;
using System.Text.RegularExpressions;

public class Task10
{
    public static void Main()
    {
        string text = "Привет. Как дела? Всё хорошо!";

        string[] sentences = Regex.Split(text, @"(?<=[.!?])");

        foreach (var sentence in sentences)
        {
            Console.WriteLine(sentence.Trim());
        }
    }
}