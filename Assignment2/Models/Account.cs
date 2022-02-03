using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models;

// public enum AccountType
// {
//     Checking = 'C',
//     Saving = 'S'
// }

public class Account
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    
    [Required, Display(Name = "Account Number")]
    public int AccountNumber { get; set; }

    [Required ,Display(Name = "Type")]
    public char AccountType { get; set; }

    [Required]
    public int CustomerId { get; set; }
    
    public virtual Customer Customer { get; set; }
    
    [InverseProperty("Account")]
     public virtual List<Transaction> Transactions { get; set; }
     
}
