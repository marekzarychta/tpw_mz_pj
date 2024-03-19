using System;

namespace KalkulatorApp
{
    public class Kalkulator
    {
        public int Dodaj(int a, int b)
        {
            return a + b;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var kalkulator = new Kalkulator();

            Console.WriteLine("Kalkulator A + B");
            Console.WriteLine("Podaj pierwszą liczbę:");
            int a;
            while (!int.TryParse(Console.ReadLine(), out a))
            {
                Console.WriteLine("To nie jest liczba. Wprowadź liczbę ponownie:");
            }

            Console.WriteLine("Podaj drugą liczbę:");
            int b;
            while (!int.TryParse(Console.ReadLine(), out b))
            {
                Console.WriteLine("To nie jest liczba. Wprowadź liczbę ponownie:");
            }

            int wynik = kalkulator.Dodaj(a, b);
            Console.WriteLine($"Wynik dodawania: {a} + {b} = {wynik}");
            Console.ReadKey();
        }
    }
}
