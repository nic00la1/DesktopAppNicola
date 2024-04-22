using DesktopAppNicola.Enums;

namespace DesktopAppNicola.Interfejsy
{
    public interface ITransaction
    {
        void Wprowadz_Transakcje
            (
            long _UserBankAccountId,
            TransactionType _tranType,
            decimal _tranAmount,
            string _description
            );

        void Zobacz_Transakcje();
    }
}
