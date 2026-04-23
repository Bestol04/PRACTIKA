using System;

public abstract class Game
{
    public abstract void Play();

    public virtual void DisplayRules()
    {
        Console.WriteLine("Общие правила игры.");
    }
}

public class Chess : Game
{
    public override void Play()
    {
        Console.WriteLine("Playing chess");
    }

    public override void DisplayRules()
    {
        Console.WriteLine("Правила шахмат: поставить мат королю.");
    }
}

public class Checkers : Game
{
    public override void Play()
    {
        Console.WriteLine("Playing checkers");
    }

    public override void DisplayRules()
    {
        Console.WriteLine("Правила шашек: побить все фигуры соперника.");
    }
}

public class Program
{
    public static void Main()
    {
        Game chess = new Chess();
        Game checkers = new Checkers();

        chess.Play();
        chess.DisplayRules();

        checkers.Play();
        checkers.DisplayRules();
    }
}