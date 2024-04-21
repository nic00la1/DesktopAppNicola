using DesktopAppNicola.UI;

class Program
{
    static void Main(string[] args)
    {
        AppScreen.Powitanie();
        long cardNumber = Walidacja.Convert<long>("twoj numer karty");
        Console.WriteLine($"Twoj numer karty to: {cardNumber}");

        Utility.WcisnijEnterByKontynuowac();
    }
}