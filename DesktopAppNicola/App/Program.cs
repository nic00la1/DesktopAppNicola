using DesktopAppNicola.Klasy;
using DesktopAppNicola.UI;

public class Program
{
    public List<UserAccount> listaUzytkownikow;
    public UserAccount wybranyUzytkownik;
    public List<Transaction> listaTransakcji;
    public AppScreen appScreen;


    public Program()
    {
        InicjalizujDane();
        appScreen = new AppScreen(this, listaUzytkownikow);
    }

    public void Run()
    {
        AppScreen.Powitanie();
        appScreen.Decyzja_Logowania_Lub_Rejestracji();
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

    public void Dodaj_Nowego_Uzytkownika(string fullName, int accountNumber,
                        int cardNumber, int cardPin, decimal initialBalance)
    {
        listaUzytkownikow.Add(new UserAccount
        {
            Id = listaUzytkownikow.Count + 1, // Automatyczne nadanie ID
            FullName = fullName,
            AccountNumber = accountNumber,
            CardNumber = cardNumber,
            CardPin = cardPin,
            AccountBalance = initialBalance,
            IsLocked = false, // Nowe konto jest domyslnie odblokowane
        });
    }
}

