using System;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
           

            Console.WriteLine("Podaj liczbę:");
            int liczba = int.Parse(Console.ReadLine());

            if (liczba % 2 == 0)
            {
                Console.WriteLine("Liczba jest parzysta.");
            }
            else
            {
                Console.WriteLine("Liczba jest nieparzysta.");
            }

            Console.WriteLine("Podaj liczbę N:");
            int N = int.Parse(Console.ReadLine());

            Console.WriteLine("Parzyste liczby od 1 do N:");
            for (int i = 2; i <= N; i += 2) 
            {
                Console.WriteLine(i);
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Sprawdź, czy liczba jest parzysta czy nieparzysta");
                Console.WriteLine("2. Wypisz wszystkie parzyste liczby od 1 do N");
                Console.WriteLine("3. Oblicz silnię z liczby");
                Console.WriteLine("4. Gra – Zgadnij liczbę");
                Console.WriteLine("5. Wyjście");
                Console.Write("Wybierz opcję: ");
                int wybor = int.Parse(Console.ReadLine());

                switch (wybor)
                {
                    case 1:
                        SprawdzCzyParzysta();
                        break;
                    case 2:
                        WypiszParzysteLiczby();
                        break;
                    case 3:
                        ObliczSilnie();
                        break;
                    case 4:
                        GraZgadnijLiczbe();
                        break;
                    case 5:
                        Console.WriteLine("Koniec programu.");
                        return;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                        break;
                }

                Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
            }
        }

        static void SprawdzCzyParzysta()
        {
            Console.WriteLine("Podaj liczbę:");
            int liczba = int.Parse(Console.ReadLine());
            if (liczba % 2 == 0)
            {
                Console.WriteLine("Liczba jest parzysta.");
            }
            else
            {
                Console.WriteLine("Liczba jest nieparzysta.");
            }
        }

        static void WypiszParzysteLiczby()
        {
            Console.WriteLine("Podaj liczbę N:");
            int N = int.Parse(Console.ReadLine());
            Console.WriteLine("Parzyste liczby od 1 do N:");
            for (int i = 2; i <= N; i += 2)
            {
                Console.WriteLine(i);
            }
        }

        static void ObliczSilnie()
        {
            Console.WriteLine("Podaj liczbę do obliczenia silni:");
            int liczba = int.Parse(Console.ReadLine());
            Console.WriteLine($"Silnia z {liczba} wynosi {Silnia(liczba)}");
        }

        static int Silnia(int n)
        {
            if (n == 0) return 1; // silnia 0 = 1
            return n * Silnia(n - 1); // rekurencyjne wywołanie
        }

        static void GraZgadnijLiczbe()
        {
            Random random = new Random();
            int liczba = random.Next(1, 101); // losowanie liczby od 1 do 100
            int proby = 0;
            int zgadujacaLiczba;

            Console.WriteLine("Zgadnij liczbę (od 1 do 100):");

            do
            {
                zgadujacaLiczba = int.Parse(Console.ReadLine());
                proby++;

                if (zgadujacaLiczba < liczba)
                {
                    Console.WriteLine("Za mało! Spróbuj jeszcze raz.");
                }
                else if (zgadujacaLiczba > liczba)
                {
                    Console.WriteLine("Za dużo! Spróbuj jeszcze raz.");
                }

            } while (zgadujacaLiczba != liczba);

            Console.WriteLine($"Gratulacje! Zgadłeś liczbę za {proby} prób.");
        }
    }
}
class Program
{
    static void Main()
    {
        Console.WriteLine("Podaj liczbę do obliczenia silni:");
        int liczba = int.Parse(Console.ReadLine());
        Console.WriteLine($"Silnia z {liczba} wynosi {Silnia(liczba)}");
    }

    static int Silnia(int n)
    {
        if (n == 0) return 1; // przypadek bazowy
        return n * Silnia(n - 1); // rekurencyjne wywołanie
        static void Main()
        {
            GraZgadnijLiczbe();
        }

        static void GraZgadnijLiczbe()
        {
            Random random = new Random();
            int liczba = random.Next(1, 101); // losowanie liczby od 1 do 100
            int proby = 0;
            int zgadujacaLiczba;

            Console.WriteLine("Zgadnij liczbę (od 1 do 100):");

            do
            {
                zgadujacaLiczba = int.Parse(Console.ReadLine());
                proby++;

                if (zgadujacaLiczba < liczba)
                {
                    Console.WriteLine("Za mało! Spróbuj jeszcze raz.");
                }
                else if (zgadujacaLiczba > liczba)
                {
                    Console.WriteLine("Za dużo! Spróbuj jeszcze raz.");
                }

            } while (zgadujacaLiczba != liczba);

            Console.WriteLine($"Gratulacje! Zgadłeś liczbę za {proby} prób.");
        }
    }
}
