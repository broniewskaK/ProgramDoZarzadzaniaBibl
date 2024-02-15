using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Biblioteka
{
    public class CzytelnikPlik
    {
        private readonly string _sciezkaDoPliku;
        
        public CzytelnikPlik()
        {
            _sciezkaDoPliku = "czytelnicy.csv";

        }


        public void DodajCzytelnika(string imie, string nazwisko)
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
                    File.WriteAllText(_sciezkaDoPliku, "Id,Imie,Nazwisko\n");
                }
                string nowyRekord = $"{noweId},{imie},{nazwisko}\n";

                // Dodanie nowego rekordu do pliku
                File.AppendAllText(_sciezkaDoPliku, nowyRekord);

                Console.WriteLine("Czytelnik został pomyślnie zapisany.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas dodawania czytelnika: {ex.Message}");
            }
        }



        public IEnumerable<CzytelnikLinia> WyszukajWszystkichCzytelnikow() // zwracanie wszystkich czytelnikow
        {
            if (!File.Exists(_sciezkaDoPliku))
            {
                Console.WriteLine("Plik z danymi czytelników nie istnieje.");
                return Enumerable.Empty<CzytelnikLinia>();
            }

            var wyniki = new List<CzytelnikLinia>();
            var linie = File.ReadAllLines(_sciezkaDoPliku);

            foreach (var linia in linie.Skip(1)) // Pomijam nagłówek
            {
                var dane = linia.Split(',');
                if (dane.Length >= 3) 
                {
                    var idCzytelnika = dane[0].Trim();
                    wyniki.Add(new CzytelnikLinia(idCzytelnika, linia));
                }
            }

            return wyniki;
        }




        public void AktualizujCzytelnika(int id, string noweImie, string noweNazwisko)
        {
            try
            {
                if (!File.Exists(_sciezkaDoPliku))
                {
                    Console.WriteLine("Plik z danymi czytelników nie istnieje.");
                    return;
                }

                var linie = File.ReadAllLines(_sciezkaDoPliku).ToList();
                bool czyZnaleziono = false;

                for (int i = 1; i < linie.Count; i++) // Pomijam nagłówek
                {
                    var dane = linie[i].Split(',');
                    if (int.Parse(dane[0]) == id)
                    {
                        linie[i] = $"{id},{noweImie},{noweNazwisko}";
                        czyZnaleziono = true;
                        break;
                    }
                }

                if (czyZnaleziono)
                {
                    File.WriteAllLines(_sciezkaDoPliku, linie);
                    Console.WriteLine("Dane czytelnika zostały zaktualizowane.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono czytelnika o podanym ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas aktualizowania danych czytelnika: {ex.Message}");
            }
        }


        public bool UsunCzytelnika(int idCzytelnika)
        {
            if (!File.Exists(_sciezkaDoPliku))
            {
                Console.WriteLine("Plik z danymi czytelników nie istnieje.");
                return false; 
            }

            var linie = File.ReadAllLines(_sciezkaDoPliku).ToList();
            var indeksDoUsuniecia = linie.FindIndex(linia => linia.StartsWith(idCzytelnika.ToString() + ","));

            if (indeksDoUsuniecia != -1) // Jeśli znaleziono czytelnika
            {
                linie.RemoveAt(indeksDoUsuniecia);
                File.WriteAllLines(_sciezkaDoPliku, linie);
                
                return true; // Czytelnik został usunięty
            }
            else
            {
                Console.WriteLine("Nie znaleziono czytelnika o podanym ID.");
                return false; // Nie znaleziono czytelnika o podanym ID
            }
        }


    }




}
