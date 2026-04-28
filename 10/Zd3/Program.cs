using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Deal
{
    public int Id { get; set; }
    public double Revenue { get; set; }
}

public class DealFileReader
{
    private const string FilePath = "file.data";

    public List<Deal> ReadDeals()
    {
        var deals = new List<Deal>();
        var lines = File.ReadAllLines(FilePath);

        foreach (var line in lines)
        {
            if (line.Contains("Id"))
            {
                int id = int.Parse(line.Split(':')[1].Replace(",", "").Trim());
                double revenue = double.Parse(
                    lines[Array.IndexOf(lines, line) + 1]
                    .Split(':')[1]
                    .Trim());

                deals.Add(new Deal { Id = id, Revenue = revenue });
            }
        }

        return deals;
    }
}
public class DealProcessor
{
    public Deal FindMostProfitableDeal(List<Deal> deals)
    {
        return deals.OrderByDescending(d => d.Revenue).FirstOrDefault();
    }
}

public class Program
{
    public static void Main()
    {
        DealFileReader reader = new DealFileReader();
        List<Deal> deals = reader.ReadDeals();

        DealProcessor processor = new DealProcessor();
        Deal best = processor.FindMostProfitableDeal(deals);

        Console.WriteLine($"Самая прибыльная сделка: ID={best.Id}, Revenue={best.Revenue}");
    }
}