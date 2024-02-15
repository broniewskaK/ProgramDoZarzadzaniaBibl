using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Biblioteka
{
    class Program
    {
        static readonly CzytelnikPlik czytelnikPlik = new CzytelnikPlik();
        static readonly KsiazkaPlik ksiazkaPlik = new KsiazkaPlik();
        static readonly WypozyczenieZapis wypozyczenieZapis = new WypozyczenieZapis();

        static void Main(string[] args)
        {
            if (SprawdzLogowanie())
            {
                PokazMenuGlowne();
            }
            else
            {
                Console.WriteLine("\nNieudane logowanie. Program zostanie zamknięty.");
            }
        }

        static bool SprawdzLogowanie()
        {
            Logowanie logowanie = new Logowanie();
            Console.WriteLine("Logowanie do systemu Biblioteka");
            Console.Write("Login: ");
            string login = Console.ReadLine() ?? "";
            Console.Write("Hasło: ");
            string haslo = Logowanie.WczytajHaslo();

            return logowanie.SprawdzLogowanie(login, haslo);
        }

        static void PokazMenuGlowne()
        {

            Console.WriteLine("\nWitaj w systemie Biblioteka!");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Zarządzaj czytelnikami");
                Console.WriteLine("2. Zarządzaj książkami");
                Console.WriteLine("3. Zarządzaj wypożyczeniami");
                Console.WriteLine("X. Zakończenie programu");

                var wybor = Console.ReadLine()?.ToUpper();

                switch (wybor)
                {
                    case "1":
                        ZarzadzajCzytelnikami();
                        break;
                    case "2":
                        ZarzadzanieKsiazkami();
                        break;
                    case "3":
                        ZarzadzanieWypozyczeniami(wypozyczenieZapis);
                        break;
                    case "X":
                        return;
                    default:
                        Console.WriteLine("Niepoprawny wybór. Spróbuj ponownie.");
                        break;

                }
            }
        }

        static void ZarzadzajCzytelnikami()
        {
            Console.Clear();
            var czytelnikPlik = new CzytelnikPlik(); // Tworzę obiekt do obsługi operacji na czytelnikach

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Zarządzanie czytelnikami:");
                Console.WriteLine("1. Dodaj czytelnika");
                Console.WriteLine("2. Wyświetl czytelników");
                Console.WriteLine("3. Edytuj dane czytelnika");
                Console.WriteLine("4. Usuń dane czytelnika");
                Console.WriteLine("X. Wróć do menu głównego");

                var wybor = Console.ReadLine()?.ToUpper();

                switch (wybor)
                {
                    case "1":
                        DodajCzytelnika(czytelnikPlik); //łacze z klasą CzytelnikPlik
                        break;

                    case "2":
                        WyswietlCzytelnikow(czytelnikPlik);
                        break;

                    case "3":
                        AktualizujCzytelnika(czytelnikPlik);
                        break;

                    case "4":
                        UsunCzytelnika(czytelnikPlik);
                        break;

                    case "X":
                        return;

                    default:
                        Console.WriteLine("Niepoprawny wybór. Spróbuj ponownie.");
                        break;
                }
            }
        }



        static void ZarzadzanieKsiazkami()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Zarządzanie książkami:");
                Console.WriteLine("1. Dodaj książkę");
                Console.WriteLine("2. Edytuj książkę");
                Console.WriteLine("3. Usuń książkę");
                Console.WriteLine("4. Wyświetl książki");
                Console.WriteLine("X. Wróć do menu głównego");

                var opcja = Console.ReadLine()?.Trim().ToUpper();

                switch (opcja)
                {
                    case "1":
                        DodajKsiazke(ksiazkaPlik); //połączenie z CzytelnikPlik
                        break;
                    case "2":
                        EdytujKsiazke(ksiazkaPlik);
                        break;
                    case "3":
                        UsunKsiazke(ksiazkaPlik);
                        break;
                    case "4":
                        WyswietlKsiazki(ksiazkaPlik);
                        break;
                    case "X":
                        return;
                    default:
                        Console.WriteLine("Niepoprawna opcja. Wybierz ponownie.");
                        break;
                }
            }
        }

        static void DodajKsiazke(KsiazkaPlik ksiazkaPlik)
        {
            Console.Clear();
            Console.WriteLine("Dodawanie książki:");
            KsiazkaDane nowaKsiazka = new KsiazkaDane();
            nowaKsiazka.ZbierzTytul();
            if (nowaKsiazka.Tytul == null) return; // Anulacja dodania książki w przypadku pustego tytułu
            nowaKsiazka.ZbierzAutora();
            if (nowaKsiazka.Autor == null) return; // anulacja dodania książki w przypadku pustego autora
            ksiazkaPlik.DodajKsiazke(nowaKsiazka.Tytul, nowaKsiazka.Autor);
            Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }

        static void EdytujKsiazke(KsiazkaPlik ksiazkaPlik)
        {
            Console.Clear();
            Console.WriteLine("Edytowanie książki:");
            Console.WriteLine("Podaj ID książki do edycji:");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                KsiazkaDane nowaKsiazka = new KsiazkaDane();
                nowaKsiazka.ZbierzTytul();
                if (nowaKsiazka.Tytul == null) return; // Anulacja edycji książki w przypadku pustego tytułu
                nowaKsiazka.ZbierzAutora();
                if (nowaKsiazka.Autor == null) return; // Anulacja edycji książki w przypadku pustego autora
                ksiazkaPlik.AktualizujKsiazke(id, nowaKsiazka.Tytul, nowaKsiazka.Autor);
                Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Niepoprawne ID książki.");
            }
        }

        static void WyswietlKsiazki(KsiazkaPlik ksiazkaPlik)
        {
            Console.Clear();
            var ksiazki = ksiazkaPlik.WyszukajWszystkieKsiazki();
            Console.WriteLine("Lista wszystkich książek:");
            foreach (var ksiazka in ksiazki)
            {
                Console.WriteLine($"{ksiazka.Id}: {ksiazka.Tytul} - {ksiazka.Autor} (Dostępna: {(ksiazka.Dostepna ? "Tak" : "Nie")})");
            }
        }

        static void UsunKsiazke(KsiazkaPlik ksiazkaPlik)
        {
            Console.Clear();
            Console.WriteLine("Usuwanie książki:");
            Console.WriteLine("Podaj ID książki do usunięcia:");
            int id;
            if (int.TryParse(Console.ReadLine(), out id))
            {
                ksiazkaPlik.UsunKsiazke(id);
                Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Niepoprawne ID książki.");
            }
        }



        static void ZarzadzanieWypozyczeniami(WypozyczenieZapis wypozyczenieZapis)
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Zarządzanie wypożyczeniami:");
                Console.WriteLine("1. Wypożycz książkę");
                Console.WriteLine("2. Zwróć książkę");
                Console.WriteLine("3. Wyświetl liczbę wypożyczeń");
                Console.WriteLine("4. Wyświetl kary za przetrzymanie książki");
                Console.WriteLine("X. Wróć do menu głównego");

                var opcja = Console.ReadLine()?.Trim().ToUpper();

                switch (opcja)
                {
                    case "1":
                        WypozyczKsiazke(wypozyczenieZapis);
                        break;
                    case "2":
                        ZwrocKsiazke(wypozyczenieZapis);
                        break;
                    case "3":
                        
                        SprawdzAktywneWypozyczenia(wypozyczenieZapis, ksiazkaPlik);
                        break;

                    case "4":
                        WyswietlKary(wypozyczenieZapis);
                        break;
                    case "X":
                        return;
                    default:
                        Console.WriteLine("Niepoprawna opcja. Wybierz ponownie.");
                        break;
                }
            }
        }

        static void WypozyczKsiazke(WypozyczenieZapis wypozyczenieZapis)
        {
            var wypozyczenieDane = new WypozyczenieDane();
            wypozyczenieDane.ZbierzKsiazkaId();
            wypozyczenieDane.ZbierzCzytelnikId();
            wypozyczenieDane.ZbierzDataWypozyczenia();
            wypozyczenieZapis.WypozyczKsiazke(wypozyczenieDane.KsiazkaId, wypozyczenieDane.CzytelnikId, wypozyczenieDane.DataWypozyczenia ?? DateTime.Today);
        }

        static void ZwrocKsiazke(WypozyczenieZapis wypozyczenieZapis)
        {
            var wypozyczenieDane = new WypozyczenieDane();
            wypozyczenieDane.ZbierzKsiazkaId();
            wypozyczenieDane.ZbierzCzytelnikId();
            wypozyczenieZapis.ZwrocKsiazke(wypozyczenieDane.KsiazkaId, wypozyczenieDane.CzytelnikId);
        }

        static void SprawdzAktywneWypozyczenia(WypozyczenieZapis wypozyczenieZapis, KsiazkaPlik ksiazkaPlik)
        {
            Console.Clear();
            Console.WriteLine("Podaj ID czytelnika, dla którego chcesz sprawdzić liczbę aktywnych wypożyczeń:");
            if (int.TryParse(Console.ReadLine(), out int czytelnikId))
            {
                var aktywneWypozyczenia = wypozyczenieZapis.SprawdzAktywneWypozyczenia(czytelnikId);
                Console.WriteLine($"Liczba aktywnych wypożyczeń dla czytelnika o ID {czytelnikId}: {aktywneWypozyczenia.Count}");

                foreach (var wypozyczenie in aktywneWypozyczenia)
                {
                    var ksiazka = ksiazkaPlik.PobierzTytulIAutoraKsiazki(int.Parse(wypozyczenie.KsiazkaId));
                    if (ksiazka.HasValue) // Sprawdzam  czy  nie jest null
                    {
                        Console.WriteLine($"Tytuł wypożyczonej książki: {ksiazka.Value.Tytul}"); // value dostep
                    }
                    else
                    {
                        Console.WriteLine("Nie znaleziono książki o podanym ID.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Niepoprawne ID czytelnika.");
            }
        }




        static void WyswietlKary(WypozyczenieZapis wypozyczenieZapis)
        {
            Console.Clear();
            Console.WriteLine("Podaj ID czytelnika, dla którego chcesz wyświetlić kary:");
            if (int.TryParse(Console.ReadLine(), out int czytelnikId))
            {
                var aktywneWypozyczenia = wypozyczenieZapis.SprawdzAktywneWypozyczenia(czytelnikId);
                decimal kara = ObliczKare(aktywneWypozyczenia);
                Console.WriteLine($"Kara za przetrzymanie książek przez czytelnika o ID {czytelnikId} wynosi : {kara:C}");
            }
            else
            {
                Console.WriteLine("Niepoprawne ID czytelnika.");
            }
        }

        static decimal ObliczKare(List<WypozyczenieLinia> aktywneWypozyczenia)
        {
            decimal kara = 0;
            foreach (var wypozyczenie in aktywneWypozyczenia)
            {
                if (wypozyczenie.DataZwrotu == null)
                {
                    // Jeśli data zwrotu jest null, oznacza to, że książka nie została jeszcze zwrócona.
                    
                    TimeSpan roznica = DateTime.Today - wypozyczenie.DataWypozyczenia;//licze roznice miedzy dzisiejsza data a data wypozyczenia
                    int dniOpoznienia = (int)roznica.TotalDays;
                    if (dniOpoznienia > 30)
                    {
                        // kara za dzien 10 gr powyzej 30dni
                        kara += dniOpoznienia * 0.10m;
                    }
                }
            }
            return kara;
        }


        static void DodajCzytelnika(CzytelnikPlik czytelnikPlik)
        {
            Console.Clear();
            Console.WriteLine("Podaj imię czytelnika:");
            string imie = Console.ReadLine() ?? "";
            Console.WriteLine("Podaj nazwisko czytelnika:");
            string nazwisko = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(imie) && !string.IsNullOrWhiteSpace(nazwisko))
            {
                
                czytelnikPlik.DodajCzytelnika(imie, nazwisko); // id dodaje sie automatycznie w CzytelnikPlik
            }
            else
            {
                Console.WriteLine("Nie dodano czytelnika. Sprawdź poprawność wprowadzonych danych.");
            }

            Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }



        static void AktualizujCzytelnika(CzytelnikPlik czytelnikPlik)
        {
            Console.Clear();

            Console.WriteLine("Podaj ID czytelnika do aktualizacji:");
            int id = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine("Podaj nowe imię czytelnika:");
            string imie = Console.ReadLine() ?? "";
            Console.WriteLine("Podaj nowe nazwisko czytelnika:");
            string nazwisko = Console.ReadLine() ?? "";

            if (id > 0 && !string.IsNullOrWhiteSpace(imie) && !string.IsNullOrWhiteSpace(nazwisko))
            {
                czytelnikPlik.AktualizujCzytelnika(id, imie, nazwisko);
            }
            else
            {
                Console.WriteLine("Nie zaktualizowano danych czytelnika. Sprawdź poprawność wprowadzonych danych.");
            }
        }







        static void UsunCzytelnika(CzytelnikPlik czytelnikPlik)
        {
            Console.Clear();
            Console.WriteLine("Podaj ID czytelnika do usunięcia:");
            if (int.TryParse(Console.ReadLine(), out int id) && id > 0)
            {
                bool wynikUsuwania = czytelnikPlik.UsunCzytelnika(id);
                if (wynikUsuwania)
                {
                    Console.WriteLine("Czytelnik został usunięty.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono czytelnika o podanym ID.");
                }
            }
            else
            {
                Console.WriteLine("Niepoprawne ID czytelnika.");
            }

            Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }









        static void WyswietlCzytelnikow(CzytelnikPlik czytelnikPlik)
        {
            var czytelnicy = czytelnikPlik.WyszukajWszystkichCzytelnikow();

            if (czytelnicy.Any())
            {
                Console.WriteLine("Lista wszystkich czytelników:");
                foreach (var czytelnik in czytelnicy)
                {
                    // Splitujemy linie czytelnika na poszczególne elementy
                    var dane = czytelnik.linia.Split(',');
                    if (dane.Length >= 3) // Sprawdzamy czy mamy co najmniej imię i nazwisko
                    {
                        var idCzytelnika = dane[0].Trim();
                        var imie = dane[1].Trim();
                        var nazwisko = dane[2].Trim();
                        Console.WriteLine($"{idCzytelnika}: {imie} {nazwisko}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Brak danych o czytelnikach.");
            }

            Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }


    }
}
