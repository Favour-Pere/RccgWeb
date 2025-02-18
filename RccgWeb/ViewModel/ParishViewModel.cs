using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RccgWeb.ViewModel
{
    public class ParishViewModel
    {
        [Required(ErrorMessage = "Parish name is required")]
        [Display(Name = "Parish Name")]
        public string ParishName { get; set; }

        [Required(ErrorMessage = "Parish Pastor is required")]
        [Display(Name = "Parish Pastor")]
        public string ParishPastor { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [Display(Name = "Location")]
        public string Location { get; set; }

        public Guid AreaId { get; set; }

        public List<SelectListItem>? Areas { get; set; }
    }
}