using RccgWeb.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RccgWeb.Models
{
    public class Workers
    {
        public Guid ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public Department Department { get; set; }

        [Required]
        public string ChurchID { get; set; }

        [Required]
        [ForeignKey(nameof(ChurchID))]
        public Church Church { get; set; }
    }
}