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
            Console.WriteLine("\nWprowadz swoje dane");

            string fullName = WalidujImieINazwisko();
            if (fullName == null) // Jesli uzytkownik wcisnal Enter 
                return;
            int accountNumber = WalidujNumer("Numer konta: ",
                x => x != 0 // Numer konta nie moze byc pusty
                && x.ToString().Length == 6 // Numer konta musi miec 6 cyfr
                && !listaUzytkownikow.Exists(u => u.AccountNumber == x));
            // Numer konta nie moze sie powtarzac


            if (accountNumber == 0) // Jesli uzytkownik wcisnal Enter
                return;

            int cardNumber = WalidujNumer("Numer karty: ",
                x => x != 0
                && x.ToString().Length == 6
                && !listaUzytkownikow.Exists(u => u.CardNumber == x));
            if (cardNumber == 0)
                return;

            int cardPin = Convert.ToInt32(Utility.SzyfrujZnaki("PIN: "));
            if (cardPin == 0)
                return;

            int cardPinConfirm;
            do
            {
                cardPinConfirm = Convert.ToInt32(Utility.SzyfrujZnaki("\nPotwierdz PIN: "));
                if (cardPinConfirm == 0)
                    return;

                if (cardPin != cardPinConfirm)
                    Utility.WyswietlWiadomosc("PIN nie zgadza sie. Sprobuj ponownie.", false);

            } while (cardPin != cardPinConfirm); // Jesli PIN sie nie zgadza, powtarzaj

            decimal initialBalance = 1000; // Saldo poczatkowe

            Dodaj_Nowego_Uzytkownika(fullName, accountNumber, cardNumber, cardPin, initialBalance);

            RegisterProgress();
            Powitaj_Zarejestrowanego_Uzytkownika(fullName);

            Utility.WyswietlWiadomosc("Konto zostalo utworzone pomyslnie. " +
                               "Wcisnij Enter aby kontynuowac", true);

            Console.WriteLine("\nTwoje dane:");
            Console.WriteLine($"Imie i nazwisko: {fullName}");
            Console.WriteLine($"Numer konta: {accountNumber}");
            Console.WriteLine($"Numer karty: {cardNumber}");
            Console.WriteLine($"Saldo poczatkowe: {initialBalance}\n\n");
        }


        private string WalidujImieINazwisko()
        {
            string input;
            do
            {
                Console.WriteLine("Imie i nazwisko: ");
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    Utility.WyswietlWiadomosc("Imie i nazwisko nie moze byc puste. Sprobuj ponownie.", false);

            } while (string.IsNullOrEmpty(input));

            return input;
        }

        private int WalidujNumer(string message, Func<int, bool> condition)
        {
            int input;
            do
            {
                Console.WriteLine(message);
                // Jesli nie jest liczba lub nie spelnia warunku
                if (!int.TryParse(Console.ReadLine(), out input) || !condition(input))
                    Utility.WyswietlWiadomosc("Niepoprawny format. Sprobuj ponownie.", false);

            } while (!condition(input));

            return input;
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

            // Zapisuje dane do pliku JSON
            var fileName = @"C:\Users\Admin\source\repos\DesktopAppNicola\DesktopAppNicola\DummyData\users.json";
            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(listaUzytkownikow);
            File.WriteAllText(fileName, jsonData);
        }

        internal static void RegisterProgress()
        {
            Console.WriteLine("\nSprawdzanie poprawnosc danych");
            Utility.WyswietlAnimacjeKropek();
        }

        internal static void Powitaj_Zarejestrowanego_Uzytkownika(string fullname)
        {
            Console.WriteLine($"\nWitaj, {fullname}! Zaloguj sie, aby sprawdzic" +
                $" czy to napewno Ty.");
            Utility.WcisnijEnterByKontynuowac();
        }


    }
}
