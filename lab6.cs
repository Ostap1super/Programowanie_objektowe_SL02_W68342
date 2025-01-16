using System;
using System.Collections.Generic;
using System.Linq;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
}

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }

    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }
}

public interface IPersonRepository
{
    void AddPerson(Person person);
    void RemovePerson(Person person);
    List<Person> GetAllPersons();
    
    // New methods
    List<Book> GetBorrowedBooks(Person person);
    void AddBorrowedBook(Person person, Book book);
}

public interface IBookRepository
{
    void AddBook(Book book);
    void RemoveBook(Book book);
    List<Book> GetAllBooks();
    
    // New methods
    List<Book> GetBooksByAuthor(string author);
    List<Book> GetBooksByYear(int year);
}

public class PersonRepository : IPersonRepository
{
    private List<Person> persons = new List<Person>();
    private Dictionary<Person, List<Book>> borrowedBooks = new Dictionary<Person, List<Book>>();

    public void AddPerson(Person person)
    {
        persons.Add(person);
        borrowedBooks[person] = new List<Book>();
    }

    public void RemovePerson(Person person)
    {
        persons.Remove(person);
        borrowedBooks.Remove(person);
    }

    public List<Person> GetAllPersons()
    {
        return persons;
    }

    // New method: Get borrowed books for a person
    public List<Book> GetBorrowedBooks(Person person)
    {
        if (borrowedBooks.ContainsKey(person))
        {
            return borrowedBooks[person];
        }
        return new List<Book>();
    }

    // New method: Add a borrowed book for a person
    public void AddBorrowedBook(Person person, Book book)
    {
        if (borrowedBooks.ContainsKey(person))
        {
            borrowedBooks[person].Add(book);
        }
        else
        {
            borrowedBooks[person] = new List<Book> { book };
        }
    }
}

public class BookRepository : IBookRepository
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        books.Remove(book);
    }

    public List<Book> GetAllBooks()
    {
        return books;
    }

    // New method: Get books by author
    public List<Book> GetBooksByAuthor(string author)
    {
        return books.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    // New method: Get books by year
    public List<Book> GetBooksByYear(int year)
    {
        return books.Where(b => b.Year == year).ToList();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Create some books
        var book1 = new Book("Book Title 1", "Author 1", 2020);
        var book2 = new Book("Book Title 2", "Author 2", 2021);
        var book3 = new Book("Book Title 3", "Author 1", 2020);

        // Create some persons
        var person1 = new Person("John Doe", 30);
        var person2 = new Person("Jane Doe", 25);

        // Create repositories
        var personRepo = new PersonRepository();
        var bookRepo = new BookRepository();

        // Add books to the book repository
        bookRepo.AddBook(book1);
        bookRepo.AddBook(book2);
        bookRepo.AddBook(book3);

        // Add persons to the person repository
        personRepo.AddPerson(person1);
        personRepo.AddPerson(person2);

        // Person1 borrows books
        personRepo.AddBorrowedBook(person1, book1);
        personRepo.AddBorrowedBook(person1, book3);

        // Check borrowed books for person1
        var borrowedBooks = personRepo.GetBorrowedBooks(person1);
        Console.WriteLine($"Books borrowed by {person1.Name}:");
        foreach (var book in borrowedBooks)
        {
            Console.WriteLine($"- {book.Title}");
        }

        // Find books by author "Author 1"
        var booksByAuthor = bookRepo.GetBooksByAuthor("Author 1");
        Console.WriteLine("\nBooks by Author 1:");
        foreach (var book in booksByAuthor)
        {
            Console.WriteLine($"- {book.Title}");
        }

        // Find books released in 2020
        var booksByYear = bookRepo.GetBooksByYear(2020);
        Console.WriteLine("\nBooks released in 2020:");
        foreach (var book in booksByYear)
        {
            Console.WriteLine($"- {book.Title}");
        }
    }
}
