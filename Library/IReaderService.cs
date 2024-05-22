using Library.Shared;

namespace Library
{
	public interface IReaderService
	{
		Task Add(Reader reader);

		Task Delete(Guid id);

		Task<Reader> Get(Guid id);

		Task<List<Reader>> GetAll();

		Task Update(Reader reader);
	}
}

