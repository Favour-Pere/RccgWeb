using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RccgWeb.ViewModel
{
    public class AreaViewModel
    {
        [Required(ErrorMessage = "Area name is required")]
        [Display(Name = "Area Name")]
        public string AreaName { get; set; }

        [Required(ErrorMessage = "Area pastor is required")]
        [Display(Name = "Area Pastor")]
        public string AreaPastor { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [Display(Name = "Location")]
        public string Location { get; set; }

        public Guid ZoneId { get; set; }

        public List<SelectListItem>? Zones { get; set; }
    }
}