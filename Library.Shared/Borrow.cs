using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Shared
{
	class Borrow : ValidationAttribute
	{
		[Required]
		[ForeignKey("Reader")]
		public int ReaderNumber { get; set; }

		[Required]
		[ForeignKey("Book")]
		public int BorrowNumber { get; set; }

		[Required]
		[CustomValidation(typeof(Borrow), nameof(BorrowDateValidation))]
		public DateTime BorrowDate { get; set; }

		[Required]
		
		public DateTime OverDueDate { get; set; }
		[CustomValidation(typeof(Borrow), nameof(OverDueDateValidation))]

		public ValidationResult BorrowDateValidation(ValidationContext context)
		{
			if(BorrowDate >= DateTime.Today)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("The field must be a date today or in the future.");
		}

		public ValidationResult OverDueDateValidation(ValidationContext context)
		{
			if (OverDueDate > BorrowDate)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("Overdue date must be after the borrow date.");
		}
	}
}
