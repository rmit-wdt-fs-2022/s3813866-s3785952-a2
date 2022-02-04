using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models.ViewModels;

public class DepositWithdrawViewModel
{
    public Account? CurrentAccount { get; set; }

    [Required] public int AccountNum { get; set; }

    [Required] public decimal Amount { get; set; }

    [StringLength(30)] public string? Comment { get; set; }
}