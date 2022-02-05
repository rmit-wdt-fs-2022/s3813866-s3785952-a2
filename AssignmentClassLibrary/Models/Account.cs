using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AssignmentClassLibrary.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }

        [Required] [Display(Name = "Type")] public char AccountType { get; set; }

        [Required] public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [JsonIgnore]
        [InverseProperty("Account")] public virtual List<Transaction> Transactions { get; set; }
    }
}