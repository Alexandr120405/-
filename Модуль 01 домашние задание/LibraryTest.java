import java.util.ArrayList;
import java.util.List;

class Book {
    private String title, author, isbn;
    private int copies;

    public Book(String title, String author, String isbn, int copies) {
        this.title = title; this.author = author; this.isbn = isbn; this.copies = copies;
    }

    public String getTitle() { return title; }
    public String getIsbn() { return isbn; }
    public int getCopies() { return copies; }

    public boolean borrow() { 
        if (copies > 0) { copies--; return true; } 
        return false; 
    }

    public void returnCopy() { copies++; }
}

class Reader {
    private String name, readerId;

    public Reader(String name, String readerId) {
        this.name = name; this.readerId = readerId;
    }
    
    public String getReaderId() { return readerId; }
}

class Library {
    private List<Book> books = new ArrayList<>();
    private List<Reader> readers = new ArrayList<>();

    public void addBook(Book book) { books.add(book); }
    public void removeBook(String isbn) { books.removeIf(b -> b.getIsbn().equals(isbn)); }
    public void registerReader(Reader reader) { readers.add(reader); }

    public boolean borrowBook(String isbn, String readerId) {
        for (Book book : books) {
            if (book.getIsbn().equals(isbn) && book.borrow()) {
                System.out.println(readerId + " borrowed " + book.getTitle());
                return true;
            }
        }
        System.out.println("Book is not available.");
        return false;
    }

    public void returnBook(String isbn) {
        for (Book book : books) {
            if (book.getIsbn().equals(isbn)) {
                book.returnCopy();
                System.out.println("Returned " + book.getTitle());
                return;
            }
        }
        System.out.println("Book not found.");
    }
}

public class LibraryTest {
    public static void main(String[] args) {
        Library library = new Library();
        library.addBook(new Book("1984", "George Orwell", "123456789", 5));
        library.addBook(new Book("To Kill a Mockingbird", "Harper Lee", "987654321", 2));
        
        library.registerReader(new Reader("Alice", "R001"));
        library.registerReader(new Reader("Bob", "R002"));

        library.borrowBook("123456789", "R001");
        library.removeBook("987654321");

        System.out.println("\nTrying to borrow 'To Kill a Mockingbird':");
        library.borrowBook("987654321", "R002");

        System.out.println("\nReturning '1984':");
        library.returnBook("123456789");
        System.out.println("\nAvailable copies of '1984': " + library.books.get(0).getCopies());
    }
}
