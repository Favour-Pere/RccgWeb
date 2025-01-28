using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RccgWeb.Models
{
    public class Pastor
    {
        [Required]
        public Guid ID { get; set; }

        [ForeignKey("Id")] public UserModel? User { get; set; }
    }
}