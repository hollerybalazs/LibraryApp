using System.ComponentModel.DataAnnotations;

namespace Library
{
    public class BorrowDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            DateTime date = (DateTime)value;
            if (date >= DateTime.Today)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Field must be a today's date or later.");
        }
    }
}
