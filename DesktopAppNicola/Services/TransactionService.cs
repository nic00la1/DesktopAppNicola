using DesktopAppNicola.Enums;
using DesktopAppNicola.Interfejsy;
using DesktopAppNicola.Klasy;
using DesktopAppNicola.UI;

namespace DesktopAppNicola.Services
{
    public class TransactionService : IUserAccountActions
    {
        private const decimal minimalna_kwota_na_koncie = 100.00m;

        Program program = new Program();


        // Referencja do wybranego użytkownika
        private UserAccount wybranyUzytkownik;

        // Konstruktor klasy TransactionService
        public TransactionService(UserAccount _wybranyUzytkownik, List<Transaction>? listaTransakcji)
        {
            wybranyUzytkownik = _wybranyUzytkownik;
        }

        // Metoda do wypłaty pieniędzy
        public void Wyplac_Pieniadze()
        {

            var kwota_transakcji = 0;
            int wybrana_kwota = AppScreen.WybierzKwote();

            // Jesli uzytkownik wybierze
            // nieprawidlowa kwote, wybierz kwote od nowa
            if (wybrana_kwota == -1)
            {
                Wyplac_Pieniadze();
                return;
            }
            else if (wybrana_kwota != 0) // Jesli prawidlowa kwota, przypisz ja do kwota_transakcji
                kwota_transakcji = wybrana_kwota;
            else
            {
                kwota_transakcji = Walidacja.Convert<int>($"kwote {AppScreen.waluta}");
            }

            // Walidacja wpisanej kwoty
            if (kwota_transakcji <= 0)
            {
                Utility.WyswietlWiadomosc("Kwota musi byc wieksza niz 0." +
                                   "Sprobuj ponownie", false);
                return;
            }
            // Jesli kwota transakcji nie jest
            // wielokrotnoscia 20, 50, 100, 200, 500
            if (kwota_transakcji % 20 != 0
                && kwota_transakcji % 50 != 0
                && kwota_transakcji % 100 != 0
                && kwota_transakcji % 200 != 0
                && kwota_transakcji % 500 != 0)
            {
                Utility.WyswietlWiadomosc("Kwota musi byc zgodna z banknotami!");
                return;
            }
            // Logika biznesowa (symulacja) - sprawdza czy
            // uzytkownik ma wystarczajaco srodkow na koncie

            if (kwota_transakcji > wybranyUzytkownik.AccountBalance)
            {
                Utility.WyswietlWiadomosc("Nie masz wystarczajaco srodkow na koncie!" +
                    $"Twoje saldo wynosi {Utility.FormatujKwote(wybranyUzytkownik.AccountBalance)}", false);
                return;
            }

            // Jesli uzytkownik chce wyplacic kwote
            // przewyzszajaca minimalna kwote
            // ktora musi pozostac na koncie
            if ((wybranyUzytkownik.AccountBalance - kwota_transakcji)
                < minimalna_kwota_na_koncie)
            {
                Utility.WyswietlWiadomosc("Nie mozesz wyplacic tej kwoty. " +
                                   "Twoje saldo musi miec zachowane " +
                                   $"minimum {Utility.FormatujKwote(minimalna_kwota_na_koncie)}", false);
                return;
            }

            // Binduje detale transakcji do obiektu transakcji
            // (Przekazuje dane)
            program.Wprowadz_Transakcje
                (
                wybranyUzytkownik.Id,
                TransactionType.Wyplata,
                -kwota_transakcji, // Ujemna kwota oznacza wyplate
                "" // Opis transakcji
                );

            // Aktualizuje saldo uzytkownika
            wybranyUzytkownik.AccountBalance -= kwota_transakcji;

            // Wyswietla komunikat o sukcesie
            Utility.WyswietlWiadomosc($"Twoja wyplata " +
                           $"{Utility.FormatujKwote(kwota_transakcji)} " +
                                      $"zostala wyplacona z konta", true);
        }

        public void Sprawdz_Swoje_Saldo()
        {
            throw new NotImplementedException();
        }

        public void Wplac_Pieniadze()
        {
            throw new NotImplementedException();
        }
    }
}
