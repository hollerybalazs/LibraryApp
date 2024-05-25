using Library.Shared;

namespace Library
{
	public interface IBorrowService
	{
		Task Add(Borrow borrow);

		Task Delete(Guid id);

		Task<Borrow> Get(Guid id);

		Task<List<Borrow>> GetAll();

		Task Update(Borrow borrow);

        Task<List<Borrow>> GetBorrowReader(Guid readerNumber);
    }
}
