using System.Text;

namespace DesktopAppNicola.UI
{
    public static class Utility
    {
        public static string SzyfrujZnaki(string prompt)
        {
            bool jestWpisany = true;
            string gwiazdki = "";

            StringBuilder input = new StringBuilder(); // stringbuilder przechowuje wprowadzone znaki

            while (true) // dopoki uzytkownik nie wprowadzi 6 znakow (poprawnego pinu)
            {
                if (jestWpisany)
                    Console.WriteLine(prompt);
                jestWpisany = false;

                ConsoleKeyInfo inputKey = Console.ReadKey(true); // Wcisnij dowolny klawisz, parametr true ukrywa wprowadzane znaki

                if (inputKey.Key == ConsoleKey.Enter)
                {
                    if (input.Length == 6) // jesli dlugosc inputu wynosi 6
                        break;
                    else
                    {
                        WyswietlWiadomosc("\nProsze wprowadz 6 znakow", false);
                        input.Clear();
                        jestWpisany = true;
                        continue;
                    }
                }
                // Jesli wcisnieto backspace i input jest wiekszy niz 0
                if (inputKey.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1); // Usun ostatni znak z inputu
                    Console.Write("\b \b"); // Usun ostatni znak z konsoli
                }
                else if (inputKey.Key != ConsoleKey.Backspace)
                // Jesli wcisnieto inny klawisz niz backspace
                {
                    input.Append(inputKey.KeyChar); // Dodaj znak do inputu
                    Console.Write(gwiazdki + "*"); // Wyswietl gwiazdke
                }
            }
            return input.ToString();
        }

        public static void WyswietlWiadomosc(string msg, bool success = true)
        {
            if (success)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor(); // Resetuje kolor konsoli
            WcisnijEnterByKontynuowac();
        }

        public static string OdbierzDaneUzytkownika(string prompt)
        {
            Console.WriteLine($"Wprowadz {prompt}");
            return Console.ReadLine();
        }

        public static void WyswietlAnimacjeKropek(int timer = 10)
        {
            for (int i = 0; i < timer; i++) // Wyswietlenie 10 kropek co 200ms
            {
                Console.Write(".");
                Thread.Sleep(200); // 1000ms = 1s (Wyswietlenie kropki co 200ms)
            }
            Console.Clear();
        }

        public static void WcisnijEnterByKontynuowac()
        {
            Console.WriteLine("\n\n Wcisnij Enter aby kontynuowac");
            Console.ReadLine();
        }
    }
}
