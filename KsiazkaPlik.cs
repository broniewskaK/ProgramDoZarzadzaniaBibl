using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Biblioteka
{


    public class KsiazkaPlik
    {
        private readonly string _sciezkaDoPliku;

        public KsiazkaPlik()
        {
            _sciezkaDoPliku = "ksiazki.csv";
        }

        public void DodajKsiazke(string tytul, string autor)
        {

            try
            {

                int noweId = 1;
                if (File.Exists(_sciezkaDoPliku))
                {
                    var linie = File.ReadAllLines(_sciezkaDoPliku);
                    if (linie.Length > 1)
                    {
                        var ostatnieId = linie.Skip(1)
                                               .Select(linia => int.Parse(linia.Split(',')[0]))
                                               .Max();
                        noweId = ostatnieId + 1;
                    }
                }
                else
                {
                    File.WriteAllText(_sciezkaDoPliku, "Id,Tytul,Autor\n");
                }

                string nowyRekord = $"{noweId},{tytul},{autor}\n";
                File.AppendAllText(_sciezkaDoPliku, nowyRekord);
                Console.WriteLine("Książka została pomyślnie zapisana.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas dodawania książki: {ex.Message}");
            }
        }


        public IEnumerable<(string Id, string Tytul, string Autor, bool Dostepna)> WyszukajWszystkieKsiazki()
        {

            if (!File.Exists(_sciezkaDoPliku))
            {
                Console.WriteLine("Plik z danymi książek nie istnieje.");
                return Enumerable.Empty<(string, string, string, bool)>();
            }

            var linie = File.ReadAllLines(_sciezkaDoPliku);
            return linie.Skip(1) // Pomijamy nagłówek
                        .Select(linia =>
                        {
                            var segmenty = linia.Split(',');
                            var id = segmenty[0];
                            var tytul = segmenty[1];
                            var autor = segmenty[2];
                            var dostepna = SprawdzDostepnoscWypozyczenia(int.Parse(id));
                            return (id, tytul, autor, dostepna);
                        });
        }

        public bool SprawdzDostepnoscWypozyczenia(int ksiazkaId)
        {

            try
            {
                if (!File.Exists("wypozyczenia.csv"))
                {
                    return true; // Zakładamy, że książka jest dostępna, jeśli nie ma pliku wypożyczeń.
                }

                var linie = File.ReadAllLines("wypozyczenia.csv");

                return linie.Skip(1) // Pomijamy nagłówek
                            .Select(linia => linia.Split(','))
                            .Where(dane => dane.Length >= 4)
                            .All(dane => int.Parse(dane[1]) != ksiazkaId || !string.IsNullOrEmpty(dane[3]));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas sprawdzania dostępności wypożyczenia: {ex.Message}");
                return false; // W przypadku błędu zakładamy, że książka nie jest dostępna.
            }
        }

        public void AktualizujKsiazke(int id, string nowyTytul, string nowyAutor)
        {

            try
            {
                if (!File.Exists(_sciezkaDoPliku))
                {
                    Console.WriteLine("Plik z danymi książek nie istnieje.");
                    return;
                }

                var linie = File.ReadAllLines(_sciezkaDoPliku).ToList();
                bool czyZnaleziono = false;

                for (int i = 1; i < linie.Count; i++)
                {
                    var dane = linie[i].Split(',');
                    if (int.Parse(dane[0]) == id)
                    {
                        linie[i] = $"{id},{nowyTytul},{nowyAutor}";
                        czyZnaleziono = true;
                        break;
                    }
                }

                if (czyZnaleziono)
                {
                    File.WriteAllLines(_sciezkaDoPliku, linie);
                    Console.WriteLine("Dane książki zostały zaktualizowane.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono książki o podanym ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas aktualizowania książki: {ex.Message}");
            }
        }


        public void UsunKsiazke(int idKsiazki)
        {

            try
            {
                if (!File.Exists(_sciezkaDoPliku))
                {
                    Console.WriteLine("Plik z danymi książek nie istnieje.");
                    return;
                }

                var linie = File.ReadAllLines(_sciezkaDoPliku).ToList();
                var indeksDoUsuniecia = linie.FindIndex(linia => linia.StartsWith(idKsiazki.ToString() + ","));

                if (indeksDoUsuniecia != -1)
                {
                    linie.RemoveAt(indeksDoUsuniecia);
                    File.WriteAllLines(_sciezkaDoPliku, linie);
                    Console.WriteLine($"Książka o ID {idKsiazki} została usunięta.");
                }
                else
                {
                    Console.WriteLine($"Nie znaleziono książki o ID {idKsiazki}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas usuwania książki: {ex.Message}");
            }
        }


        public (string Tytul, string Autor)? PobierzTytulIAutoraKsiazki(string ksiazkaId)
        {
            try
            {
                if (!File.Exists(_sciezkaDoPliku))
                {
                    Console.WriteLine("Plik z danymi książek nie istnieje.");
                    return null;
                }

                var linie = File.ReadAllLines(_sciezkaDoPliku);

                foreach (var linia in linie.Skip(1))
                {
                    var dane = linia.Split(',');
                    if (dane[0] == ksiazkaId)
                    {
                        return (dane[1], dane[2]);
                    }
                }

                Console.WriteLine($"Nie znaleziono książki o Id: {ksiazkaId}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas wyszukiwania tytułu i autora książki: {ex.Message}");
                return null;
            }
        }
        public (string Tytul, string Autor)? PobierzTytulIAutoraKsiazki(int idKsiazki)
        {
            if (!File.Exists(_sciezkaDoPliku))
            {
                Console.WriteLine("Plik z danymi książek nie istnieje.");
                return null;
            }

            var linie = File.ReadAllLines(_sciezkaDoPliku);

            foreach (var linia in linie.Skip(1)) // Pomijamy nagłówek
            {
                var dane = linia.Split(',');
                if (dane.Length >= 3 && int.Parse(dane[0]) == idKsiazki)
                {
                    return (dane[1], dane[2]);
                }
            }

            return null;
        }



    }
}
