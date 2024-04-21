namespace DesktopAppNicola.Klasy
{
    public class Account // Klasa reprezentujaca konto bankowe
    {
        public string? AccountNumber { get; set; } // Numer konta bankowego
        public string? AccountType { get; set; } // Typ konta bankowego
        public decimal Balance { get; set; } // Saldo konta bankowego
        public List<Transaction> Transactions { get; set; } // Lista transakcji na koncie
    }
}
