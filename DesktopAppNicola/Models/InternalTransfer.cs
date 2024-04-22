namespace DesktopAppNicola.Klasy
{
    public class InternalTransfer
    {
        public decimal TransferAmount { get; set; } // Kwota przelewu
        public long RecipeintBankAccountNumber { get; set; } // Numer konta odbiorcy
        public string RecipeintBankAccountName { get; set; } // Nazwa konta odbiorcy
    }
}
