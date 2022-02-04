using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models;

public enum Period
{
    OneOff = 'O',
    Monthly = 'M'
}

public class BillPay
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BillPayId { get; set; }

    [Required] [ForeignKey("Account")] public int AccountNumber { get; set; }

    [Required] [ForeignKey("Payee")] public int PayeeId { get; set; }

    [Required]
    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [Required] public DateTime ScheduleTimeUtc { get; set; }

    [Required] public Period Period { get; set; }
}