using System.Web;
using Assignment2.Models;
using Castle.DynamicProxy.Generators.Emitters;
using Newtonsoft.Json;

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
    
    // Used from stack overflow
    // https://stackoverflow.com/questions/33749543/unique-4-digit-random-number-in-c-sharp/33749610
    public static int GenerateRandomId()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }     
    

    public static string RandomCompanyName()
    {
        string[] companies = {"BHP Group", "Rio Tinto", "Commonwealth Bank", "CSL", "Westpac", "NAB", "ANZ", "Fortescue", "Wesfarmers", "Macquarie Group", "Woolworths", "Santos", "Telstra", "Transurban", "APA Group"};
        return companies[Faker.RandomNumber.Next(0,companies.Length)];
    }

    public static string RandomAddress()
    {
        return Faker.RandomNumber.Next(0,1000) + " " +Faker.Address.StreetName();
    }
    
    public static string RandomSuburb()
    {
        return Faker.Address.StreetName();
    }


    public static string RandomPostcode()
    {
        return Faker.RandomNumber.Next(0,5000).ToString();
    }
    
    public static string RandomPhone()
    {
        return "04" + Faker.RandomNumber.Next(0,10000000);
    }
}