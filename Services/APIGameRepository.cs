// This class may be unnecessary
using Micro;
using System.Text.Json;

namespace GameMicroServer.Services
{
    public class APIGameRepository : IGameRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public APIGameRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ICollection<GameInfo>> ReadAllAsync()
        {
            var client = _httpClientFactory.CreateClient("GameAPI");
            List<GameInfo> games = null;

            var response = await client.GetAsync("all");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                games = JsonSerializer.Deserialize<List<GameInfo>>(json, serializeOptions);
            }

            games ??= new List<GameInfo>(); // if games is null, create an empty list

            return games;
        }
        public async Task<GameInfo> GetByIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("GameAPI");
            GameInfo game = null;

            var response = await client.GetAsync($"game/{id}"); // Adjust the endpoint to accept ID

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                game = JsonSerializer.Deserialize<GameInfo>(json, serializeOptions);
            }

            return game;
        }
    }
}
