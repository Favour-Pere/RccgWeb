using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RccgWeb.Models
{
    public class ProgramActivity
    {
        [Key]
        public Guid ProgramActivityId { get; set; }

        [Required]
        public string ActivityName { get; set; }

        [Required]
        public string ActivityDescription { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateTimeSubmitted { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Offering { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Tithe { get; set; }

        public int Attendance { get; set; }

        public int ActiveWorkers { get; set; }

        [Required]
        public string PastorInCharge { get; set; }

        [Required]
        public string ChurchId { get; set; }

        [ForeignKey("Zone")]
        public Guid? ZoneId { get; set; }

        public Zone Zone { get; set; }

        [ForeignKey("Area")]
        public Guid? AreaId { get; set; }

        public Area Area { get; set; }

        [ForeignKey("Parish")]
        public Guid? ParishId { get; set; }

        public Parish Parish { get; set; }
    }
}