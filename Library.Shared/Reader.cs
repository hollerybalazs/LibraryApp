using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Shared
{
	class Reader
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public Guid ReaderNumber { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		[Range(typeof(DateTime), "1900-01-01", "2024-05-22")]
		public DateTime Date { get; set; }
	}
}
