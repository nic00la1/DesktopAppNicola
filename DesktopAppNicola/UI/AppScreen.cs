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
    }
}
