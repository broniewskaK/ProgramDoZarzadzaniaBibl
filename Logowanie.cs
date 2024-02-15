using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace Biblioteka
{
    public class Logowanie
    {
        private readonly string _sciezkaDoPliku;

        public Logowanie()
        {
            _sciezkaDoPliku = "pracownicy.csv"; // Przykładowa nazwa pliku z danymi logowania
        }

        public bool SprawdzLogowanie(string login, string haslo)
        {
            if (!File.Exists(_sciezkaDoPliku)) return false;

            var linie = File.ReadAllLines(_sciezkaDoPliku);

            foreach (var linia in linie)
            {
                var dane = linia.Split(',');
                if (dane.Length >= 2 && dane[0].Trim().Equals(login) && dane[1].Trim().Equals(haslo))
                {
                    return true; // Logowanie udane
                }
            }

            return false; // Logowanie nieudane
        }

        public static string WczytajHaslo()
        {
            string haslo = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    haslo += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && haslo.Length > 0)
                    {
                        haslo = haslo.Substring(0, (haslo.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);

            return haslo;
        }
    }
}

