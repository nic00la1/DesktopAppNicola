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
        var fileNameUsers = @"C:\Users\Admin\source\repos\DesktopAppNicola\DesktopAppNicola\DummyData\users.json";
        var fileNameTransactions = @"C:\Users\Admin\source\repos\DesktopAppNicola\DesktopAppNicola\DummyData\transactions.json";

        string jsonUsers = File.ReadAllText(fileNameUsers);
        string jsonTransactions = File.ReadAllText(fileNameTransactions);

        listaUzytkownikow = Newtonsoft.Json
            .JsonConvert.DeserializeObject<List<UserAccount>>(jsonUsers);

        listaTransakcji = Newtonsoft.Json
            .JsonConvert.DeserializeObject<List<Transaction>>(jsonTransactions);
    }
}

