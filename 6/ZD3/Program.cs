using System;

public abstract class GameCharacter
{
    public string Name { get; }

    protected GameCharacter(string name)
    {
        Name = name;
    }
}

public interface IMeleeFighter
{
    void AttackWithSword();
}

public interface IRangedFighter
{
    void Shoot();
}

public class Knight : GameCharacter, IMeleeFighter
{
    public Knight(string name) : base(name) { }

    public void AttackWithSword()
    {
        Console.WriteLine($"{Name} атакует мечом!");
    }
}

public class Archer : GameCharacter, IRangedFighter
{
    public Archer(string name) : base(name) { }

    public void Shoot()
    {
        Console.WriteLine($"{Name} стреляет из лука!");
    }
}

public class Program
{
    public static void Main()
    {
        GameCharacter[] characters =
        {
            new Knight("Артур"),
            new Archer("Робин"),
            new Archer("Леголас")
        };

        Console.WriteLine("Лучники в игре:");

        foreach (GameCharacter character in characters)
        {
            if (character is IRangedFighter ranged)
            {
                Console.WriteLine(character.Name);
                ranged.Shoot();
            }
        }
    }
}