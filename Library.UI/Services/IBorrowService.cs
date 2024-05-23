using Library.Shared;

namespace Library.UI.Services
{
    public interface IBorrowService
    {
        Task<IEnumerable<Borrow>> GetAllBorrowAsync();

        Task<Borrow> GetBorrowAsync(Guid id);

        Task DeleteBorrowAsync(Guid id);

        Task AddBorrowAsync(Borrow borrow);

        Task UpdateBorrowAsync(Guid id, Borrow borrow);
    }
}
