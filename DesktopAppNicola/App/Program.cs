using DesktopAppNicola.Interfejsy;
using DesktopAppNicola.Klasy;
using DesktopAppNicola.UI;

public class Program : IUserLogin, IUserAccountActions
{
    private List<UserAccount> listaUzytkownikow;
    private UserAccount wybranyUzytkownik;

    public void Run()
    {
        AppScreen.Powitanie();
        Sprawdz_Num_Karty_Klienta_I_Haslo();
        AppScreen.Powitaj_Zalogowanego_Uzytkownika(wybranyUzytkownik.FullName);
        // Tworzy nowy obiekt Menu i przekazuje do niego obecny obiekt Program
        // jako argument konstruktora, poniewaz Menu potrzebuje dostepu do metod
        // w obiekcie Program
        Menu menu = new Menu(this);
        menu.StartMenu();
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
    }

    public void Sprawdz_Num_Karty_Klienta_I_Haslo()
    {
        bool czyPoprawnyLogin = false;

        while (czyPoprawnyLogin == false)
        {
            UserAccount inputAccount = AppScreen.UserLoginForm(); // Pobiera dane z formularza logowania
            AppScreen.LoginProgress(); // Wyświetla animacje kropek (symulacja logowania)

            // Dla kazdego usera w liscie uzytkownikow, sprawdz czy numer karty i pin sa poprawne
            foreach (UserAccount uzytkownik in listaUzytkownikow)
            {
                wybranyUzytkownik = uzytkownik;
                // Sprawdza czy numer karty i pin sa poprawne
                if (inputAccount.CardNumber.Equals(wybranyUzytkownik.CardNumber))
                {
                    wybranyUzytkownik.TotalLogin++; // Zwieksza liczbe prob logowania

                    // Jesli pin jest poprawny
                    if (inputAccount.CardPin.Equals(wybranyUzytkownik.CardPin))
                    {
                        wybranyUzytkownik = uzytkownik;

                        // Jesli konto jest zablokowane lub liczba prob logowania jest wieksza niz 3
                        if (wybranyUzytkownik.IsLocked || wybranyUzytkownik.TotalLogin > 3)
                        {
                            // Wyswietl komunikat o zablokowanym koncie
                            AppScreen.Wyswietl_Komunikat_O_Zablokowanym_Koncie();
                        }
                        else
                        {
                            /* Jesli konto nie jest zablokowane,
                             resetuj liczbe prob logowania */
                            wybranyUzytkownik.TotalLogin = 0;
                            czyPoprawnyLogin = true;
                            break;
                        }
                    }
                }
                // Jesli numer karty i pin sa niepoprawne
                if (czyPoprawnyLogin == false)
                {
                    Utility.WyswietlWiadomosc("\n Numer karty lub PIN jest niepoprawny. ", false);
                    // Jesli liczba prob logowania jest wieksza niz 3, zablokuj konto
                    wybranyUzytkownik.IsLocked = wybranyUzytkownik.TotalLogin == 3;

                    if (wybranyUzytkownik.IsLocked)
                    {
                        AppScreen.Wyswietl_Komunikat_O_Zablokowanym_Koncie();
                    }
                }
                Console.Clear();
            }
        }
    }

    public void Sprawdz_Swoje_Saldo()
    {
        if (wybranyUzytkownik != null) // Jesli uzytkownik nie jest nullem
        {
            Utility.WyswietlWiadomosc($"Twoje saldo wynosi: " +
           $"{Utility.FormatujKwote(wybranyUzytkownik.AccountBalance)}");
        }
        else
        {
            Utility.WyswietlWiadomosc("Nie wybrano uzytkownika");
        }

    }

    public void Wplac_Pieniadze()
    {
        throw new NotImplementedException();
    }

    public void Wyplac_Pieniadze()
    {
        throw new NotImplementedException();
    }
}
