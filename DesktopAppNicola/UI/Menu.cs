namespace DesktopAppNicola.UI
{
    public class Menu
    {
        static string[] pozycjeMenu = { "W jakim banku chcesz zalozyc konto?", "Zakoncz program" };
        static int aktywnaPozycjaMenu = 0; // Zacznij od pierwszej pozycji

        public static void StartMenu()
        {
            Console.Title = "Bankomat";
            Console.CursorVisible = false; // Ukryj kursor podczas wyboru opcji

            /*Petla dla metod potrzebnych do dzialania menu,
              jej skonczenie oznacza wcisniecie klawisza przez uzytkownika */
            while (true)
            {
                PokazMenu();
                WybieranieOpcji();
                UruchomOpcje();
            }
        }

        private static void PokazMenu()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear(); // Wyczysc ekran
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(">>> Bankomat <<<\n");

            for (int i = 0; i < pozycjeMenu.Length; i++) // Petla dla kazdej pozycji menu
            {
                if (i == aktywnaPozycjaMenu) // Jesli pozycja jest aktywna
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                    // Wyswietl pozycje menu na srodku ekranu, wartosc -35
                    // to odleglosc od lewej krawedzi
                    Console.WriteLine("{0, -35}", pozycjeMenu[i]);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                else
                {
                    Console.WriteLine(pozycjeMenu[i]);
                }
            }
        }

        private static void WybieranieOpcji()
        {
            do
            {
                ConsoleKeyInfo klawisz = Console.ReadKey(); // Pobierz klawisz od uzytkownika
                if (klawisz.Key == ConsoleKey.UpArrow) // strzalka w gore, 
                {
                    aktywnaPozycjaMenu = aktywnaPozycjaMenu > 0 ? aktywnaPozycjaMenu - 1 : pozycjeMenu.Length - 1;
                    //Jesli pozycja na samej gorze = podswietlenie przejdzie do samego konca 
                    PokazMenu();
                }
                else if (klawisz.Key == ConsoleKey.DownArrow) // strzalka w dol
                {
                    // Jesli pozycja na samej dole = podswietlenie przejdzie na samej gore
                    aktywnaPozycjaMenu = (aktywnaPozycjaMenu + 1) % pozycjeMenu.Length;
                    PokazMenu();
                }
                else if (klawisz.Key == ConsoleKey.Escape)
                {
                    aktywnaPozycjaMenu = pozycjeMenu.Length - 1; // Zakoncz program
                    break;
                }
                else if (klawisz.Key == ConsoleKey.Enter)
                {
                    break; // Wybierz aktywna pozycje
                }

            }
            while (true);
        }

        private static void UruchomOpcje()
        {
            switch (aktywnaPozycjaMenu)
            {
                case 0:
                    opcjaWBudowie();
                    break;
                case 1:
                    Environment.Exit(0);
                    break;
            }
        }

        private static void opcjaWBudowie()
        {
            // Kursor 12 kolumna, 4 wiersz
            Console.SetCursorPosition(12, 4);
            Console.WriteLine("Opcja w budowie");
            Console.ReadKey();
        }
    }
}
