using Library.Shared;
using System.Net.Http.Json;

namespace Library.UI.Services
{
    public class ReaderService : IReaderService
    {
        private readonly HttpClient _httpClient;

        public ReaderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Reader>> GetAllReaderAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Reader>>("/Readers");
        }

        public async Task<Reader> GetReaderAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Reader>($"Readers/{id}");
        }

        public async Task DeleteReaderAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"/Readers/{id}");
        }

        public async Task AddReaderAsync(Reader reader)
        {
            await _httpClient.PostAsJsonAsync("/Books", reader);
        }

        public async Task UpdateReaderAsync(Guid id, Reader reader)
        {
            await _httpClient.PutAsJsonAsync($"/Readers/{id}", reader);
        }
    }
}
