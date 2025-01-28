using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RccgWeb.Models
{
    public class SpecialProgram
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public string ProgramName { get; set; }

        [Required]
        public string ProgramTopic { get; set; }

        [Required]
        public string Preacher { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; }

        [Required]
        public int Attendance { get; set; }

        [Required]
        public string ChurchID { get; set; }

        [Required]
        [ForeignKey(nameof(ChurchID))]
        public Church Church { get; set; }
    }
}