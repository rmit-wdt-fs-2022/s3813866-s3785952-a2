using System.ComponentModel.DataAnnotations;
using Assignment2.Models;

namespace Assignment2.ViewModels;

public class TransferViewModel
{
    public Account? CurrentAccount { get; set; }
    
    public Account? DestinationAccount { get; set; }
    
    [Required]
    public int AccountNum { get; set; }
    
    [RegularExpression(@"[0-9]*$", ErrorMessage = "Please enter a valid number ")]
    [Required]
    public int DestinationAccountNum { get; set; }
    
    [Required]
    public decimal Amount { get; set; }
    
    [StringLength(30)]
    public string? Comment { get; set; }
}