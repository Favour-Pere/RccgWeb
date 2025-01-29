using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RccgWeb.Models;

public class ChurchAssignments
{
    [Required]
    [Key]
    public Guid ID { get; set; }
    
    [Required]
    public Guid ParentChurchId { get; set; }
    
    [ForeignKey(nameof(ParentChurchId))]
    public Church ParentChurch { get; set; }
    
    [Required]
    public Guid ChildChurchId { get; set; }
    
    [ForeignKey(nameof(ChildChurchId))]
    public Church ChildChurch { get; set; }
    
}