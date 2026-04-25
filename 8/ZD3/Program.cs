using System;

public class InvalidPhoneNumberException : Exception
{
    public InvalidPhoneNumberException(string message)
        : base(message)
    {
    }
}

public class PhoneNumberValidator
{
    public void ValidatePhoneNumber(string phone)
    {
        if (string.IsNullOrEmpty(phone))
            throw new InvalidPhoneNumberException("Номер пустой.");

        if (phone.Length != 10 || !IsDigitsOnly(phone))
            throw new InvalidPhoneNumberException("Неверный формат номера.");

        Console.WriteLine("Номер корректен.");
    }

    private bool IsDigitsOnly(string phone)
    {
        foreach (char c in phone)
        {
            if (!char.IsDigit(c))
                return false;
        }
        return true;
    }
}

public class Program
{
    public static void Main()
    {
        PhoneNumberValidator validator = new PhoneNumberValidator();

        try
        {
            validator.ValidatePhoneNumber("1234567");
        }
        catch (InvalidPhoneNumberException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}