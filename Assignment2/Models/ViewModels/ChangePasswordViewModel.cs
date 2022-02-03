using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models.ViewModels;

public class ChangePasswordViewModel
{
    [Required]
    [MinLength(8, ErrorMessage = "Password must be a minimum of 8 characters")]
    [MaxLength(20, ErrorMessage = "Password is too long maximum number of characters is 20")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Password must be a minimum of 8 characters")]
    [MaxLength(20, ErrorMessage = "Password is too long maximum number of characters is 20")]
    [CompareAttribute("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}