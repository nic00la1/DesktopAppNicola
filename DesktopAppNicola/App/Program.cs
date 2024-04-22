using DesktopAppNicola.Klasy;
using DesktopAppNicola.Services;
using DesktopAppNicola.UI;

public class Program
{
    public List<UserAccount> listaUzytkownikow;
    public UserAccount wybranyUzytkownik;
    public List<Transaction> listaTransakcji;

    private readonly UserAuthenticationService _autoryzujService;

    public Program()
    {
        InicjalizujDane();
        _autoryzujService = new UserAuthenticationService(listaUzytkownikow);

        // Przekazanie listyTransakcji do serwisu transakcji
        var transactionService = new TransactionService(wybranyUzytkownik, listaUzytkownikow, listaTransakcji);
    }

    public void Run()
    {
        AppScreen.Powitanie();
        // Uzycie serwisu autoryzacji uzytkownika
        wybranyUzytkownik = _autoryzujService.Sprawdz_Num_Karty_Klienta_I_Haslo();
        AppScreen.Powitaj_Zalogowanego_Uzytkownika(wybranyUzytkownik.FullName);

        // Tworzy nowy obiekt Menu i przekazuje do niego obecny obiekt Program
        // jako argument konstruktora, poniewaz Menu potrzebuje dostepu do metod
        // w obiekcie Program
        while (true) // dopoki user sie nie wyloguje
        {
            Menu menu = new Menu(this);
            menu.StartMenu();
        }
    }

    public void InicjalizujDane()
    {
        listaUzytkownikow = new List<UserAccount>
        {
            new UserAccount
            {
                Id = 1,
                FullName = "Nicola Kaleta",
                AccountNumber = 123456,
                CardNumber = 321321,
                CardPin = 123123,
                AccountBalance = 1000.00m, // m oznacza walute
                IsLocked = false,
            },
            new UserAccount
            {
                Id = 2,
                FullName = "Jan Kowalski",
                AccountNumber = 654321,
                CardNumber = 123123,
                CardPin = 321321,
                AccountBalance = 2000.00m,
                IsLocked = false,
            },
            new UserAccount
            {
                Id = 3,
                FullName = "Anna Nowak",
                AccountNumber = 987654,
                CardNumber = 456456,
                CardPin = 654654,
                AccountBalance = 3000.00m,
                IsLocked = true,
            }
        };
        // Inicjalizuje liste transakcji jako nowa pusta liste
        listaTransakcji = new List<Transaction>();
    }
}
