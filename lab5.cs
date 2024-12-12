using System;

public abstract class Shape
{
    public abstract double CalculateArea();
}

public class Circle : Shape
{
    public double Radius { get; set; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public override double CalculateArea()
    {
        return Math.PI * Math.Pow(Radius, 2);
    }
}

public class Square : Shape
{
    public double Side { get; set; }

    public Square(double side)
    {
        Side = side;
    }

    public override double CalculateArea()
    {
        return Math.Pow(Side, 2);
    }
}

class Program
{
	public static void Main()

    {
        Shape circle = new Circle(5);
        Shape square = new Square(4);

        Console.WriteLine("Circle area: " + circle.CalculateArea());
        Console.WriteLine("Square area: " + square.CalculateArea());
    }
}

public interface IVehicle
{
    void Start();
    void Stop();
    int MaxSpeed { get; }
}

public class Car : IVehicle
{
    public int MaxSpeed { get; private set; }

    public Car(int maxSpeed)
    {
        MaxSpeed = maxSpeed;
    }

    public void Start()
    {
        Console.WriteLine("Car started.");
    }

    public void Stop()
    {
        Console.WriteLine("Car stopped.");
    }

    public void Honk()
    {
        Console.WriteLine("Car is honking!");
    }
}

public class Bike : IVehicle
{
    public int MaxSpeed { get; private set; }

    public Bike(int maxSpeed)
    {
        MaxSpeed = maxSpeed;
    }

    public void Start()
    {
        Console.WriteLine("Bike started.");
    }

    public void Stop()
    {
        Console.WriteLine("Bike stopped.");
    }

    public void RingBell()
    {
        Console.WriteLine("Bike bell is ringing!");
    }
}

class Program1
{
    static void Main(string[] args)
    {
        IVehicle car = new Car(180);
        IVehicle bike = new Bike(25);

        car.Start();
        bike.Start();

        Console.WriteLine("Car max speed: " + car.MaxSpeed);
        Console.WriteLine("Bike max speed: " + bike.MaxSpeed);

        // Unique methods
        ((Car)car).Honk();
        ((Bike)bike).RingBell();

        car.Stop();
        bike.Stop();
    }
}

public interface IEntity<T>
{
    T Id { get; set; }
}

public interface ICreated
{
    DateTime CreatedDate { get; }
}

public class Book : IEntity<int>, ICreated
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public DateTime CreatedDate { get; private set; }

    public Book(int id, string title, string author, int year)
    {
        Id = id;
        Title = title;
        Author = author;
        Year = year;
        CreatedDate = DateTime.Now;
    }
}

public class Person : IEntity<int>, ICreated
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public List<Book> BorrowedBooks { get; set; }
    public DateTime CreatedDate { get; private set; }

    public Person(int id, string firstName, string lastName, int age)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        BorrowedBooks = new List<Book>();
        CreatedDate = DateTime.Now;
    }

    public void BorrowBook(Book book)
    {
        BorrowedBooks.Add(book);
    }

    public void ReturnBook(Book book)
    {
        BorrowedBooks.Remove(book);
    }
}

class Program2
{
    static void Main(string[] args)
    {
        Book book1 = new Book(1, "C# Programming", "John Doe", 2024);
        Book book2 = new Book(2, "Advanced C#", "Jane Smith", 2023);

        Person person = new Person(1, "Alice", "Johnson", 30);
        person.BorrowBook(book1);
        person.BorrowBook(book2);

        Console.WriteLine($"Person: {person.FirstName} {person.LastName}, Age: {person.Age}");
        Console.WriteLine("Borrowed Books:");
        foreach (var book in person.BorrowedBooks)
        {
            Console.WriteLine($"{book.Title} by {book.Author}");
        }

        Console.WriteLine("Book created on: " + book1.CreatedDate);
        Console.WriteLine("Person created on: " + person.CreatedDate);
    }
}