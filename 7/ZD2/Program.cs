using System;

public delegate void KeyEventHandler(int keyCode);

public class Program
{
    public static void Main()
    {
        HandleKeyPress(13, OnKeyEnter);
        HandleKeyPress(27, OnKeyEscape);
    }

    public static void HandleKeyPress(int keyCode, KeyEventHandler handler)
    {
        Console.WriteLine($"Нажата клавиша с кодом: {keyCode}");
        handler(keyCode);
    }

    public static void OnKeyEnter(int keyCode)
    {
        Console.WriteLine("Нажат Enter.");
    }

    public static void OnKeyEscape(int keyCode)
    {
        Console.WriteLine("Нажат Escape.");
    }
}