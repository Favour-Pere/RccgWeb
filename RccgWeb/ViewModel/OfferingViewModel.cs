using System.ComponentModel.DataAnnotations;

namespace RccgWeb.ViewModel
{
    public class OfferingViewModel
    {
        [Display(Name = "Enter an Amount")]
        [Required(ErrorMessage = "An Amount is required")]
        public double Amount { get; set; }

        [Display(Name = "Enter a Description")]
        [Required(ErrorMessage = "A description is required")]
        public string Description { get; set; }
    }
}