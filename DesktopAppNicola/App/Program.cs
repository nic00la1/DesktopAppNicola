using DesktopAppNicola.Klasy;
using DesktopAppNicola.UI;

public class Program
{
    public List<UserAccount> listaUzytkownikow;
    public UserAccount wybranyUzytkownik;
    public List<Transaction> listaTransakcji;
    public AppScreen appScreen;


    public Program()
    {
        InicjalizujDane();
        appScreen = new AppScreen(this, listaUzytkownikow);
    }

    public void Run()
    {
        AppScreen.Powitanie();
        appScreen.Decyzja_Logowania_Lub_Rejestracji();
    }

    public void InicjalizujDane()
    {
        // Uzywam paczke z NugetPackage
        // Newtonsoft.Json do deserializacji danych z pliku JSON
        var fileName = @"C:\Users\Admin\source\repos\DesktopAppNicola\DesktopAppNicola\DummyData\users.json";

        string jsonText = File.ReadAllText(fileName);

        listaUzytkownikow = Newtonsoft.Json
            .JsonConvert.DeserializeObject<List<UserAccount>>(jsonText);

        // Inicjalizuje liste transakcji
        listaTransakcji = new List<Transaction>();
    }
}

