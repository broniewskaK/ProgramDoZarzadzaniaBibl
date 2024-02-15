using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Biblioteka
{
    public record CzytelnikLinia(string CzytelnikId, string linia);
    public class CzytelnikDane
    {
        public int Id { get; private set; }
        public string? Imie { get; private set; } // dopuszczam null
        public string? Nazwisko { get; private set; }

        public CzytelnikDane(int id, string imie, string nazwisko)
        {
            Id = id;
            Imie = imie;
            Nazwisko = nazwisko;
        }
        public void ZbierzImie()
        {
            Console.WriteLine("Podaj imię czytelnika:");
            string? imie = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(imie))
            {
                Console.WriteLine("Nie podano imienia. Czy chcesz spróbować jeszcze raz? (tak/nie)");
                string? odpowiedz = Console.ReadLine();
                if (odpowiedz?.Trim().ToLower() == "tak")
                {
                    ZbierzImie();
                }
                else
                {
                    Console.WriteLine("Anulowano wprowadzanie imienia.");
                    Imie = null; // tutaj sprawdzam czy imie podano jako null jak tak to nastepuje anulacja
                }
            }
            else
            {
                Imie = imie; // jak imie nie null to przypisuje imie
            }
        }


        public void ZbierzNazwisko()
        {
            Console.WriteLine("Podaj nazwisko czytelnika:");
            string? nazwisko = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nazwisko))
            {
                Console.WriteLine("Nie podano nazwiska. Czy chcesz spróbować jeszcze raz? (tak/nie)");
                string? odpowiedz = Console.ReadLine();
                if (odpowiedz?.Trim().ToLower() == "tak")
                {
                    ZbierzNazwisko();
                }
                else
                {
                    Console.WriteLine("Anulowano wprowadzanie nazwiska.");
                    Nazwisko = null; // analogicznie jak w ZbierzImie
                }
            }
            else
            {
                Nazwisko = nazwisko; 
            }
        }


        
    }
}
