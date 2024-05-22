using Library.Shared;
using Microsoft.EntityFrameworkCore;

namespace Library
{
	public class ReaderService : IReaderService
	{
		private readonly LibraryContext _context;
		private readonly ILogger<ReaderService> _logger;

		public ReaderService(ILogger<ReaderService> logger, LibraryContext context)
		{
			_logger = logger;
			_context = context;
		}

		public async Task Add(Reader reader)
		{
			await _context.Readers.AddAsync(reader);
			await _context.SaveChangesAsync();

			_logger.LogInformation("Reader added");
		}

		public async Task Delete(Guid id)
		{
			var reader = await Get(id);

			_context.Readers.Remove(reader);

			await _context.SaveChangesAsync();
		}

		public async Task<Reader> Get(Guid id)
		{
			var reader = await _context.Readers.FindAsync(id);
			_logger.LogInformation("Reader retrieved: {@Reader}", reader);
			return reader;
		}

		public async Task<List<Reader>> GetAll()
		{
			return await _context.Readers.ToListAsync();
		}

		public async Task Update(Reader newReader)
		{
			var reader = await Get(newReader.ReaderNumber);
			reader.Name = newReader.Name;
			reader.Address = newReader.Address;
			reader.Date = newReader.Date;
			await _context.SaveChangesAsync();
		}
	}
}

