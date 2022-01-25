using Assignment2.Models;

namespace Assignment2.Utility;

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
    
}