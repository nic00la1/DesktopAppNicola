using DesktopAppNicola.Enums;

namespace DesktopAppNicola.Klasy
{
    public class Transaction
    {
        public long TransactionId { get; set; }
        public long UserBankAccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public string? Description { get; set; }
        public Decimal TransactionAmount { get; set; }

    }
}
