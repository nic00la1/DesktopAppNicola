using DesktopAppNicola.Klasy;
using DesktopAppNicola.Services;

namespace DesktopAppNicola.UI
{
    public class AppScreen
    {
        private readonly Program program;
        private readonly LoginService loginService;
        internal const string waluta = "PLN";

        public AppScreen(Program program, List<UserAccount> listaUzytkownikow)
        {
            this.program = program;
            loginService = new LoginService(program, listaUzytkownikow, this);
        }

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

        internal void Decyzja_Logowania_Lub_Rejestracji()
        {
            Console.WriteLine("\n>>> Wybierz opcje: <<<");
            Console.WriteLine("1. Zaloguj sie");
            Console.WriteLine("2. Zarejestruj nowe konto");

            int wybor = Walidacja.Convert<int>("decyzje wejscia: ");
            switch (wybor)
            {
                case 1:
                    loginService.ZalogujSie();
                    break;
                default:
                    Console.WriteLine("Rejestracja nowego konta nie jest jeszcze dostepna w tej wersji programu. Przepraszamy za utrudnienia.");
                    Console.ReadKey();
                    break;
            }
        }

        internal static void Wyswietl_Komunikat_O_Zablokowanym_Koncie()
        {
            Console.Clear();
            Utility.WyswietlWiadomosc("Twoje konto jest zablokowane. Skontaktuj sie " +
                "ze swoim najblizszym bankiem, aby je odblokowac. Dziekujemy, Firma XYZ :(", true);

            Environment.Exit(1); // Zakoncz program
        }


        internal static int WybierzKwote()
        {
            Console.WriteLine("");
            Console.WriteLine(":1. 20{0}        5. 500{0}", waluta);
            Console.WriteLine(":2. 50{0}        6. 800{0}", waluta);
            Console.WriteLine(":3. 100{0}       7. 1000{0}", waluta);
            Console.WriteLine(":4. 200{0}       8. 2000{0}", waluta);
            Console.WriteLine(":0. Inna\n");

            int wybranaKwota = Walidacja.Convert<int>("opcje:");
            switch (wybranaKwota)
            {
                case 1:
                    return 20;
                    break;
                case 2:
                    return 50;
                    break;
                case 3:
                    return 100;
                    break;
                case 4:
                    return 200;
                    break;
                case 5:
                    return 500;
                    break;
                case 6:
                    return 800;
                    break;
                case 7:
                    return 1000;
                    break;
                case 8:
                    return 2000;
                    break;
                case 0:
                    return 0;
                    break;
                default:
                    Utility.WyswietlWiadomosc("Nieprawidlowa kwota. Sprobuj ponownie", false);
                    return -1;
                    break;
            }
        }
        internal InternalTransfer Formularz_Do_Przelewu()
        {
            // nowy obiekt klasy InternalTransfer
            var przelewMiedzyKontami = new InternalTransfer();

            przelewMiedzyKontami.RecipeintBankAccountNumber =
                Walidacja.Convert<long>("numer konta odbiorcy");

            przelewMiedzyKontami.TransferAmount =
                Walidacja.Convert<decimal>($"kwote {waluta}");

            przelewMiedzyKontami.RecipeintBankAccountName =
                Utility.OdbierzDaneUzytkownika("nazwe odbiorcy: ");

            return przelewMiedzyKontami;
        }
    }
}
