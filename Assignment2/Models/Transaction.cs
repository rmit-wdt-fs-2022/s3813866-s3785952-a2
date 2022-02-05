using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models;

public enum TransactionType
{
    Deposit = 'D',
    Withdraw = 'W',
    Transfer = 'T',
    ServiceCharge = 'S',
    BillPay = 'B'
}

public class Transaction
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionId { get; set; }

    [Required] public char TransactionType { get; set; }

    [Required] [ForeignKey("Account")] public int AccountNumber { get; set; }

    public virtual Account Account { get; set; }

    [ForeignKey("DestinationAccount")] public int? DestinationAccountNumber { get; set; }

    public virtual Account? DestinationAccount { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [StringLength(30)] public string? Comment { get; set; }

    [Required] 
    [DisplayName("Transaction Time")]
    public DateTime TransactionTimeUtc { get; set; }
}