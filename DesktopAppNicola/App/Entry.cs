using DesktopAppNicola.UI;

namespace DesktopAppNicola.App
{
    class Entry
    {
        static void Main(string[] args)
        {
            AppScreen.Powitanie();
            Program program = new Program(); // Program to klasa z pliku Program.cs

            program.InicjalizujDane();
            program.Sprawdz_Num_Karty_Klienta_I_Haslo();
            program.Powitaj_Zalogowanego_Uzytkownika();

            Utility.WcisnijEnterByKontynuowac();
        }
    }
}
