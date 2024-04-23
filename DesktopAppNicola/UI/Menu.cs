using DesktopAppNicola.Services;

namespace DesktopAppNicola.UI
{
    public class Menu
    {
        static string[] pozycjeMenu = { "Sprawdz saldo konta", "Wplac pieniadze na konto" ,
                                        "Wyplac pieniadze z konta", "Wykonaj przelew",
                                        "Zobacz wszystkie transakcje", "Wyloguj" };
        static int aktywnaPozycjaMenu = 0; // Zacznij od pierwszej pozycji
        private readonly Program program;
        private readonly AppScreen screen;
        private readonly TransactionService transactionService;

        public Menu(Program program, LoginService loginService, AppScreen screen)
        {
            this.program = program;
            this.screen = screen;
            transactionService = new TransactionService(
                loginService.wybranyUzytkownik,
                program.listaUzytkownikow,
                program.listaTransakcji);
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
                    transactionService.Sprawdz_Swoje_Saldo();
                    break;
                case 1:
                    transactionService.Wplac_Pieniadze();
                    break;
                case 2:
                    transactionService.Wyplac_Pieniadze();
                    break;
                case 3:
                    var przelewMiedzyKontami = screen.Formularz_Do_Przelewu(); // Przypisanie obiektu przelewMiedzyKontami do metody Formularz_Do_Przelewu
                    transactionService.Process_Przelewu_Miedzy_Kontami(przelewMiedzyKontami);
                    break;
                case 4:
                    transactionService.Zobacz_Transakcje();
                    break;
                case 5:
                    LoginService.WylogujProgress();
                    Utility.WyswietlWiadomosc("Udane wylogowanie. Prosze wyjac karte z bankomatu");
                    program.Run();
                    break;
            }
        }
    }
}
