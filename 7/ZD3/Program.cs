using System;

public delegate void WaterLevelChangedHandler(int level);

public class WaterTankSensor
{
    public event WaterLevelChangedHandler WaterLevelChanged;

    public void SetWaterLevel(int level)
    {
        Console.WriteLine($"Уровень воды: {level}%");
        WaterLevelChanged?.Invoke(level);
    }
}

public class PumpController
{
    public void OnWaterLevelChanged(int level)
    {
        if (level < 30)
            Console.WriteLine("Насос включён для заполнения.");
    }
}

public class WarningSystem
{
    public void OnWaterLevelChanged(int level)
    {
        if (level > 90)
            Console.WriteLine("ВНИМАНИЕ! Возможное переполнение!");
    }
}

public class Program
{
    public static void Main()
    {
        WaterTankSensor sensor = new WaterTankSensor();
        PumpController pump = new PumpController();
        WarningSystem warning = new WarningSystem();

        sensor.WaterLevelChanged += pump.OnWaterLevelChanged;
        sensor.WaterLevelChanged += warning.OnWaterLevelChanged;

        sensor.SetWaterLevel(20);
        sensor.SetWaterLevel(95);
    }
}