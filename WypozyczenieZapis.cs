using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Biblioteka
{


    public class WypozyczenieZapis
    {
        private readonly string _sciezkaDoPliku;

        public WypozyczenieZapis()
        {
            _sciezkaDoPliku = "wypozyczenia.csv";
        }

        public void WypozyczKsiazke(int ksiazkaId, int czytelnikId, DateTime dataWypozyczenia)
        {
            try
            {
                // Tworzenie pliku, jeśli nie istnieje, z nagłówkiem
                if (!File.Exists(_sciezkaDoPliku))
                {
                    File.WriteAllText(_sciezkaDoPliku, "KsiazkaId,CzytelnikId,DataWypozyczenia,DataZwrotu\n");
                }

                var linie = File.ReadAllLines(_sciezkaDoPliku);

                // Sprawdzenie, czy książka jest już wypożyczona
                if (linie.Skip(1).Any(linia =>
                {
                    var dane = linia.Split(',');
                    return dane.Length >= 3 && int.Parse(dane[0]) == ksiazkaId && string.IsNullOrEmpty(dane[3]);
                }))
                {
                    Console.WriteLine("Książka nie może być wypożyczona, ponieważ nie została jeszcze zwrócona.");
                    return;
                }

                string nowyWpis = $"{ksiazkaId},{czytelnikId},{dataWypozyczenia:yyyy-MM-dd},\n";
                File.AppendAllText(_sciezkaDoPliku, nowyWpis);
                Console.WriteLine("Książka została pomyślnie wypożyczona.");
                Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas wypożyczania książki: {ex.Message}");
            }
        }

        public void ZwrocKsiazke(int ksiazkaId, int czytelnikId)
        {
            try
            {
                if (!File.Exists(_sciezkaDoPliku))
                {
                    Console.WriteLine("Plik z danymi wypożyczeń nie istnieje.");
                    return;
                }

                var linie = File.ReadAllLines(_sciezkaDoPliku).ToList();
                bool znalezionoWpis = false;
                DateTime dzisiaj = DateTime.Today;

                for (int i = 1; i < linie.Count; i++) // Pomijamy nagłówek
                {
                    var dane = linie[i].Split(',');
                    if (dane.Length >= 4 && int.Parse(dane[0]) == ksiazkaId && int.Parse(dane[1]) == czytelnikId && string.IsNullOrEmpty(dane[3]))
                    {
                        linie[i] = $"{dane[0]},{dane[1]},{dane[2]},{dzisiaj.ToString("yyyy-MM-dd")}"; // Uaktualnienie linii z datą zwrotu
                        znalezionoWpis = true;
                        break;
                    }
                }

                if (znalezionoWpis)
                {
                    File.WriteAllLines(_sciezkaDoPliku, linie);
                    Console.WriteLine("Książka została pomyślnie zwrócona.");
                    Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Nie znaleziono wypożyczenia o podanych danych.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas próby zwrotu książki: {ex.Message}");
            }
        }


        public List<WypozyczenieLinia> SprawdzAktywneWypozyczenia(int czytelnikId)
        {
            var aktywneWypozyczenia = new List<WypozyczenieLinia>();
            try
            {
                if (!File.Exists(_sciezkaDoPliku))
                {
                    Console.WriteLine("Plik z danymi wypożyczeń nie istnieje.");
                    return aktywneWypozyczenia;
                }

                var linie = File.ReadAllLines(_sciezkaDoPliku);

                foreach (var linia in linie.Skip(1)) // Pomijamy nagłówek
                {
                    var dane = linia.Split(',');
                    if (dane.Length >= 4 && int.Parse(dane[1]) == czytelnikId && string.IsNullOrEmpty(dane[3]))
                    {
                        var dataWypozyczenia = DateTime.Parse(dane[2]);
                        aktywneWypozyczenia.Add(new WypozyczenieLinia(dane[0], dataWypozyczenia));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas sprawdzania aktywnych wypożyczeń: {ex.Message}");
            }

            return aktywneWypozyczenia;
            Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }


    }

}
