using System.Net.Http.Json;
using Library.Shared;

namespace Library.UI.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Book>> GetAllBookAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Book>>("/Books");
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Book>($"Books/{id}");
        }

        public async Task DeleteBookAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"/Books/{id}");
        }

        public async Task AddBookAsync(Book book)
        {
            await _httpClient.PostAsJsonAsync("/Books", book);
        }

        public async Task UpdateBookAsync(Guid id, Book book)
        {
            await _httpClient.PutAsJsonAsync($"/Books/{id}", book);
        }
    }
}
