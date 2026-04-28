using System;

public class Character
{
    public string Class { get; set; }
    public int Health { get; set; }
    public int Mana { get; set; }
    public int Strength { get; set; }

    public void ShowInfo()
    {
        Console.WriteLine($"Класс: {Class}");
        Console.WriteLine($"Здоровье: {Health}");
        Console.WriteLine($"Мана: {Mana}");
        Console.WriteLine($"Сила: {Strength}");
        Console.WriteLine();
    }
}

public interface ICharacterBuilder
{
    void SetClass();
    void SetHealth();
    void SetMana();
    void SetStrength();
    Character GetCharacter();
}

public class WarriorBuilder : ICharacterBuilder
{
    private Character character = new Character();

    public void SetClass() => character.Class = "Warrior";
    public void SetHealth() => character.Health = 150;
    public void SetMana() => character.Mana = 30;
    public void SetStrength() => character.Strength = 100;

    public Character GetCharacter() => character;
}

public class MageBuilder : ICharacterBuilder
{
    private Character character = new Character();

    public void SetClass() => character.Class = "Mage";
    public void SetHealth() => character.Health = 80;
    public void SetMana() => character.Mana = 200;
    public void SetStrength() => character.Strength = 40;

    public Character GetCharacter() => character;
}

public class ArcherBuilder : ICharacterBuilder
{
    private Character character = new Character();

    public void SetClass() => character.Class = "Archer";
    public void SetHealth() => character.Health = 100;
    public void SetMana() => character.Mana = 60;
    public void SetStrength() => character.Strength = 70;

    public Character GetCharacter() => character;
}
public class CharacterDirector
{
    public void Construct(ICharacterBuilder builder)
    {
        builder.SetClass();
        builder.SetHealth();
        builder.SetMana();
        builder.SetStrength();
    }
}

public class Program
{
    public static void Main()
    {
        CharacterDirector director = new CharacterDirector();

        ICharacterBuilder warriorBuilder = new WarriorBuilder();
        director.Construct(warriorBuilder);
        Character warrior = warriorBuilder.GetCharacter();
        warrior.ShowInfo();
    }
}