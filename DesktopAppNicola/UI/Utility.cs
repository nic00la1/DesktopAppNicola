namespace DesktopAppNicola.UI
{
    public static class Utility
    {
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
