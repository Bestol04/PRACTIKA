using System;
using System.Collections.Generic;

namespace Task4
{
    public partial class Athlete
    {
        public string Name { get; set; }
        public string Sport { get; set; }
        public string Country { get; set; }
        public int RecordsCount { get; set; }

        public Athlete(string name, string sport, string country, int records)
        {
            Name = name;
            Sport = sport;
            Country = country;
            RecordsCount = records;
        }
    }

    public partial class Athlete
    {
        public void PrintShortInfo()
        {
            Console.WriteLine($"{Name} ({Country}) - {Sport}. Рекордов: {RecordsCount}");
        }
    }

    public class SportsEvent
    {
        private Athlete[] _athletes;

        public SportsEvent(Athlete[] athletes)
        {
            _athletes = athletes;
        }

        public List<Athlete> GetAthletesBySport(string sport)
        {
            List<Athlete> result = new List<Athlete>();
            foreach (var a in _athletes)
            {
                if (a.Sport.Equals(sport, StringComparison.OrdinalIgnoreCase))
                    result.Add(a);
            }
            return result;
        }

        public List<Athlete> GetRecordHolders()
        {
            List<Athlete> result = new List<Athlete>();
            foreach (var a in _athletes)
            {
                if (a.RecordsCount > 0)
                    result.Add(a);
            }
            return result;
        }
    }

    class Program
    {
        static void Main()
        {
            Athlete[] list = {
                new Athlete("Болт", "Бег", "Ямайка", 8),
                new Athlete("Феликс", "Бег", "США", 0),
                new Athlete("Фелпс", "Плавание", "США", 23)
            };

            SportsEvent games = new SportsEvent(list);

            Console.WriteLine("Рекордсмены:");
            foreach (var a in games.GetRecordHolders()) a.PrintShortInfo();

            Console.WriteLine("\nСпортсмены в дисциплине 'Плавание':");
            foreach (var a in games.GetAthletesBySport("Плавание")) a.PrintShortInfo();
        }
    }
}