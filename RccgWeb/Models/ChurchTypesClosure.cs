using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RccgWeb.Models;

public class ChurchTypesClosure
{
    [Key]
    public Guid ID { get; set; } 
    public Guid ChurchId { get; set; }
    [ForeignKey(nameof(ChurchId))]
    public Church Church { get; set; }
    
    public Guid ChurchTypeId { get; set; }
    [ForeignKey(nameof(ChurchTypeId))]
    public ChurchTypes ChurchType { get; set; }
}