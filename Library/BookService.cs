using Library.Shared;
using Microsoft.EntityFrameworkCore;

namespace Library
{
	public class BookService : IBookService
	{
		private readonly LibraryContext _context;
		private readonly ILogger<BookService> _logger;

		public BookService(ILogger<BookService> logger, LibraryContext context)
		{
			_logger = logger;
			_context = context;
		}

		public async Task Add(Book book)
		{
			await _context.Books.AddAsync(book);
			await _context.SaveChangesAsync();

			_logger.LogInformation("Book added");
		}

		public async Task Delete(Guid id)
		{
			var book = await Get(id);

			_context.Books.Remove(book);

			await _context.SaveChangesAsync();
		}

		public async Task<Book> Get(Guid id)
		{
			var book = await _context.Books.FindAsync(id);
			_logger.LogInformation("Book retrieved: {@Book}", book);
			return book;
		}

		public async Task<List<Book>> GetAll()
		{
			return await _context.Books.ToListAsync();
		}

		public async Task Update(Book newBook)
		{
			var book = await Get(newBook.InventoryNumber);
			book.Title = newBook.Title;
			book.Author = newBook.Author;
			book.Publisher = newBook.Publisher;
			book.Date = newBook.Date;

			await _context.SaveChangesAsync();
		}
	}
}
