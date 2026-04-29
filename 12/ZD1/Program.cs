using System;

public interface IWeapon
{
    void Attack();
}

public class Sword : IWeapon
{
    public void Attack()
    {
        Console.WriteLine("Меч наносит рубящий удар!");
    }
}

public class Bow : IWeapon
{
    public void Attack()
    {
        Console.WriteLine("Лук выпускает стрелу!");
    }
}

public class Crossbow : IWeapon
{
    public void Attack()
    {
        Console.WriteLine("Арбалет стреляет болтом!");
    }
}

public abstract class WeaponFactory
{
    public abstract IWeapon CreateWeapon();
}

public class SwordFactory : WeaponFactory
{
    public override IWeapon CreateWeapon()
    {
        return new Sword();
    }
}

public class BowFactory : WeaponFactory
{
    public override IWeapon CreateWeapon()
    {
        return new Bow();
    }
}

public class CrossbowFactory : WeaponFactory
{
    public override IWeapon CreateWeapon()
    {
        return new Crossbow();
    }
}


public class Program
{
    public static void Main()
    {
        WeaponFactory factory = new SwordFactory();
        IWeapon weapon = factory.CreateWeapon();
        weapon.Attack();

        factory = new BowFactory();
        weapon = factory.CreateWeapon();
        weapon.Attack();
    }
}