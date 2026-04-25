using System;
using System.Net.Mail;

public class EmailSendingException : Exception
{
    public EmailSendingException() { }

    public EmailSendingException(string message)
        : base(message) { }

    public EmailSendingException(string message, Exception innerException)
        : base(message, innerException) { }
}
public class EmailSender
{
    public void SendEmail(string email)
    {
        throw new SmtpException("SMTP сервер недоступен.");
    }
}

public class EmailManager
{
    private readonly EmailSender emailSender = new EmailSender();

    public void Send(string email)
    {
        try
        {
            emailSender.SendEmail(email);
        }
        catch (SmtpException ex)
        {
            throw new EmailSendingException("Ошибка при отправке email.", ex);
        }
    }
}
public class Program
{
    public static void Main()
    {
        EmailManager manager = new EmailManager();

        try
        {
            manager.Send("test@mail.com");
        }
        catch (EmailSendingException ex)
        {
            Console.WriteLine("Произошла ошибка:");
            Console.WriteLine(ex.Message);
            Console.WriteLine("Стек вызовов:");
            Console.WriteLine(ex.StackTrace);

            if (ex.InnerException != null)
            {
                Console.WriteLine("Внутреннее исключение:");
                Console.WriteLine(ex.InnerException.Message);
            }
        }
    }
}