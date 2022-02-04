namespace Assignment2.Models.ViewModels;

public class BillPayViewModel
{

    
    public int BillPayId { get; set; }
  
    public int PayeeId { get; set; }
    
    public int AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime ScheduleTimeUtc { get; set; }
    public Period Period { get; set; }
    public int SelectedAccountNumber { get; set; }
    public List<Payee>? Payees { get; set; }

    public List<BillPay>? BillPays { get; set; }
}