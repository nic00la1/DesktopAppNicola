using DesktopAppNicola.Klasy;
using DesktopAppNicola.UI;

namespace DesktopAppNicola.Services
{
    public class LoginService
    {
        private List<UserAccount> listaUzytkownikow;
        public UserAccount wybranyUzytkownik;
        private readonly UserAuthenticationService _autoryzujService;
        private readonly Program program;
        private readonly AppScreen screen;


        public LoginService(Program program, List<UserAccount> _listaUzytkownikow, AppScreen appScreen)
        {
            this.program = program;
            listaUzytkownikow = _listaUzytkownikow;
            _autoryzujService = new UserAuthenticationService(listaUzytkownikow);
            screen = appScreen;
        }

        internal void ZalogujSie()
        {
            wybranyUzytkownik = _autoryzujService.Sprawdz_Num_Karty_Klienta_I_Haslo();
            Powitaj_Zalogowanego_Uzytkownika(wybranyUzytkownik.FullName);

            while (true) // dopoki user sie nie wyloguje
            {
                Menu menu = new Menu(program, this, screen);
                menu.StartMenu();
            }
        }

        // internal oznacza, ze metoda jest dostepna tylko w tym podzespole (UI)
        internal static UserAccount UserLoginForm()
        {
            // Tymczasowe konto uzytkownika
            UserAccount tempUserAccount = new UserAccount();

            tempUserAccount.CardNumber = Walidacja.Convert<long>("twoj numer karty");
            tempUserAccount.CardPin = Convert.ToInt32(Utility.SzyfrujZnaki("Wprowadz swoj pin do karty"));

            return tempUserAccount; // Zwroc tymczasowe konto uzytkownika
        }


        internal static void LoginProgress()
        {
            Console.WriteLine("\nSprawdzanie numeru karty i PIN'u...");
            Utility.WyswietlAnimacjeKropek();
        }


        internal static void Powitaj_Zalogowanego_Uzytkownika(string fullname)
        {
            Console.WriteLine($"Witaj ponownie, {fullname}!");
            Utility.WcisnijEnterByKontynuowac();
        }

        internal static void WylogujProgress()
        {
            Console.WriteLine("Dziekujemy za skorzystanie z uslug naszego bankomatu!");
            Utility.WyswietlAnimacjeKropek();
            Console.Clear();
        }
    }
}
