namespace Assignment2.DTO;

public class TransactionDTO
{
    public int TransactionId;
    public char TransactionType;
    public int AccountNumber;
    public int? DestinationAccountNumber;
    public decimal Amount;
    public string? Comment;
    public DateTime TransactionTimeUtc;
}