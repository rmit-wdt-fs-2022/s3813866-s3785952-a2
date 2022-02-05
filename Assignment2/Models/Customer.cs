using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assignment2.Utility;

namespace Assignment2.Models;

public class Customer
{
    [DisplayName("Customer Identification")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CustomerId { get; set; }

    [DisplayName("Full Name")]
    [ModelAttributes.IsAName("^([a-zA-Z]{2,}\\s[a-zA-Z]{1,}'?-?[a-zA-Z]{2,}\\s?([a-zA-Z]{1,})?)",
        ErrorMessage = "Name Must Have The Valid Characters of (A-Z) (a-z) (' space -)")]
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [ModelAttributes.IsAPositiveNumber(@"(^\d*\.?\d+$)", ErrorMessage = "Tax File Number must be a positive number")]
    [ModelAttributes.IsANumber(@"(^[0-9]+$)", ErrorMessage = "Tax File Number must be a number")]
    [MinLength(11, ErrorMessage = "Tax File Number cannot be less than 11 digits")]
    [MaxLength(11, ErrorMessage = "Tax File Number cannot exceed 11 digits")]
    [StringLength(11)]
    public string? TFN { get; set; }

    [ModelAttributes.IsAAddress(@"^\d+\s[A-z]+\s[A-z]+", ErrorMessage = "Address Is Not Valid")]
    [StringLength(50)]
    public string? Address { get; set; }

    [ModelAttributes.IsASuburb(@"^[a-zA-Z',.\s-]{1,25}$", ErrorMessage = "Suburb Is Not Valid")]
    [StringLength(40)]
    public string? Suburb { get; set; }

    [StringLength(3)]
    [ModelAttributes.IsAState(@"(^(VIC|NSW|NT|QLD|SA|TAS|WA|NS|QL|TS|VI))", ErrorMessage = "State Is Not Valid")]
    public string? State { get; set; }

    [ModelAttributes.IsAPostCode(
        @"^(0[289][0-9]{2})|([1345689][0-9]{3})|(2[0-8][0-9]{2})|(290[0-9])|(291[0-4])|(7[0-4][0-9]{2})|(7[8-9][0-9]{2})$",
        ErrorMessage = "Post Code is Not Valid")]
    [StringLength(4)]
    public string? PostCode { get; set; }

    [Phone]
    [ModelAttributes.IsAPhoneNumber(
        @"^(?:\+?(61))? ?(?:\((?=.*\)))?(0?[2-57-8])\)? ?(\d\d(?:[- ](?=\d{3})|(?!\d\d[- ]?\d[- ]))\d\d[- ]?\d[- ]?\d{3})$",
        ErrorMessage = "Phone number is not valid")]
    [StringLength(12)]
    public string? Mobile { get; set; }

    public virtual List<Account>? Accounts { get; set; }

    public virtual Login? Login { get; set; }
}