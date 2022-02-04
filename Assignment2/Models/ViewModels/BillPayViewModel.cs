namespace Assignment2.Models.ViewModels;

public class BillPayViewModel
{
    public enum Periods
    {
        OneOff = 'O',
        Monthly = 'M',
    }
    
    public int PayeeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ScheduleTimeUtc { get; set; }
    public Periods Period { get; set; }
    public int SelectedAccountNumber { get; set; }
    public List<Payee>? Payees { get; set; }
     
    public List<BillPay>? BillPays { get; set; }
}