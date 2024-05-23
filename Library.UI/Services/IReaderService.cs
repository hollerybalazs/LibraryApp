using Library.Shared;

namespace Library.UI.Services
{
    public interface IReaderService
    {
        Task<IEnumerable<Reader>> GetAllReaderAsync();

        Task<Reader> GetReaderAsync(Guid id);

        Task DeleteReaderAsync(Guid id);

        Task AddReaderAsync(Reader reader);

        Task UpdateReaderAsync(Guid id, Reader reader);
    }
}
