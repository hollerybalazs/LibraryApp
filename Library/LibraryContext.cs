using Microsoft.EntityFrameworkCore;
using Library.Shared;

namespace Library
{
	public class LibraryContext : DbContext
	{
		public LibraryContext(DbContextOptions<LibraryContext> options)
			: base(options)
		{
		}

		public LibraryContext() { }

		public virtual DbSet<Book> Books { get; set; }

		public virtual DbSet<Reader> Readers { get; set; }

		public virtual DbSet<Borrow> Borrows { get; set; }
	}
}
