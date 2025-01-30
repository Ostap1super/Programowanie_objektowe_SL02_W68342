using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Data.SQLite;

// Models
public class BorrowedBook : IEntity<long>, IHasCreationTime
{
    public long Id { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.Now;
    public int BookId { get; set; }
    public Book Book { get; set; }
    public long PersonId { get; set; }
    public Person Person { get; set; }
}

public class Person
{
    public long Id { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.Now;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public List<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();
}

public class Book
{
    public long Id { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.Now;
    public string Title { get; set; }
    public string Author { get; set; }
    public DateTime PublishDate { get; set; }

    public List<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();
}

// Repository Implementation - FileBookRepository
public class FileBookRepository : IBookRepository
{
    private readonly string _filePath;

    public FileBookRepository(string filePath)
    {
        _filePath = filePath;
    }

    public void Create(Book entity)
    {
        var books = GetAll();
        books.Add(entity);
        Save(books);
    }

    public Book Get(long id)
    {
        var books = GetAll();
        return books.Find(b => b.Id == id);
    }

    public void Delete(long id)
    {
        var books = GetAll();
        var bookToRemove = books.Find(b => b.Id == id);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
            Save(books);
        }
    }

    private List<Book> GetAll()
    {
        if (!File.Exists(_filePath))
            return new List<Book>();

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
    }

    private void Save(List<Book> books)
    {
        var json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}

// SQLite Database Creator
namespace WebApplication1.Utilities
{
    public static class DbCreator
    {
        public static void CreateTable(string connectionString)
        {
            using SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string createTablesCommand = @"
                CREATE TABLE IF NOT EXISTS Books (
                    Id INTEGER PRIMARY KEY,
                    CreationTime DATETIME NOT NULL,
                    Title TEXT NOT NULL,
                    Author TEXT NOT NULL,
                    PublishDate DATETIME NOT NULL
                );
                CREATE TABLE IF NOT EXISTS Persons (
                    Id INTEGER PRIMARY KEY,
                    CreationTime DATETIME NOT NULL,
                    FirstName TEXT NOT NULL,
                    LastName TEXT NOT NULL,
                    Age INTEGER NOT NULL
                );
                CREATE TABLE IF NOT EXISTS BorrowedBooks (
                    Id INTEGER PRIMARY KEY,
                    CreationTime DATETIME NOT NULL,
                    BookId INTEGER NOT NULL,
                    PersonId INTEGER NOT NULL,
                    FOREIGN KEY (BookId) REFERENCES Books(Id),
                    FOREIGN KEY (PersonId) REFERENCES Persons(Id)
                );
            ";
            using SQLiteCommand command = new(createTablesCommand, connection);
            command.ExecuteNonQuery();
            Console.WriteLine("Tabele zostały utworzone!");
        }
    }
}

// SQLite Repository Implementation for Books
public class BookRepository : IBookRepository
{
    private readonly string _connectionString;

    public BookRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Create(Book entity)
    {
        using var connection = new SQLiteConnection(_connectionString);
        connection.Open();
        string query = @"INSERT INTO Books (CreationTime, Title, Author, PublishDate)
                         VALUES (@CreationTime, @Title, @Author, @PublishDate)";
        using var command = new SQLiteCommand(query, connection);
        command.Parameters.AddWithValue("@CreationTime", entity.CreationTime);
        command.Parameters.AddWithValue("@Title", entity.Title);
        command.Parameters.AddWithValue("@Author", entity.Author);
        command.Parameters.AddWithValue("@PublishDate", entity.PublishDate);
        command.ExecuteNonQuery();
    }

    public Book Get(long id)
    {
        using var connection = new SQLiteConnection(_connectionString);
        connection.Open();
        string query = "SELECT * FROM Books WHERE Id = @Id";
        using var command = new SQLiteCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Book
            {
                Id = Convert.ToInt32(reader["Id"]),
                CreationTime = Convert.ToDateTime(reader["CreationTime"]),
                Title = reader["Title"].ToString(),
                Author = reader["Author"].ToString(),
                PublishDate = Convert.ToDateTime(reader["PublishDate"])
            };
        }
        return null;
    }

    public void Delete(long id)
    {
        using var connection = new SQLiteConnection(_connectionString);
        connection.Open();
        string query = "DELETE FROM Books WHERE Id = @Id";
        using var command = new SQLiteCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
    }

    public List<Book> GetBooksByPublishYear(int year)
    {
        var books = new List<Book>();
        using var connection = new SQLiteConnection(_connectionString);
        connection.Open();
        string query = @"SELECT * FROM Books WHERE strftime('%Y', PublishDate) = @Year";
        using var command = new SQLiteCommand(query, connection);
        command.Parameters.AddWithValue("@Year", year.ToString());
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            books.Add(new Book
            {
                Id = Convert.ToInt32(reader["Id"]),
                CreationTime = Convert.ToDateTime(reader["CreationTime"]),
                Title = reader["Title"].ToString(),
                Author = reader["Author"].ToString(),
                PublishDate = Convert.ToDateTime(reader["PublishDate"])
            });
        }
        return books;
    }

    public void Update(Book entity)
    {
        using var connection = new SQLiteConnection(_connectionString);
        connection.Open();
        string query = @"UPDATE Books
                         SET CreationTime = @CreationTime,
                             Title = @Title,
                             Author = @Author,
                             PublishDate = @PublishDate
                         WHERE Id = @Id";
        using var command = new SQLiteCommand(query, connection);
        command.Parameters.AddWithValue("@Id", entity.Id);
        command.Parameters.AddWithValue("@CreationTime", entity.CreationTime);
        command.Parameters.AddWithValue("@Title", entity.Title);
        command.Parameters.AddWithValue("@Author", entity.Author);
        command.Parameters.AddWithValue("@PublishDate", entity.PublishDate);
        command.ExecuteNonQuery();
    }

    public List<Book> GetBorrowedBook(int personId)
    {
        var books = new List<Book>();
        using var connection = new SQLiteConnection(_connectionString);
        connection.Open();
        string query = @"SELECT b.* FROM BorrowedBooks bb
                         JOIN Books b ON bb.BookId = b.Id
                         WHERE bb.PersonId = @PersonId";
        using var command = new SQLiteCommand(query, connection);
        command.Parameters.AddWithValue("@PersonId", personId);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            books.Add(new Book
            {
                Id = Convert.ToInt32(reader["Id"]),
                CreationTime = Convert.ToDateTime(reader["CreationTime"]),
                Title = reader["Title"].ToString(),
                Author = reader["Author"].ToString(),
                PublishDate = Convert.ToDateTime(reader["PublishDate"])
            });
        }
        return books;
    }
}

// DbContext for Entity Framework
public class LibraryDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }

    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    { }
}

// Program.cs - SQLite Connection String & EF Registration
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Program.cs - Database Table Creation Call
DbCreator.CreateTable(builder.Configuration.GetConnectionString("DefaultConnection"));

// Migrations
// dotnet ef migrations add InitialCreate
// dotnet ef database update
