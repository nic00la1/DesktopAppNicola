using DesktopAppNicola.Klasy;
using DesktopAppNicola.UI;

namespace DesktopAppNicola.Services
{
    public class RegisterService
    {
        public List<UserAccount> listaUzytkownikow;

        public RegisterService(List<UserAccount> _listaUzytkownikow)
        {
            listaUzytkownikow = _listaUzytkownikow;
        }

        public void Registruj_Form()
        {
            Console.Clear();

            Console.WriteLine("\n\n-------Rejestracja nowego konta-------");
            Console.WriteLine("Wprowadz swoje dane");
            Console.WriteLine("Imie i nazwisko: ");
            string fullName = Console.ReadLine();

            if (string.IsNullOrEmpty(fullName))
            {
                Utility.WyswietlWiadomosc("Imie i nazwisko nie moze byc puste. Sprobuj ponownie.", false);
                return;
            }

            Console.WriteLine("Numer konta: ");
            int accountNumber = Convert.ToInt32(Console.ReadLine());

            if (accountNumber == 0)
            {
                Utility.WyswietlWiadomosc("Numer konta nie moze byc zerem. Sprobuj ponownie.", false);
                return;
            }
            // Jesli to nie numer
            if (!int.TryParse(accountNumber.ToString(), out int czyNumer))
            {
                Utility.WyswietlWiadomosc("Numer konta musi byc liczba. Sprobuj ponownie.", false);
                return;
            }
            else if (accountNumber.ToString().Length != 6)
            {
                Utility.WyswietlWiadomosc("Numer konta musi miec 6 cyfr. Sprobuj ponownie.", false);
                return;
            }
            else if (listaUzytkownikow.Exists(x => x.AccountNumber == accountNumber))
            {
                Utility.WyswietlWiadomosc("Konto o podanym numerze juz istnieje. Sprobuj ponownie.", false);
                return;
            }

            Console.WriteLine("Numer karty: ");
            int cardNumber = Convert.ToInt32(Console.ReadLine());

            if (cardNumber == 0)
            {
                Utility.WyswietlWiadomosc("Numer karty nie moze byc zerem. Sprobuj ponownie.", false);
                return;
            }

            else if (cardNumber.ToString().Length != 6)
            {
                Utility.WyswietlWiadomosc("Numer karty musi miec 6 cyfr. Sprobuj ponownie.", false);
                return;
            }
            else if (listaUzytkownikow.Exists(x => x.CardNumber == cardNumber))
            {
                Utility.WyswietlWiadomosc("Karta o podanym numerze juz istnieje. Sprobuj ponownie.", false);
                return;
            }

            Console.WriteLine("Numer PIN: ");
            int cardPin = Convert.ToInt32(Console.ReadLine());

            // jesli PIN to nie numer
            if (!int.TryParse(cardPin.ToString(), out int result))
            {
                Utility.WyswietlWiadomosc("PIN musi byc liczba. Sprobuj ponownie.", false);
                return;
            }
            if (cardPin == 0)
            {
                Utility.WyswietlWiadomosc("PIN nie moze byc zerem. Sprobuj ponownie.", false);
            }
            else if (cardPin.ToString().Length != 6)
            {
                Utility.WyswietlWiadomosc("PIN musi miec 6 cyfr. Sprobuj ponownie.", false);
                return;
            }

            Console.WriteLine("Potwierdz PIN: ");
            int cardPinConfirm = Convert.ToInt32(Console.ReadLine());

            if (cardPin != cardPinConfirm)
            {
                Utility.WyswietlWiadomosc("PIN nie zgadza sie. Sprobuj ponownie.", false);
                return;
            }

            decimal initialBalance = 1000; // Saldo poczatkowe

            Dodaj_Nowego_Uzytkownika(fullName, accountNumber, cardNumber, cardPin, initialBalance);

            Utility.WyswietlWiadomosc("Konto zostalo utworzone pomyslnie. " +
                               "Wcisnij Enter aby kontynuowac", true);

            Console.WriteLine("\nTwoje dane:");
            Console.WriteLine($"Imie i nazwisko: {fullName}");
            Console.WriteLine($"Numer konta: {accountNumber}");
            Console.WriteLine($"Numer karty: {cardNumber}");
            Console.WriteLine($"Saldo poczatkowe: {initialBalance}\n\n");
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
}
