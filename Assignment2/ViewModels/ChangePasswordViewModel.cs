using System.ComponentModel.DataAnnotations;
using Assignment2.Models;

namespace Assignment2.ViewModels;

public class ChangePasswordViewModel
{
    public Login Login { get; set; }
    
    [Required]
    [MinLength(8)]
    [MaxLength(20)]
    public string Password { get; set; }
    
    [Required]
    [MinLength(8)]
    [MaxLength(20)]
    public string ConfirmPassword { get; set; }
    
}