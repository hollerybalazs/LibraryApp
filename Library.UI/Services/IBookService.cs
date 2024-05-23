using Library.Shared;

namespace Library.UI.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBookAsync();

        Task<Book> GetBookAsync(Guid id);

        Task DeleteBookAsync(Guid id);

        Task AddBookAsync(Book book);

        Task UpdateBookAsync(Guid id, Book book);
    }
}
