﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models;

public enum AccountType
{
    Checking = 1,
    Saving = 2
}

public class Account
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    
    [Required, Display(Name = "Account Number")]
    public int AccountNumber { get; set; }

    [Required ,Display(Name = "Type")]
    public AccountType AccountType { get; set; }

    [Required]
    public int CustomerID { get; set; }
    
    public virtual Customer Customer { get; set; }

    [Column(TypeName = "money")]
    [DataType(DataType.Currency)]
    public decimal Balance { get; set; }
    
    //[InverseProperty("Account")]
    // public virtual List<Transaction> Transactions { get; set; }
}
