using DesktopAppNicola.Interfejsy;
using DesktopAppNicola.Klasy;
using DesktopAppNicola.UI;

class Program : IUserLogin
{
    private List<UserAccount> listaUzytkownikow;
    private UserAccount wybranyUzytkownik;

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
                IsLocked = false,
            }
        };
    }

    public void SprawdzNumKartyKlientaIHaslo()
    {
        bool czyPoprawnyLogin = false;
        UserAccount inputAccount = AppScreen.UserLoginForm(); // Pobiera dane z formularza logowania

        AppScreen.LoginProgress();
    }
}