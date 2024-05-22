using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Shared
{
	public class Borrow //: ValidationAttribute
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		[ForeignKey("Reader")]
		public int ReaderNumber { get; set; }

		[Required]
		[ForeignKey("Book")]
		public int InventoryNumber { get; set; }

		[Required]
		//[CustomValidation(typeof(Borrow), nameof(BorrowDateValidation))]
		public DateTime BorrowDate { get; set; }

		[Required]
		//[CustomValidation(typeof(Borrow), nameof(OverDueDateValidation))]
		public DateTime OverDueDate { get; set; }

		/*
		public virtual ValidationResult BorrowDateValidation(ValidationContext context)
		{
			if(BorrowDate >= DateTime.Today)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("The field must be a date today or in the future.");
		}

		public virtual ValidationResult OverDueDateValidation(ValidationContext context)
		{
			if (OverDueDate > BorrowDate)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("Overdue date must be after the borrow date.");
		}*/
	}
}
