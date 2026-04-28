using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
}

public class BookJsonWriter
{
    private const string FilePath = "file.data";

    public void WriteBooksAsJson(List<Book> books)
    {
        StringBuilder json = new StringBuilder();
        json.AppendLine("[");

        for (int i = 0; i < books.Count; i++)
        {
            Book book = books[i];
            json.AppendLine("  {");
            json.AppendLine($"    \"Title\": \"{book.Title}\",");
            json.AppendLine($"    \"Author\": \"{book.Author}\",");
            json.AppendLine($"    \"Year\": {book.Year}");
            json.Append("  }");

            if (i < books.Count - 1)
                json.Append(",");

            json.AppendLine();
        }

        json.AppendLine("]");

        File.WriteAllText(FilePath, json.ToString());
    }
}
public class Program
{
    public static void Main()
    {
        List<Book> books = new List<Book>
        {
            new Book { Title = "1984", Author = "Orwell", Year = 1949 },
            new Book { Title = "Dune", Author = "Herbert", Year = 1965 }
        };

        BookJsonWriter writer = new BookJsonWriter();
        writer.WriteBooksAsJson(books);

        Console.WriteLine("Книги записаны в file.data");
    }
}