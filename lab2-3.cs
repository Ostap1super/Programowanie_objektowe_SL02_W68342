using System;

// Zadanie 1
class Osoba
{
    
    private string imie;
    private string nazwisko;
    private int wiek;
    private string pesel;

    
    public Osoba(string imie, string nazwisko, int wiek, string pesel)
    {
        this.imie = imie;
        this.nazwisko = nazwisko;
        this.wiek = wiek >= 0 ? wiek : 0;  
        this.pesel = pesel;
    }


    public string Imie
    {
        get { return imie; }
        set { imie = value; }
    }

    public string Nazwisko
    {
        get { return nazwisko; }
        set { nazwisko = value; }
    }

    public int Wiek
    {
        get { return wiek; }
        set { wiek = value >= 0 ? value : 0; } 
    }

 
    public string Pesel
    {
        get { return pesel; }
    }


    public string PrzedstawSie()
    {
        return $"Nazywam się {imie} {nazwisko} i mam {wiek} lat.";
    }
}

class Program
{
    static void Main()
    {
     
        Osoba osoba = new Osoba("Jan", "Kowalski", 30, "12345678901");
        Console.WriteLine(osoba.PrzedstawSie());
        Console.WriteLine($"Pesel: {osoba.Pesel}");

     
        osoba.Wiek = -5; 
        Console.WriteLine(osoba.PrzedstawSie());
    }

    // Zadanie 2
    class Licz
    {
    
        private int wartosc;


        public Licz(int wartosc)
        {
            this.wartosc = wartosc;
        }

     
        public void Dodaj(int liczba)
        {
            wartosc += liczba;
        }

     
        public void Odejmij(int liczba)
        {
            wartosc -= liczba;
        }

      
        public void WypiszStan()
        {
            Console.WriteLine($"Aktualna wartość: {wartosc}");
        }
    }

    class Program1
    {
        static void Main()
        {
         
            Licz licznik = new Licz(10);
            licznik.WypiszStan();

            licznik.Dodaj(5);
            licznik.WypiszStan();

            licznik.Odejmij(3);
            licznik.WypiszStan();
        }
    }

    // Zadanie 3
    class Sumator
    {
       
        private int[] liczby;

      
        public Sumator(int[] liczby)
        {
            this.liczby = liczby;
        }

      
        public int Suma()
        {
            int suma = 0;
            foreach (var liczba in liczby)
            {
                suma += liczba;
            }
            return suma;
        }

    
        public int SumaPodziel3()
        {
            int suma = 0;
            foreach (var liczba in liczby)
            {
                if (liczba % 3 == 0)
                {
                    suma += liczba;
                }
            }
            return suma;
        }

       
        public int IleElementów()
        {
            return liczby.Length;
        }

       
        public void WypiszElementy()
        {
            foreach (var liczba in liczby)
            {
                Console.WriteLine(liczba);
            }
        }

      
        public void WypiszElementy(int lowIndex, int highIndex)
        {
            for (int i = lowIndex; i <= highIndex; i++)
            {
                if (i >= 0 && i < liczby.Length)
                {
                    Console.WriteLine(liczby[i]);
                }
            }
        }
    }

    class Program2
    {
        static void Main()
        {
            int[] liczby = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Sumator sumator = new Sumator(liczby);

            Console.WriteLine($"Suma liczb: {sumator.Suma()}");
            Console.WriteLine($"Suma liczb podzielnych przez 3: {sumator.SumaPodziel3()}");
            Console.WriteLine($"Liczba elementów: {sumator.IleElementów()}");

            Console.WriteLine("Wszystkie elementy:");
            sumator.WypiszElementy();

            Console.WriteLine("Elementy od indeksu 3 do 7:");
            sumator.WypiszElementy(3, 7);
        }
    }
}
