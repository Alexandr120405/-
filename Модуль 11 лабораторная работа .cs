using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; private set; } = true;

        public void MarkAsLoaned()
        {
            IsAvailable = false;
        }

        public void MarkAsAvailable()
        {
            IsAvailable = true;
        }
    }

    public class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Loan> Loans { get; private set; } = new List<Loan>();

        public void BorrowBook(Book book)
        {
            if (book.IsAvailable)
            {
                var loan = new Loan
                {
                    Book = book,
                    Reader = this,
                    LoanDate = DateTime.Now
                };
                Loans.Add(loan);
                book.MarkAsLoaned();
                Console.WriteLine($"Book '{book.Title}' loaned to {Name}");
            }
            else
            {
                Console.WriteLine($"Book '{book.Title}' is not available.");
            }
        }

        public void ReturnBook(Book book)
        {
            var loan = Loans.FirstOrDefault(l => l.Book == book);
            if (loan != null)
            {
                loan.CompleteLoan();
                Loans.Remove(loan);
                Console.WriteLine($"Book '{book.Title}' returned by {Name}");
            }
            else
            {
                Console.WriteLine($"No active loan found for book '{book.Title}'");
            }
        }
    }

    public class Librarian
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }

        private List<Book> Books = new List<Book>();
        private List<Reader> Readers = new List<Reader>();

        public void AddBook(Book book)
        {
            Books.Add(book);
            Console.WriteLine($"Book '{book.Title}' added to the library.");
        }

        public void RemoveBook(Book book)
        {
            if (Books.Remove(book))
            {
                Console.WriteLine($"Book '{book.Title}' removed from the library.");
            }
            else
            {
                Console.WriteLine($"Book '{book.Title}' not found in the library.");
            }
        }

        public void RegisterReader(Reader reader)
        {
            Readers.Add(reader);
            Console.WriteLine($"Reader '{reader.Name}' registered.");
        }

        public List<Book> GetAvailableBooks()
        {
            return Books.Where(b => b.IsAvailable).ToList();
        }
    }

    public class Loan
    {
        public Book Book { get; set; }
        public Reader Reader { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; private set; }

        public void CompleteLoan()
        {
            ReturnDate = DateTime.Now;
            Book.MarkAsAvailable();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создаем библиотекаря
            var librarian = new Librarian { Id = 1, Name = "Anna", Position = "Manager" };

            // Добавляем книги
            var book1 = new Book { Title = "1984", Author = "George Orwell", ISBN = "12345" };
            var book2 = new Book { Title = "Brave New World", Author = "Aldous Huxley", ISBN = "67890" };

            librarian.AddBook(book1);
            librarian.AddBook(book2);

            // Регистрируем читателя
            var reader = new Reader { Id = 1, Name = "John Doe", Email = "john.doe@example.com" };
            librarian.RegisterReader(reader);

            // Читатель берет книгу
            reader.BorrowBook(book1);

            // Пытаемся взять ту же книгу снова
            reader.BorrowBook(book1);

            // Читатель возвращает книгу
            reader.ReturnBook(book1);

            // Проверяем доступные книги
            var availableBooks = librarian.GetAvailableBooks();
            Console.WriteLine("Available books:");
            availableBooks.ForEach(b => Console.WriteLine($"- {b.Title}"));
        }
    }
}
