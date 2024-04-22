namespace DesktopAppNicola.UI
{
    public class Menu
    {
        static string[] pozycjeMenu = { "Sprawdz saldo konta", "Wplac pieniadze na konto" ,
                                        "Wyplac pieniadze z konta", "Wyloguj" };
        static int aktywnaPozycjaMenu = 0; // Zacznij od pierwszej pozycji
        private Program program;

        public Menu(Program program)
        {
            this.program = program;
        }

        public void StartMenu()
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

        private void PokazMenu()
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

        private void WybieranieOpcji()
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

        private void UruchomOpcje()
        {
            switch (aktywnaPozycjaMenu)
            {
                case 0:
                    program.Sprawdz_Swoje_Saldo();
                    break;
                case 1:
                    program.Wplac_Pieniadze();
                    break;
                case 2:
                    program.Wyplac_Pieniadze();
                    break;
                case 3:
                    AppScreen.WylogujProgress();
                    Utility.WyswietlWiadomosc("Udane wylogowanie. Prosze wyjac karte z bankomatu");
                    program.Run();
                    break;
            }
        }
    }
}
