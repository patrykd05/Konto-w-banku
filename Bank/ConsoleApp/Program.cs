using System;
using Bank;
using KontoLimitLibrary;

namespace KontoLimitSimulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Symulacja konta z limitem debetowym ===");

            Konto mojeKonto = new Konto("Jan Kowalski", 100);
            Console.WriteLine($"Utworzono konto: {mojeKonto.Nazwa}, bilans: {mojeKonto.Bilans} PLN");

            KontoLimit kontoLimit = new KontoLimit(mojeKonto, 50);
            Console.WriteLine($"Konto limitowe z limitem: {kontoLimit.Limit} PLN, bilans startowy: {kontoLimit.Bilans} PLN");

            Console.WriteLine("\nWpłata 70 PLN...");
            kontoLimit.Wplata(70);
            Console.WriteLine($"Bilans po wpłacie: {kontoLimit.Bilans} PLN");
            Console.WriteLine("\nWypłata 50 PLN...");
            kontoLimit.Wyplata(50);
            Console.WriteLine($"Bilans po wypłacie: {kontoLimit.Bilans} PLN, zablokowane: {kontoLimit.Zablokowane}");

            Console.WriteLine("\nWypłata 150 PLN (użycie debetu)...");
            try
            {
                kontoLimit.Wyplata(150);
                Console.WriteLine($"Bilans po wypłacie: {kontoLimit.Bilans} PLN, zablokowane: {kontoLimit.Zablokowane}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }

            Console.WriteLine("\nPróba wypłaty 50 PLN (ponad limit)...");
            try
            {
                kontoLimit.Wyplata(50);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }

            Console.WriteLine("\nWpłata 100 PLN (spłata debetu)...");
            kontoLimit.Wplata(100);
            Console.WriteLine($"Bilans po wpłacie: {kontoLimit.Bilans} PLN, zablokowane: {kontoLimit.Zablokowane}");


            Console.WriteLine("\nZmiana limitu debetowego na 200 PLN...");
            kontoLimit.Limit = 200;
            Console.WriteLine($"Nowy limit debetowy: {kontoLimit.Limit} PLN");

            Console.WriteLine("\n=== Symulacja zakończona ===");
            Console.ReadLine();
        }
    }
}