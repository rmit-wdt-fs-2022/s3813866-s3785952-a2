using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentClassLibrary.Models;

public class Payee
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PayeeId { get; set; }

    [Required] [StringLength(50)] public string Name { get; set; }

    [Required] [StringLength(50)] public string Address { get; set; }

    [Required] [StringLength(40)] public string Suburb { get; set; }

    [Required] [StringLength(3)] public string State { get; set; }

    [Required] [StringLength(4)] public string Postcode { get; set; }

    [Required] [StringLength(14)] [RegularExpression(@"\(04\) \d{4}")] 
    public string Phone { get; set; }
}