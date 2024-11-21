//UML-диаграмма классов (Class Diagram) для системы управления библиотекой
+---------------+         +------------+          +------------+
|    Library    |<>-------|    Book    |          |  Librarian |
|---------------|         |------------|          |------------|
| +Books[]      |         | +Title     |          | +Name      |
|---------------|         | +Author    |          |------------|
| +AddBook()    |         | +ISBN      |          | +Manage()  |
| +RemoveBook() |         | +Status    |          +------------+
+---------------+         +------------+                 |
         ^                                          (manages)
         |
       (has)                                     +---------------+
         |                                       |    Reader     |
         |                                       |---------------|
+---------------+                               | +Name         |
|   Composition  |<-----------------------------| +Borrowed[]   |
+---------------+                               | +BorrowBook() |
                                                | +ReturnBook() |
                                                +---------------+
using System;
using System.Collections.Generic;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool IsAvailable { get; set; } = true;

    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
    }
}

class Reader
{
    public string Name { get; set; }
    public List<Book> BorrowedBooks { get; private set; } = new List<Book>();

    public Reader(string name)
    {
        Name = name;
    }

    public void BorrowBook(Book book)
    {
        if (book.IsAvailable)
        {
            BorrowedBooks.Add(book);
            book.IsAvailable = false;
            Console.WriteLine($"{Name} borrowed \"{book.Title}\".");
        }
        else
        {
            Console.WriteLine($"\"{book.Title}\" is not available.");
        }
    }

    public void ReturnBook(Book book)
    {
        if (BorrowedBooks.Contains(book))
        {
            BorrowedBooks.Remove(book);
            book.IsAvailable = true;
            Console.WriteLine($"{Name} returned \"{book.Title}\".");
        }
        else
        {
            Console.WriteLine($"{Name} does not have \"{book.Title}\".");
        }
    }
}

class Librarian
{
    public string Name { get; set; }

    public Librarian(string name)
    {
        Name = name;
    }

    public void ManageLibrary()
    {
        Console.WriteLine($"{Name} is managing the library.");
    }
}

class Library
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine($"Book \"{book.Title}\" added to the library.");
    }

    public void RemoveBook(Book book)
    {
        if (books.Contains(book))
        {
            books.Remove(book);
            Console.WriteLine($"Book \"{book.Title}\" removed from the library.");
        }
        else
        {
            Console.WriteLine($"Book \"{book.Title}\" is not in the library.");
        }
    }

    public void DisplayBooks()
    {
        Console.WriteLine("Books in the library:");
        foreach (var book in books)
        {
            string status = book.IsAvailable ? "Available" : "Borrowed";
            Console.WriteLine($"- {book.Title} by {book.Author} (ISBN: {book.ISBN}, Status: {status})");
        }
    }

    public Book SearchBookByTitle(string title)
    {
        return books.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public Book SearchBookByAuthor(string author)
    {
        return books.Find(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library();
        Librarian librarian = new Librarian("Alice");
        Reader reader = new Reader("Bob");

        Book book1 = new Book("The Great Gatsby", "F. Scott Fitzgerald", "1234567890");
        Book book2 = new Book("1984", "George Orwell", "0987654321");

        library.AddBook(book1);
        library.AddBook(book2);

        library.DisplayBooks();

        reader.BorrowBook(book1);
        library.DisplayBooks();

        reader.ReturnBook(book1);
        library.DisplayBooks();

        librarian.ManageLibrary();

        var foundBook = library.SearchBookByTitle("1984");
        Console.WriteLine($"Found book: {foundBook.Title} by {foundBook.Author}");
    }
}
