using System;
using System.Collections.Generic;
using System.Linq;

// Интерфейсы
interface ICatalog
{
    void AddBook(Book book);
    void RemoveBook(string isbn);
    Book SearchBookByTitle(string title);
    IEnumerable<Book> FilterBooksByAuthor(string author);
    IEnumerable<Book> FilterBooksByGenre(string genre);
}

interface ILoanSystem
{
    void IssueLoan(Reader reader, Book book);
    void ReturnLoan(Reader reader, Book book);
}

// Классы
class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string ISBN { get; set; }
    public bool IsAvailable { get; set; } = true;

    public Book(string title, string author, string genre, string isbn)
    {
        Title = title;
        Author = author;
        Genre = genre;
        ISBN = isbn;
    }

    public override string ToString()
    {
        return $"{Title} by {Author}, Genre: {Genre}, ISBN: {ISBN}, Available: {IsAvailable}";
    }
}

class Reader
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TicketNumber { get; set; }

    public Reader(string firstName, string lastName, string ticketNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        TicketNumber = ticketNumber;
    }

    public override string ToString()
    {
        return $"Reader: {FirstName} {LastName}, Ticket Number: {TicketNumber}";
    }
}

class Librarian
{
    private ICatalog _catalog;
    private ILoanSystem _loanSystem;

    public Librarian(ICatalog catalog, ILoanSystem loanSystem)
    {
        _catalog = catalog;
        _loanSystem = loanSystem;
    }

    public void AddBook(Book book)
    {
        _catalog.AddBook(book);
    }

    public void IssueBook(Reader reader, string title)
    {
        var book = _catalog.SearchBookByTitle(title);
        if (book != null && book.IsAvailable)
        {
            _loanSystem.IssueLoan(reader, book);
        }
        else
        {
            Console.WriteLine("Book is not available.");
        }
    }

    public void ReturnBook(Reader reader, string title)
    {
        var book = _catalog.SearchBookByTitle(title);
        if (book != null)
        {
            _loanSystem.ReturnLoan(reader, book);
        }
    }
}

class LibraryCatalog : ICatalog
{
    private List<Book> _books = new List<Book>();

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public void RemoveBook(string isbn)
    {
        _books.RemoveAll(b => b.ISBN == isbn);
    }

    public Book SearchBookByTitle(string title)
    {
        return _books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Book> FilterBooksByAuthor(string author)
    {
        return _books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Book> FilterBooksByGenre(string genre)
    {
        return _books.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
    }

    public void DisplayBooks()
    {
        foreach (var book in _books)
        {
            Console.WriteLine(book);
        }
    }
}

class LoanSystem : ILoanSystem
{
    private Dictionary<Reader, List<Book>> _loans = new Dictionary<Reader, List<Book>>();

    public void IssueLoan(Reader reader, Book book)
    {
        if (!_loans.ContainsKey(reader))
        {
            _loans[reader] = new List<Book>();
        }

        _loans[reader].Add(book);
        book.IsAvailable = false;
        Console.WriteLine($"Loan issued: {book.Title} to {reader.FirstName} {reader.LastName}");
    }

    public void ReturnLoan(Reader reader, Book book)
    {
        if (_loans.ContainsKey(reader) && _loans[reader].Remove(book))
        {
            book.IsAvailable = true;
            Console.WriteLine($"Book returned: {book.Title} by {reader.FirstName} {reader.LastName}");
        }
    }

    public void DisplayLoans()
    {
        foreach (var loan in _loans)
        {
            Console.WriteLine($"{loan.Key}: {string.Join(", ", loan.Value.Select(b => b.Title))}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var catalog = new LibraryCatalog();
        var loanSystem = new LoanSystem();
        var librarian = new Librarian(catalog, loanSystem);

        // Добавление книг
        librarian.AddBook(new Book("1984", "George Orwell", "Dystopian", "12345"));
        librarian.AddBook(new Book("Brave New World", "Aldous Huxley", "Dystopian", "67890"));

        // Добавление читателя
        var reader = new Reader("John", "Doe", "0001");

        // Работа с библиотекой
        Console.WriteLine("All books:");
        catalog.DisplayBooks();

        librarian.IssueBook(reader, "1984");
        Console.WriteLine("After issuing a book:");
        catalog.DisplayBooks();

        loanSystem.DisplayLoans();

        librarian.ReturnBook(reader, "1984");
        Console.WriteLine("After returning a book:");
        catalog.DisplayBooks();
    }
}
