using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Biblioteka
{


    public class KsiazkaDane
    {
        public int Id { get; private set; }
        public string? Tytul { get; private set; } // Zmienione na dopuszczające wartość null
        public string? Autor { get; private set; }

        public void ZbierzID()
        {
            while (true)
            {
                Console.WriteLine("Podaj ID książki:");
                if (int.TryParse(Console.ReadLine(), out int id) && id > 0)
                {
                    Id = id;
                    break;
                }
                else
                {
                    Console.WriteLine("Nie podano poprawnego ID. Czy chcesz spróbować jeszcze raz? (tak/nie)");
                    var odpowiedz = Console.ReadLine()?.Trim().ToLower();
                    if (odpowiedz != "tak")
                    {
                        Console.WriteLine("Anulowano wprowadzanie ID.");
                        Id = 0;
                        break;
                    }
                }
            }
        }


        public void ZbierzTytul()
        {
            while (true)
            {
                Console.WriteLine("Podaj tytuł książki:");
                Tytul = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(Tytul))
                {
                    break; // Wychodzi z pętli, jeśli tytuł jest prawidłowy.
                }
                else
                {
                    Console.WriteLine("Nie podano tytułu. Czy chcesz spróbować jeszcze raz? (tak/nie)");
                    var odpowiedz = Console.ReadLine()?.Trim().ToLower();
                    if (odpowiedz != "tak")
                    {
                        Console.WriteLine("Anulowano wprowadzanie tytułu.");
                        Tytul = null; // Opcjonalnie, można zdecydować o zachowaniu tej wartości null lub przyjąć inny sposób obsługi tej sytuacji.
                        break;
                    }
                }
            }
        }



        public void ZbierzAutora()
        {
            while (true)
            {
                Console.WriteLine("Podaj autora książki:");
                Autor = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(Autor))
                {
                    break; // Wychodzi z pętli, jeśli autor jest prawidłowy.
                }
                else
                {
                    Console.WriteLine("Nie podano autora. Czy chcesz spróbować jeszcze raz? (tak/nie)");
                    var odpowiedz = Console.ReadLine()?.Trim().ToLower();
                    if (odpowiedz != "tak")
                    {
                        Console.WriteLine("Anulowano wprowadzanie autora.");
                        Autor = null; // Opcjonalnie, można zdecydować o zachowaniu tej wartości null lub przyjąć inny sposób obsługi tej sytuacji.
                        break;
                    }
                }
            }
        }


    }

}
