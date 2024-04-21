namespace DesktopAppNicola.UI
{
    public static class Utility
    {
        public static void WyswietlWiadomosc(string msg, bool success = true)
        {
            if (success)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor(); // Resetuje kolor konsoli
            WcisnijEnterByKontynuowac();
        }

        public static string OdbierzDaneUzytkownika(string prompt)
        {
            Console.WriteLine($"Wprowadz {prompt}");
            return Console.ReadLine();
        }

        public static void WcisnijEnterByKontynuowac()
        {
            Console.WriteLine("\n\n Wcisnij Enter aby kontynuowac");
            Console.ReadLine();
        }
    }
}
