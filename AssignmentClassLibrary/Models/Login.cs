using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentClassLibrary.Models;

public class Login
{
    [Column(TypeName = "char")]
    [DisplayName("Login Identification")]
    [StringLength(8)]
    public string LoginId { get; set; }

    [Required] [ForeignKey("Customer")] public int CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    [Column(TypeName = "char")]
    [Required]
    [StringLength(64)]
    public string PasswordHash { get; set; }
}