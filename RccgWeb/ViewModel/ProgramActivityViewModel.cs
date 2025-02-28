using RccgWeb.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace RccgWeb.ViewModel
{
    public class ProgramActivityViewModel
    {
        [Required(ErrorMessage = "Name of Activity is required")]
        [Display(Name = "Name of Activity")]
        public string ActivityName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description of Activity is required")]
        [Display(Name = "Description of Activity")]
        public string ActivityDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date is required ")]
        [Display(Name = "Date")]
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Offering is required")]
        [Display(Name = "Offering")]
        public decimal Offering { get; set; }

        [Required(ErrorMessage = "Tithe is required")]
        [Display(Name = "Tithe")]
        public decimal Tithe { get; set; }

        [Required(ErrorMessage = "Attendance is required")]
        [Display(Name = "Total Attendace")]
        public int Attendance { get; set; }

        [Required(ErrorMessage = "Active Workers is required")]
        [Display(Name = "Attendace of Workers")]
        [AttendanceValidation(ErrorMessage = "Attendance of Workers can't be more than total attendance")]
        public int ActiveWorkers { get; set; }

        [Required(ErrorMessage = "Pastor in charge is required")]
        [Display(Name = "Pastor In Charge")]
        public string PastorInCharge { get; set; } = string.Empty;
    }
}