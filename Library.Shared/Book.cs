using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Shared
{
	public class Book
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid InventoryNumber { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Author { get; set; }

		[Required]
		public string Publisher { get; set; }

		[Required]
		[Range(typeof(DateTime), "0000-01-01", "2024-05-22")]
		public DateTime Date { get; set; }
	}
}

