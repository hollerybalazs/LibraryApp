using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Shared
{
	public class Borrow
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Required]
		[ForeignKey("Reader")]
		public Guid ReaderNumber { get; set; }

		[Required]
		[ForeignKey("Book")]
		public Guid InventoryNumber { get; set; }

		[Required]
		[BorrowDateValidation]
		public DateTime BorrowDate { get; set; }

		[Required]
        public DateTime OverDueDate { get; set; }
    }
}
