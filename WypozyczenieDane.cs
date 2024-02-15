using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Biblioteka
{

    public record WypozyczenieLinia(string KsiazkaId, DateTime DataWypozyczenia, DateTime? DataZwrotu = null);

    public class WypozyczenieDane
    {
        public int KsiazkaId { get; private set; }
        public int CzytelnikId { get; private set; }
        public DateTime? DataWypozyczenia { get; private set; }
        public DateTime? DataZwrotu { get; private set; }

        public void ZbierzKsiazkaId()
        {
            while (true)
            {
                Console.WriteLine("Podaj ID książki:");
                if (int.TryParse(Console.ReadLine(), out int ksiazkaId) && ksiazkaId > 0)
                {
                    KsiazkaId = ksiazkaId;
                    break;
                }
                else
                {
                    Console.WriteLine("Nie podano poprawnego ID książki. Czy chcesz spróbować jeszcze raz? (tak/nie)");
                    var odpowiedz = Console.ReadLine()?.Trim().ToLower();
                    if (odpowiedz != "tak")
                    {
                        Console.WriteLine("Anulowano wprowadzanie ID książki.");
                        KsiazkaId = 0; // Można rozważyć, czy 0 jest odpowiednią wartością w tym kontekście
                        break;
                    }
                }
            }
        }


        public void ZbierzCzytelnikId()
        {
            while (true)
            {
                Console.WriteLine("Podaj ID czytelnika:");
                if (int.TryParse(Console.ReadLine(), out int czytelnikId) && czytelnikId > 0)
                {
                    CzytelnikId = czytelnikId;
                    break;
                }
                else
                {
                    Console.WriteLine("Nie podano poprawnego ID czytelnika. Czy chcesz spróbować jeszcze raz? (tak/nie)");
                    var odpowiedz = Console.ReadLine()?.Trim().ToLower();
                    if (odpowiedz != "tak")
                    {
                        Console.WriteLine("Anulowano wprowadzanie ID czytelnika.");
                        CzytelnikId = 0; // Może być potrzebne przemyślenie, jak obsłużyć ten przypadek w logice aplikacji.
                        break;
                    }
                }
            }
        }


        public void ZbierzDataWypozyczenia()
        {
            while (true)
            {
                Console.WriteLine("Podaj datę wypożyczenia (RRRR-MM-DD):");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dataWypozyczenia))
                {
                    DataWypozyczenia = dataWypozyczenia;
                    break;
                }
                else
                {
                    Console.WriteLine("Nie podano poprawnej daty wypożyczenia. Czy chcesz spróbować jeszcze raz? (tak/nie)");
                    var odpowiedz = Console.ReadLine()?.Trim().ToLower();
                    if (odpowiedz != "tak")
                    {
                        Console.WriteLine("Anulowano wprowadzanie daty wypożyczenia.");
                        DataWypozyczenia = null;
                        break;
                    }
                }
            }


        }

        public void ZbierzDataZwrotu()
        {
            while (true)
            {
                Console.WriteLine("Podaj datę zwrotu (RRRR-MM-DD):");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime dataZwrotu))
                {
                    DataZwrotu = dataZwrotu;
                    break;
                }
                else
                {
                    Console.WriteLine("Nie podano poprawnej daty zwrotu. Czy chcesz spróbować jeszcze raz? (tak/nie)");
                    var odpowiedz = Console.ReadLine()?.Trim().ToLower();
                    if (odpowiedz != "tak")
                    {
                        Console.WriteLine("Anulowano wprowadzanie daty zwrotu.");
                        DataZwrotu = null; // Ustawienie na null wskazuje, że data zwrotu nie została poprawnie wprowadzona.
                        break;
                    }
                }
            }
        }

    }

}
