using Library.Shared;
using System.Net.Http.Json;

namespace Library.UI.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly HttpClient _httpClient;

        public BorrowService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Borrow>> GetAllBorrowAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Borrow>>("/Borrows");
        }

        public async Task<Borrow> GetBorrowAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Borrow>($"Borrows/{id}");
        }

        public async Task DeleteBorrowAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"/Borrows/{id}");
        }

        public async Task AddBorrowAsync(Borrow borrow)
        {
            await _httpClient.PostAsJsonAsync("/Borrows", borrow);
        }

        public async Task UpdateBorrowAsync(Guid id, Borrow borrow)
        {
            await _httpClient.PutAsJsonAsync($"/Borrows/{id}", borrow);
        }
    }
}
