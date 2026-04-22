using System;
using System.Linq;

namespace Task3
{
    // Абстрактный класс - база для всех питомцев
    public abstract class Pet
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public string OwnerName { get; set; }

        protected Pet(string name, string breed, int age, string ownerName)
        {
            Name = name;
            Breed = breed;
            Age = age;
            OwnerName = ownerName;
        }
    }
    public sealed class Dog : Pet
    {
        public Dog(string name, string breed, int age, string ownerName)
            : base(name, breed, age, ownerName) { }
    }

    public sealed class Cat : Pet
    {
        public Cat(string name, string breed, int age, string ownerName)
            : base(name, breed, age, ownerName) { }
    }

    public class VeterinaryClinic
    {
        private Pet[] _pets;

        public VeterinaryClinic(Pet[] pets)
        {
            _pets = pets;
        }

        public Pet GetOldestPet()
        {
            if (_pets == null || _pets.Length == 0) return null;

            Pet oldest = _pets[0];
            foreach (var pet in _pets)
            {
                if (pet.Age > oldest.Age)
                    oldest = pet;
            }
            return oldest;
        }

        public Pet[] GetPetsByOwner(string ownerName)
        {

            int count = 0;
            foreach (var p in _pets)
                if (p.OwnerName.Equals(ownerName, StringComparison.OrdinalIgnoreCase)) count++;

            Pet[] result = new Pet[count];
            int index = 0;
            foreach (var p in _pets)
            {
                if (p.OwnerName.Equals(ownerName, StringComparison.OrdinalIgnoreCase))
                {
                    result[index++] = p;
                }
            }
            return result;
        }
    }

    class Program
    {
        static void Main()
        {
            Pet[] initialPets = {
                new Dog("Шарик", "Овчарка", 5, "Игорь"),
                new Cat("Мурка", "Сиамская", 12, "Мария"),
                new Dog("Бобик", "Такса", 3, "Игорь")
            };

            VeterinaryClinic clinic = new VeterinaryClinic(initialPets);

            Pet oldest = clinic.GetOldestPet();
            Console.WriteLine($"Самый старый: {oldest?.Name}, Возраст: {oldest?.Age}");

            Console.WriteLine("\nПитомцы Игоря:");
            foreach (var p in clinic.GetPetsByOwner("Игорь"))
            {
                Console.WriteLine($"- {p.Name} ({p.Breed})");
            }
        }
    }
}