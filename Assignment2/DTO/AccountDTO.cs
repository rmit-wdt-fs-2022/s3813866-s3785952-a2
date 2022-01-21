namespace Assignment2.DTO;

public class AccountDTO
{
    public int AccountNumber { get; set; }
    public char AccountType { get; set; }
    public int CustomerId { get; set; }
    public List<TransactionDTO> Transactions { get; set; }
}