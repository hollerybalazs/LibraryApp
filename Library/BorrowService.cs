using Library.Shared;
using Microsoft.EntityFrameworkCore;

namespace Library
{
	public class BorrowService : IBorrowService
	{
		private readonly LibraryContext _context;
		private readonly ILogger<BorrowService> _logger;

		public BorrowService(ILogger<BorrowService> logger, LibraryContext context)
		{
			_logger = logger;
			_context = context;
		}

		public async Task Add(Borrow borrow)
		{
			await _context.Borrows.AddAsync(borrow);
			await _context.SaveChangesAsync();

			_logger.LogInformation("Borrow added");
		}

		public async Task Delete(Guid id)
		{
			var borrow = await Get(id);

			_context.Borrows.Remove(borrow);

			await _context.SaveChangesAsync();
		}

		public async Task<Borrow> Get(Guid id)
		{
			var borrow = await _context.Borrows.FindAsync(id);
			_logger.LogInformation("Borrow retrieved: {@Borrow}", borrow);
			return borrow;
		}

		public async Task<List<Borrow>> GetAll()
		{
			return await _context.Borrows.ToListAsync();
		}

		public async Task Update(Borrow newBorrow)
		{
			var borrow = await Get(newBorrow.Id);
			borrow.ReaderNumber = newBorrow.ReaderNumber;
			borrow.InventoryNumber = newBorrow.InventoryNumber;
			borrow.BorrowDate = newBorrow.BorrowDate;
			borrow.OverDueDate = newBorrow.OverDueDate;
			await _context.SaveChangesAsync();
		}

		public async Task<List<Borrow>> GetBorrowReader(Guid readerNumber)
		{
			return await _context.Borrows.Where(x => x.ReaderNumber == readerNumber).ToListAsync();
		}
	}
}
