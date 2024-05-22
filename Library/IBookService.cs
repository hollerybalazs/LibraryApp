using Library.Shared;

namespace Library
{
	public interface IBookService
	{
		Task Add(Book book);

		Task Delete(Guid id);

		Task<Book> Get(Guid id);

		Task<List<Book>> GetAll();

		Task Update(Book book);
	}
}
