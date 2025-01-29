using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RccgWeb.Models;

public class ChurchTypes
{
    [Key]
    [Required]
    public Guid ID { get; set; }
    
    [Required]
    public string Type { get; set; }
}