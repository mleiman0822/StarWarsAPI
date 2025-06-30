using StarWarsAPI.Data.Models;

namespace StarWarsAPI.Data.Services
{
    public class StarshipService
    {

        private readonly HttpClient _httpClient;

        public StarshipService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://swapi.dev/api/")
            };
        }

        public async Task<List<Starship>> GetAllStarshipsAsync()
        {
            var allStarships = new List<Starship>();
            string? nextUrl = "starships";

            while (!string.IsNullOrEmpty(nextUrl))
            {
                var response = await _httpClient.GetFromJsonAsync<StarshipAPIResponse>(nextUrl);
                if (response == null || response.Results == null) break;

                allStarships.AddRange(response.Results);
                nextUrl = response.Next?.Replace("http://", "https://")?.Replace("https://swapi.dev/api/", "");
            }

            return allStarships;
        }
    }
}
