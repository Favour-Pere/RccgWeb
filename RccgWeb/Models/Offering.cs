using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RccgWeb.Models
{
    public class Offering
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ChurchId { get; set; }

        [ForeignKey(nameof(ChurchId))]
        [Required]
        public Church Church { get; set; }
    }
}