using RccgWeb.Models;
using RccgWeb.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace RccgWeb.CustomValidations
{
    public class AttendanceValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ProgramActivityViewModel programActivity = (ProgramActivityViewModel)validationContext.ObjectInstance;

            if (programActivity.ActiveWorkers > programActivity.Attendance)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}