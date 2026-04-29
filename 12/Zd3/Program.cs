using System;

public interface ICommand
{
    void Execute();
}

public class TaskScheduler
{
    public void StartTask()
    {
        Console.WriteLine("Задача запущена.");
    }

    public void CancelTask()
    {
        Console.WriteLine("Задача отменена.");
    }
}

public class StartTaskCommand : ICommand
{
    private TaskScheduler scheduler;

    public StartTaskCommand(TaskScheduler scheduler)
    {
        this.scheduler = scheduler;
    }

    public void Execute()
    {
        scheduler.StartTask();
    }
}

public class CancelTaskCommand : ICommand
{
    private TaskScheduler scheduler;

    public CancelTaskCommand(TaskScheduler scheduler)
    {
        this.scheduler = scheduler;
    }

    public void Execute()
    {
        scheduler.CancelTask();
    }
}

public class SchedulerController
{
    private ICommand command;

    public void SetCommand(ICommand command)
    {
        this.command = command;
    }

    public void RunCommand()
    {
        command.Execute();
    }
}

public class Program
{
    public static void Main()
    {
        TaskScheduler scheduler = new TaskScheduler();

        ICommand startCommand = new StartTaskCommand(scheduler);
        ICommand cancelCommand = new CancelTaskCommand(scheduler);

        SchedulerController controller = new SchedulerController();

        controller.SetCommand(startCommand);
        controller.RunCommand();

        controller.SetCommand(cancelCommand);
        controller.RunCommand();
    }
}