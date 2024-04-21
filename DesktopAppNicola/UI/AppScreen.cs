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

        internal static void Wyswietl_Komunikat_O_Zablokowanym_Koncie()
        {
            Console.Clear();
            Utility.WyswietlWiadomosc("Twoje konto jest zablokowane. Skontaktuj sie " +
                "ze swoim najblizszym bankiem, aby je odblokowac. Dziekujemy, Firma XYZ :(", true);

            Environment.Exit(1); // Zakoncz program
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
