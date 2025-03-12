using System.ComponentModel.DataAnnotations;
using travelExpense.Models;

namespace travelExpense.Utils
{
    public class BetweenDateUtils : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var travel = (Travel)validationContext.ObjectInstance;

            if (travel.StartDate.HasValue && travel.EndDate.HasValue)
            {
                if (travel.EndDate <= travel.StartDate)
                {
                    return new ValidationResult("End Date must be after Start Date.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
