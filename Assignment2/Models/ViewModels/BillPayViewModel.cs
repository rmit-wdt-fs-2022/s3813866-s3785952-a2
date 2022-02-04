namespace Assignment2.Models.ViewModels;

public class BillPayViewModel
{
    public enum Periods
    {
        OneOff = 'O',
        Monthly = 'M',
        Fortnightly = 'F',
        Weekly = 'W',
        Daily = 'D'
    }
    
    public int PayeeId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ScheduleTimeUtc { get; set; }
    public Periods Period { get; set; }
}