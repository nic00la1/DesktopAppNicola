using DesktopAppNicola.Klasy;

namespace DesktopAppNicola.UI
{
    public static class AppScreen
    {
        internal static void Powitanie()
        {
            Console.Clear();

            Console.WriteLine("\n\n-------Witaj w bankomacie!-------");
            Console.WriteLine("Wloz swoja karte bankomatowa");

            Console.WriteLine("Notka: Program jest w fazie testow, nie wprowadzaj prawdziwych danych!");

            Console.WriteLine("Uwaga: Bankomat zaakceptuje i zatwierdzi karte bankomatowa, odczyta numer karty i zatwierdzi ja");

            Console.WriteLine("\n\n Wcisnij Enter aby kontynuowac");
            Console.ReadLine();
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
    }
}
