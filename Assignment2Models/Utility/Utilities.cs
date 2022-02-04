using Assignment2Models.Models;

namespace Assignment2Models.Utility;

public static class Utilities
{
    public static bool HasMoreThanNDecimalPlaces(this decimal value, int n) => decimal.Round(value, n) != value;
    
    public static bool TwoDecimalPlacesCheck(this decimal value) => value.HasMoreThanNDecimalPlaces(2);

    public static decimal CalculateAccountBalance(this List<Transaction> transactions)
    {
        decimal amount = 0;
        
        foreach (var t in transactions)
        {
            switch (t.TransactionType)
            {
                case 'D':
                case 'T' when t.DestinationAccount == null:
                    amount += t.Amount;
                    break;
                case 'T' when t.DestinationAccount != null:
                    amount -= t.Amount;
                    break;
                case 'W' or 'S':
                    amount -= t.Amount;
                    break;
                default:
                    continue;
            }
            // add more for billpay
        }
        return amount;
    }

    public static bool MoreThanTwoTransactions(this List<Transaction> transactions)
    {
        int count = 0;

        foreach (var T in transactions)
        {
            if (T.TransactionType == Constants.Transfer || T.TransactionType == Constants.Withdraw)
            {
                count++;
            }
        }
        if (count > 2)
        {
            return true;
        }
        return false;
        
        
    }
    
}