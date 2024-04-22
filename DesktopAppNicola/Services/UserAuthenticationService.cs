﻿using DesktopAppNicola.Interfejsy;
using DesktopAppNicola.Klasy;
using DesktopAppNicola.UI;

namespace DesktopAppNicola.Services
{
    public class UserAuthenticationService : IUserLogin
    {
        private List<UserAccount> listaUzytkownikow; // Lista kont uzytkownikow
        private UserAccount wybranyUzytkownik; // Wybrany uzytkownik

        public UserAuthenticationService(List<UserAccount> _listaUzytkownikow)
        {
            listaUzytkownikow = _listaUzytkownikow; // Przekazanie listy kont uzytkownikow do konstruktora klasy 
        }

        public UserAccount Sprawdz_Num_Karty_Klienta_I_Haslo()
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
                                return wybranyUzytkownik; // Zwraca zautoryzowanego użytkownika
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
            return null; // Zwraca null, gdy autoryzacja się nie powiedzie
        }
    }
}
