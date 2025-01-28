using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RccgWeb.Models
{
    public class Tithe
    {
        public Guid ID { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ChurchID { get; set; }

        [Required]
        [ForeignKey(nameof(ChurchID))]
        public Church Church { get; set; }
    }
}