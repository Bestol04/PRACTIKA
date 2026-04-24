using System;

public delegate double CurrencyConverter(double amount);

public class DollarToEuro
{
    private const double ExchangeRate = 0.92;

    public double Convert(double amount)
    {
        return amount * ExchangeRate;
    }
}

public class EuroToYen
{
    private const double ExchangeRate = 165.3;

    public double Convert(double amount)
    {
        return amount * ExchangeRate;
    }
}

public class Program
{
    public static void Main()
    {
        double amount = 100;

        DollarToEuro dollarToEuro = new DollarToEuro();
        EuroToYen euroToYen = new EuroToYen();

        CurrencyConverter converter;

        converter = dollarToEuro.Convert;
        Console.WriteLine($"100 USD → {converter(amount)} EUR");

        converter = euroToYen.Convert;
        Console.WriteLine($"100 EUR → {converter(amount)} YEN");
    }
}