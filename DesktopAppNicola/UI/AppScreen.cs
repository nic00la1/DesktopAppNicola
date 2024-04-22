﻿using DesktopAppNicola.Klasy;

namespace DesktopAppNicola.UI
{
    public static class AppScreen
    {
        internal const string waluta = "PLN";
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

        internal static int WybierzKwote()
        {
            Console.WriteLine("");
            Console.WriteLine(":1. 20{0}        5. 500{0}", waluta);
            Console.WriteLine(":2. 50{0}        6. 800{0}", waluta);
            Console.WriteLine(":3. 100{0}       7. 1000{0}", waluta);
            Console.WriteLine(":4. 200{0}       8. 2000{0}", waluta);
            Console.WriteLine(":0. Inna\n");

            int wybranaKwota = Walidacja.Convert<int>("opcje:");
            switch (wybranaKwota)
            {
                case 1:
                    return 20;
                    break;
                case 2:
                    return 50;
                    break;
                case 3:
                    return 100;
                    break;
                case 4:
                    return 200;
                    break;
                case 5:
                    return 500;
                    break;
                case 6:
                    return 800;
                    break;
                case 7:
                    return 1000;
                    break;
                case 8:
                    return 2000;
                    break;
                case 0:
                    return 0;
                    break;
                default:
                    Utility.WyswietlWiadomosc("Nieprawidlowa kwota. Sprobuj ponownie", false);
                    WybierzKwote();
                    return -1;
                    break;
            }
        }
    }
}
