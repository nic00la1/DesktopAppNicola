using ConsoleTables;
using DesktopAppNicola.Enums;
using DesktopAppNicola.Interfejsy;
using DesktopAppNicola.Klasy;
using DesktopAppNicola.Services;
using DesktopAppNicola.UI;

public class Program : ITransaction
{
    private List<UserAccount> listaUzytkownikow;
    public UserAccount wybranyUzytkownik;
    public List<Transaction> listaTransakcji;
    private const decimal minimalna_kwota_na_koncie = 100.00m;

    private readonly UserAuthenticationService _autoryzujService;

    public Program()
    {
        InicjalizujDane();
        _autoryzujService = new UserAuthenticationService(listaUzytkownikow);
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
        Console.WriteLine("\nWplac pieniadze na konto\n");
        var kwota_transakcji = Walidacja.Convert<int>($"kwote {AppScreen.waluta}");

        // Symulacja liczenia
        Console.WriteLine("Sprawdzanie i liczenie pieniedzy...");
        Utility.WyswietlAnimacjeKropek();
        Console.WriteLine($"");

        // Obsluga bledow
        if (kwota_transakcji <= 0)
        {
            Utility.WyswietlWiadomosc("Kwota musi byc wieksza niz 0." +
                "Sprobuj ponownie", false);
            return;
        }
        // Jesli kwota transakcji
        // Nie jest wielokrotnoscia 20, 50, 100, 200, 500
        if (kwota_transakcji % 20 != 0
            && kwota_transakcji % 50 != 0
            && kwota_transakcji % 100 != 0
            && kwota_transakcji % 200 != 0
            && kwota_transakcji % 500 != 0)
        {
            Utility.WyswietlWiadomosc("Kwota musi byc zgodna z banknotami!");
            return;
        }
        // Jesli uzytkownik przerwie transakcje
        if (Podglad_Ilosci_Banknotow(kwota_transakcji) == false)
        {
            Utility.WyswietlWiadomosc("Anulowales transakcje.", false);
            return;
        }

        // Binduje detale transakcji z obiektem transakcji
        // (Przekazuje dane)
        Wprowadz_Transakcje(
            wybranyUzytkownik.Id,
            TransactionType.Wplata,
            kwota_transakcji,
            "" // opis transakcji
            );

        // Aktualizuje saldo uzytkownika
        wybranyUzytkownik.AccountBalance += kwota_transakcji;

        // Wyswietla komunikat o sukcesie
        Utility.WyswietlWiadomosc($"Twoja wplata " +
            $"{Utility.FormatujKwote(kwota_transakcji)} " +
            $"zostala wplacona na konto", true);
    }

    private bool Podglad_Ilosci_Banknotow(int amount)
    {
        int originalAmount = amount; // Zapamiętaj początkową kwotę

        int banknot_500 = amount / 500;
        amount %= 500;
        int banknot_200 = amount / 200;
        amount %= 200;
        int banknot_100 = amount / 100;
        amount %= 100;
        int banknot_50 = amount / 50;
        amount %= 50;
        int banknot_20 = amount / 20;
        amount %= 20;

        Console.WriteLine($"Podsumowanie");
        Console.WriteLine("-----------------");

        // Wyswietla banknoty ktore zostana wydane
        // Przy tej konkretnej kwocie ktora
        // uzytkownik chce wplacic
        if (banknot_500 > 0)
            Console.WriteLine($"500{AppScreen.waluta} X {banknot_500}");
        if (banknot_200 > 0)
            Console.WriteLine($"200{AppScreen.waluta} X {banknot_200}");
        if (banknot_100 > 0)
            Console.WriteLine($"100{AppScreen.waluta} X {banknot_100}");
        if (banknot_50 > 0)
            Console.WriteLine($"50{AppScreen.waluta} X {banknot_50}");
        if (banknot_20 > 0)
            Console.WriteLine($"20{AppScreen.waluta} X {banknot_20}");

        Console.WriteLine($"Kwota calkowita: " +
            $"{Utility.FormatujKwote(originalAmount)}\n\n");

        int wybor = Walidacja.Convert<int>("1. Potwierdz\n");
        return wybor.Equals(1);
    }

    public void Wprowadz_Transakcje(long _UserBankAccountId, TransactionType _tranType, decimal _tranAmount, string _description)
    {
        // Tworzy nowy obiekt transakcji
        var transakcja = new Transaction()
        {
            TransactionId = Utility.DostanIdTransakcji(), // Pobiera unikalne id transakcji
            UserBankAccountId = _UserBankAccountId,
            TransactionDate = DateTime.Now, // Pobiera obecna date
            TransactionType = _tranType, // Typ transakcji
            TransactionAmount = _tranAmount, // Kwota transakcji
            Description = _description // Opis 
        };

        // Dodaje transakcje do listy transakcji
        listaTransakcji.Add(transakcja);
    }

    public void Zobacz_Transakcje()
    {
        // sortuje transakcje po Id uzytkownika
        var posortowaneTransakcje = listaTransakcji.Where(
                t => t.UserBankAccountId == wybranyUzytkownik.Id).ToList();

        // Sprawdz czy sa jakies transakcje
        if (posortowaneTransakcje.Count <= 0) // Jesli nie ma transakcji
        {
            Utility.WyswietlWiadomosc("Nie masz jeszcze zadnych transakcji.", true);
            return;
        }
        else // Jesli sa transakcje
        {
            // Uzywam paczke z NugetPackage - ,,ConsoleTables"
            var table = new ConsoleTable("Id", "Data transakcji",
                "Opisy", "Typ", "Kwota " + AppScreen.waluta);

            // Dla kazdej transakcji w posortowanych transakcjach
            // dodaj wiersz do tabeli
            foreach (var transakcja in posortowaneTransakcje)
            {
                table.AddRow(transakcja.TransactionId,
                             transakcja.TransactionDate,
                             transakcja.Description,
                             transakcja.TransactionType,
                             transakcja.TransactionAmount);
            }

            table.Options.EnableCount = false; // Wylacza numerowanie wierszy
            table.Write(); // Wyswietla tabele

            if (posortowaneTransakcje.Count >= 5)
                Utility.WyswietlWiadomosc($"Wykonales {posortowaneTransakcje.Count} transakcji", true);
            else
                Utility.WyswietlWiadomosc($"Wykonales {posortowaneTransakcje.Count} transakcje", true);
        }
    }

    public void Process_Przelewu_Miedzy_Kontami(
        InternalTransfer przelewMiedzyKontami)
    {
        if (przelewMiedzyKontami.TransferAmount <= 0)
        {
            Utility.WyswietlWiadomosc("Kwota musi byc wieksza niz 0." +
                               "Sprobuj ponownie", false);
            return;
        }
        // Sprawdz saldo uzytkownika (nadawcy)
        if (przelewMiedzyKontami.TransferAmount
            > wybranyUzytkownik.AccountBalance)
        {
            Utility.WyswietlWiadomosc("Nie masz wystarczajaco srodkow na koncie aby wykonac przelew!" +
                $"Twoje saldo wynosi {Utility.FormatujKwote(wybranyUzytkownik.AccountBalance)}", false);
            return;
        }
        // Sprawdz czy minimalna kwota na koncie
        // zostanie zachowana po przelewie
        if ((wybranyUzytkownik.AccountBalance - przelewMiedzyKontami.TransferAmount)
            < minimalna_kwota_na_koncie)
        {
            Utility.WyswietlWiadomosc
                ("Nie mozesz wykonac tego przelewu. " +
                "Twoje saldo musi miec zachowane " +
                $"minimum {Utility.FormatujKwote(minimalna_kwota_na_koncie)}"
                , false);
            return;
        }
        // Sprawdz czy numer konta odbiorcy jest poprawny, 
        // jesli nie, zwroc null 
        var wybrane_Konto_Odbiorcy = (from kontoUsera in listaUzytkownikow
                                      where kontoUsera.AccountNumber
                                      == przelewMiedzyKontami.RecipeintBankAccountNumber
                                      select kontoUsera).FirstOrDefault(); // Pobiera pierwszy element z listy lub null

        if (wybrane_Konto_Odbiorcy == null)
        {
            Utility.WyswietlWiadomosc("Przelew przerwany! "
                + "Nie znaleziono konta odbiorcy. "
                + "Sprobuj ponownie", false);
            return;
        }

        // Sprawdz Nazwe konta odbiorcy
        if (wybrane_Konto_Odbiorcy.FullName
            != przelewMiedzyKontami.RecipeintBankAccountName)
        {
            Utility.WyswietlWiadomosc("Przelew przerwany! " +
                "Nazwa odbiorcy jest niepoprawna.", false);
            return;
        }

        // Dodaj transakcje do listy transakcji
        // (wysylanie rekordu przelewu) (nadawca)
        Wprowadz_Transakcje
            (
            wybranyUzytkownik.Id,
            TransactionType.Przelew,
            -przelewMiedzyKontami.TransferAmount, // Ujemna kwota oznacza pieniadze przekazane
                                                  // na konto odbiorcy
            $"Przelew na konto: {wybrane_Konto_Odbiorcy.AccountNumber}" +
            $" ({wybrane_Konto_Odbiorcy.FullName})" // opis transakcji
            );

        // Aktualizuj saldo uzytkownika, wysylajacego przelew (nadawcy)
        wybranyUzytkownik.AccountBalance -= przelewMiedzyKontami.TransferAmount;

        // Dodaj transakcje do listy transakcji
        // (otrzymywanie rekordu przelewu) (odbiorca)
        Wprowadz_Transakcje(
                wybrane_Konto_Odbiorcy.Id,
                TransactionType.Przelew,
                przelewMiedzyKontami.TransferAmount, // Dodatnia kwota oznacza pieniadze otrzymane
                $"Przelew od: {wybranyUzytkownik.AccountNumber}" +
                $"({wybranyUzytkownik.FullName})" // opis transakcji
            );

        // Aktualizuj saldo uzytkownika, (odbiorcy przelewu)
        wybrane_Konto_Odbiorcy.AccountBalance += przelewMiedzyKontami.TransferAmount;

        // Wyswietl komunikat o sukcesie
        Utility.WyswietlWiadomosc("Udalo sie wykonac przelew! " +
            $"Kwota {Utility.FormatujKwote(przelewMiedzyKontami.TransferAmount)} " +
            $"zostala przelana do" +
            $" {przelewMiedzyKontami.RecipeintBankAccountName}", true);

    }
}
