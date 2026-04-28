using System;
using System.Collections.Generic;

public interface IPatientObserver
{
    void Update(string message);
}

public class Clinic
{
    private List<IPatientObserver> patients = new List<IPatientObserver>();

    public void Attach(IPatientObserver patient)
    {
        patients.Add(patient);
    }

    public void Detach(IPatientObserver patient)
    {
        patients.Remove(patient);
    }

    public void Notify(string message)
    {
        foreach (var patient in patients)
        {
            patient.Update(message);
        }
    }

    public void RemindAppointments()
    {
        string message = "Напоминание: завтра приём у врача.";
        Notify(message);
    }
}

public class Patient : IPatientObserver
{
    public string Name { get; }

    public Patient(string name)
    {
        Name = name;
    }

    public void Update(string message)
    {
        Console.WriteLine($"{Name} получил уведомление: {message}");
    }
}


public class Program
{
    public static void Main()
    {
        Clinic clinic = new Clinic();

        Patient patient1 = new Patient("Иван");
        Patient patient2 = new Patient("Мария");

        clinic.Attach(patient1);
        clinic.Attach(patient2);

        clinic.RemindAppointments();
    }
}