namespace Assignment2.Utility;

public static class Constants
{
    //Fees for different transaction types
    public const decimal WithdrawFee = (decimal) 0.05;
    public const decimal TransferFee = (decimal) 0.1;

    //Account minimum balances
    public const int SavingMinimumBalance = 0;
    public const int CheckingMinimumBalance = 300;

    //Account types
    public const char SavingAccType = 'S';
    public const char CheckingAccType = 'C';

    //Transaction types
    public const char Withdraw = 'W';
    public const char ServiceFee = 'S';
    public const char Deposit = 'D';
    public const char Transfer = 'T';
    public const char BillPay = 'B';
}