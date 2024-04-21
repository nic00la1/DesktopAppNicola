namespace DesktopAppNicola.Klasy
{
    public class Customer // Klasa reprezentujaca klienta
    {
        public int Id { get; set; } // Id klienta
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Account? BankAccount { get; set; } // Konto bankowe klienta

    }
}
