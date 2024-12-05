using System;
using System.Collections.Generic;

public abstract class Shape
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }

    public abstract void Draw();

    public class Rectangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Rysowanie prostokąta.");
        }
    }

    public class Triangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Rysowanie trójkąta.");
        }
    }

    public class Circle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("Rysowanie koła.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Shape> shapes = new List<Shape>
        {
            new Rectangle(),
            new Triangle(),
            new Circle()
        };

            foreach (var shape in shapes)
            {
                shape.Draw();
            }
        }
    }
}

public abstract class Osoba
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public string Pesel { get; set; }

    public void UstawImie(string imie) => Imie = imie;
    public void UstawNazwisko(string nazwisko) => Nazwisko = nazwisko;
    public void UstawPesel(string pesel) => Pesel = pesel;

    public int PobierzWiek()
    {
        if (Pesel.Length != 11) throw new Exception("Nieprawidłowy PESEL.");
        int rok = int.Parse(Pesel.Substring(0, 2));
        int miesiac = int.Parse(Pesel.Substring(2, 2));

        if (miesiac > 20)
        {
            rok += 2000;
            miesiac -= 20;
        }
        else
        {
            rok += 1900;
        }

        int obecnyRok = DateTime.Now.Year;
        int wiek = obecnyRok - rok;
        return wiek;
    }

    public string PobierzPlec()
    {
        int cyfraPlec = int.Parse(Pesel.Substring(9, 1));
        return cyfraPlec % 2 == 0 ? "Kobieta" : "Mężczyzna";
    }

    public abstract string PobierzInformacjeOEdukacji();

    public string PobierzPelneImieNazwisko() => $"{Imie} {Nazwisko}";

    public abstract bool CzyMozeSamWrocicDoDomu();
}

public class Uczen : Osoba
{
    public string Szkola { get; set; }
    public bool MozeSamWrocicDoDomu { get; set; }

    public void UstawSzkole(string szkola) => Szkola = szkola;
    public void ZmienSzkole(string szkola) => Szkola = szkola;
    public void UstawCzyMozeSamWrocic(bool pozwolenie) => MozeSamWrocicDoDomu = pozwolenie;

    public string Informacja()
    {
        if (PobierzWiek() >= 12 || MozeSamWrocicDoDomu)
        {
            return $"{PobierzPelneImieNazwisko()} może wracać samodzielnie.";
        }
        return $"{PobierzPelneImieNazwisko()} nie może wracać samodzielnie.";
    }

    public override string PobierzInformacjeOEdukacji()
    {
        return $"Uczeń w szkole {Szkola}.";
    }

    public override bool CzyMozeSamWrocicDoDomu()
    {
        return PobierzWiek() >= 12 || MozeSamWrocicDoDomu;
    }
}

public class Nauczyciel : Osoba
{
    public string TytulNaukowy { get; set; }
    public List<Uczen> UczniowieKlasy { get; } = new List<Uczen>();

    public void DodajUcznia(Uczen uczen) => UczniowieKlasy.Add(uczen);

    public List<string> KtorzyUczniowieMogaWrocicSamodzielnie()
    {
        return UczniowieKlasy
            .Where(uczen => uczen.CzyMozeSamWrocicDoDomu())
            .Select(uczen => $"{uczen.PobierzPelneImieNazwisko()} - {uczen.PobierzPlec()}")
            .ToList();
    }

    public void PodsumowanieKlasy(DateTime data)
    {
        Console.WriteLine($"Dnia: {data:dd.MM.yyyy}");
        Console.WriteLine($"Nauczyciel: {TytulNaukowy} {PobierzPelneImieNazwisko()}");
        Console.WriteLine("Lista studentów:");

        int lp = 1;
        foreach (var uczen in UczniowieKlasy)
        {
            string verdict = uczen.CzyMozeSamWrocicDoDomu() ? "TAK" : "NIE";
            string reasoning = uczen.PobierzWiek() < 12 && !uczen.MozeSamWrocicDoDomu
                ? "Brak zgody opiekuna i wiek poniżej 12 lat."
                : "Spełnione wymagania.";

            Console.WriteLine($"{lp}. {uczen.PobierzPelneImieNazwisko()} - {uczen.PobierzPlec()} - {verdict} ({reasoning})");
            lp++;
        }
    }

    public override string PobierzInformacjeOEdukacji()
    {
        return $"Nauczyciel z tytułem naukowym {TytulNaukowy}.";
    }

    public override bool CzyMozeSamWrocicDoDomu()
    {
        return true; // Nauczyciele zawsze mogą wracać sami
    }
}

class Program
{
    static void Main(string[] args)
    {
        Uczen uczen1 = new Uczen { Imie = "Jan", Nazwisko = "Kowalski", Pesel = "12120112345", Szkola = "SP 1", MozeSamWrocicDoDomu = true };
        Uczen uczen2 = new Uczen { Imie = "Anna", Nazwisko = "Nowak", Pesel = "08123154321", Szkola = "SP 1", MozeSamWrocicDoDomu = false };
        Uczen uczen3 = new Uczen { Imie = "Piotr", Nazwisko = "Wiśniewski", Pesel = "14020112345", Szkola = "SP 1", MozeSamWrocicDoDomu = true };

        Nauczyciel nauczyciel = new Nauczyciel
        {
            Imie = "Maria",
            Nazwisko = "Wiśniewska",
            Pesel = "82010176543",
            TytulNaukowy = "Mgr"
        };

        nauczyciel.DodajUcznia(uczen1);
        nauczyciel.DodajUcznia(uczen2);
        nauczyciel.DodajUcznia(uczen3);

        nauczyciel.PodsumowanieKlasy(DateTime.Now);
    }
}