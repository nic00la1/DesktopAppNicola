namespace DesktopAppNicola.App
{
    class Entry
    {
        static void Main(string[] args)
        {
            Program program = new Program(); // Program to klasa z pliku Program.cs
            program.InicjalizujDane();
            program.Run(); // Uruchamia program rozpoczynajac od ekranu powitalnego i logowania
        }
    }
}
