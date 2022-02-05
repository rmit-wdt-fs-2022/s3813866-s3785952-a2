namespace Assignment2.DTO;

public class TransactionDTO
{
    public int AccountNumber;
    public decimal Amount;
    public string? Comment;
    public int? DestinationAccountNumber;
    public int TransactionId;
    public DateTime TransactionTimeUtc;
    public char TransactionType;
}