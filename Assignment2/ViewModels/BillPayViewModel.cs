using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assignment2.Utility;
using AssignmentClassLibrary.Models;

namespace Assignment2.ViewModels;

public class BillPayViewModel
{

    
    public int BillPayId { get; set; }
  
    [ForeignKey("Payee")] public int PayeeId { get; set; }
    
    [ForeignKey("Account")] public int AccountNumber { get; set; }
    
    [Column(TypeName = "money")]
    [ModelAttributes.IsANumber(@"(^[0-9]+$)", ErrorMessage = "Amount must be a number")]
    public decimal Amount { get; set; }
    
    [Required]
    [DataType("datetime-local")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
    public DateTime ScheduleTimeUtc { get; set; }
    public Period Period { get; set; }
    public int SelectedAccountNumber { get; set; }
    public List<Payee>? Payees { get; set; }

    public List<BillPay>? BillPays { get; set; }
}