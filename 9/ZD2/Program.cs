using System;
using System.Collections;

public class TerminalCommand
{
    public string CommandText { get; set; }
    public DateTime Timestamp { get; set; }

    public TerminalCommand(string commandText)
    {
        CommandText = commandText;
        Timestamp = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Timestamp}: {CommandText}";
    }
}

public class TerminalHistory
{
    private Stack commandStack = new Stack();

    public void AddCommand(string commandText)
    {
        commandStack.Push(new TerminalCommand(commandText));
    }

    public void UndoLastCommand()
    {
        if (commandStack.Count > 0)
        {
            TerminalCommand removed = (TerminalCommand)commandStack.Pop();
            Console.WriteLine($"Удалена команда: {removed.CommandText}");
        }
    }

    public void ShowLastCommand()
    {
        if (commandStack.Count > 0)
        {
            TerminalCommand command = (TerminalCommand)commandStack.Peek();
            Console.WriteLine($"Последняя команда: {command}");
        }
    }

    public void ShowAll()
    {
        foreach (TerminalCommand command in commandStack)
        {
            Console.WriteLine(command);
        }
    }

    public void FindByKeyword(string keyword)
    {
        foreach (TerminalCommand command in commandStack)
        {
            if (command.CommandText.Contains(keyword))
                Console.WriteLine($"Найдено: {command}");
        }
    }
}

public class Program
{
    public static void Main()
    {
        TerminalHistory history = new TerminalHistory();

        history.AddCommand("dir");
        history.AddCommand("cd Projects");
        history.AddCommand("git status");

        history.ShowLastCommand();
        history.FindByKeyword("git");

        history.UndoLastCommand();
        history.ShowAll();
    }
}