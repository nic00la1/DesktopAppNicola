using System.ComponentModel;

namespace DesktopAppNicola.UI
{
    public static class Walidacja
    {
        public static T Convert<T>(string prompt)
        {
            bool valid = false;
            string userInput;

            while (!valid) // Jesli walidacja sie nie powiedzie
            {
                userInput = Utility.OdbierzDaneUzytkownika(prompt);

                try
                {
                    // Konwertuj string na typ T (w tym przypadku long)
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (converter != null)
                    {
                        // Jesli konwersja sie powiedzie, zwroc wartosc
                        return (T)converter.ConvertFromString(userInput);
                    }
                    else
                    {
                        return default;
                    }
                }
                catch
                {
                    Utility.WyswietlWiadomosc("Niepoprawne dane, sprobuj ponownie", false);
                }
            }
            return default;
        }
    }
}
