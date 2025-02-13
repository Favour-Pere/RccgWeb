using System.ComponentModel.DataAnnotations;

namespace RccgWeb.ViewModel
{
    public class ZoneViewModel
    {
        [Required]
        [Display(Name = "Name of Zone")]
        public string ZoneName { get; set; }

        [Required]
        [Display(Name = "Pastor in charge of Zone")]
        public string ZonePastor { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }
    }
}