using System;

public class Guide
{
    public string Name { get; }

    public Guide(string name)
    {
        Name = name;
    }
}

public class Customer
{
    public string Name { get; }

    public Customer(string name)
    {
        Name = name;
    }
}

public class TourPackage
{
    public string Destination { get; }

    public TourPackage(string destination)
    {
        Destination = destination;
    }
}

public class TravelAgency
{
    public string AgencyName { get; }
    private Guide[] guides;

    private TourPackage tourPackage;

    public TravelAgency(string agencyName, Guide[] guides, string destination)
    {
        AgencyName = agencyName;
        this.guides = guides;
        tourPackage = new TourPackage(destination);
    }

    public void BookTour(Customer customer)
    {
        Console.WriteLine($"{customer.Name} забронировал тур в {tourPackage.Destination} через {AgencyName}");
    }

    public void ShowGuides()
    {
        Console.WriteLine($"Гиды агентства {AgencyName}:");
        foreach (Guide guide in guides)
        {
            Console.WriteLine($"- {guide.Name}");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Guide guide1 = new Guide("Олег");
        Guide guide2 = new Guide("Анна");

        TravelAgency[] agencies =
        {
            new TravelAgency("SunTravel", new Guide[] { guide1, guide2 }, "Италия"),
            new TravelAgency("BestTours", new Guide[] { guide2 }, "Франция")
        };

        Customer customer = new Customer("Дмитрий");

        foreach (TravelAgency agency in agencies)
        {
            agency.ShowGuides();
            agency.BookTour(customer);
            Console.WriteLine();
        }
    }
}