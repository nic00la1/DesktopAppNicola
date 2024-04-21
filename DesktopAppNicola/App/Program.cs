using DesktopAppNicola.UI;

class Program
{
    static void Main(string[] args)
    {
        AppScreen.Powitanie();
        string cardNumber = Utility.OdbierzDaneUzytkownika("swoj numer karty");
        Console.WriteLine($"Twoj numer karty to: {cardNumber}");

        Utility.WcisnijEnterByKontynuowac();
    }
}