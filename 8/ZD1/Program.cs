using System;

public class InvalidPhoneNumberException : Exception
{
    public InvalidPhoneNumberException()
        : base("Неверный формат телефонного номера.")
    {
    }

    public InvalidPhoneNumberException(string message)
        : base(message)
    {
    }

    public InvalidPhoneNumberException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
public class PhoneNumberValidator
{
    public void ValidatePhoneNumber(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new InvalidPhoneNumberException("Номер не может быть пустым.");

        if (phone.Length != 10)
            throw new InvalidPhoneNumberException("Номер должен содержать 10 цифр.");

        foreach (char symbol in phone)
        {
            if (!char.IsDigit(symbol))
                throw new InvalidPhoneNumberException("Номер содержит недопустимые символы.");
        }

        Console.WriteLine("Номер телефона корректен.");
    }
}

public class Program
{
    public static void Main()
    {
        PhoneNumberValidator validator = new PhoneNumberValidator();

        try
        {
            validator.ValidatePhoneNumber("12345AB789");
        }
        catch (InvalidPhoneNumberException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}