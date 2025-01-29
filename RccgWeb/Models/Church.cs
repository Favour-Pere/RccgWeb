using RccgWeb.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RccgWeb.Models
{
    public class Church
    {
        [Required]
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string ChurchName { get; set; }

        [Required]
        public string ChurchLocation { get; set; }
        
        [Required]
        public bool IsAssignable { get; set; }

        [Required]
        public string PastorID { get; set; }

        [ForeignKey(nameof(PastorID))]
        public Pastor Pastor { get; set; }
        
        public ICollection<Workers> Workers { get; set; }
        public ICollection<SpecialProgram> SpecialPrograms { get; set; }
        public ICollection<Offering> Offerings { get; set; }
        public ICollection<Tithe> Tithes { get; set; }
        public ICollection<ChurchTypesClosure> ChurchTypes { get; set; }
        public ICollection<ChurchAssignments>? AssignedChurches { get; set; }
    }
}