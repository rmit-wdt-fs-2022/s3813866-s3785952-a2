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
    public int BillPayID { get; set; }
    
    [Required, ForeignKey("Account")]
    public int AccountNumber { get; set; }
    
    [Required, ForeignKey("Payee")]
    public int PayeeID { get; set; }
    
    [Required]
    public decimal Amount {get; set;}
    
    [Required]
    public DateTime ScheduleTimeUtc {get; set;}
    
    [Required]
    public Period Period {get; set;}
}