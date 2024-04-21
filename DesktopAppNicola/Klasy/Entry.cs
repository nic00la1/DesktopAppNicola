using DesktopAppNicola.UI;

namespace DesktopAppNicola.Klasy
{
    class Entry
    {
        static void Main(string[] args)
        {
            AppScreen.Powitanie();
            Program program = new Program(); // Program to klasa z pliku Program.cs

            program.SprawdzNumKartyKlientaIHaslo();

            Utility.WcisnijEnterByKontynuowac();
        }
    }
}
