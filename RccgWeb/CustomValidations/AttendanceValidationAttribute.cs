using RccgWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace RccgWeb.CustomValidations
{
    public class AttendanceValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ProgramActivity activity = (ProgramActivity)validationContext.ObjectInstance;

            if (activity.ActiveWorkers > activity.Attendance)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}