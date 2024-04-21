namespace DesktopAppNicola.Klasy
{
    internal class Bank
    {
        public string? Name { get; set; } // Nazwa banku
        public string? Description { get; set; }
        public List<Customer> Customers { get; set; } // Lista klientów banku
    }
}
