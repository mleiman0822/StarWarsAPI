using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Data.Models;

namespace StarWarsAPI.Data.Services
{
    public class StarshipService
    {

        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public StarshipService(ApplicationDbContext context)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://swapi.dev/api/")
            };
            _context = context;
        }

        public async Task<List<Starship>> QueryStarshipsFromSwapi()
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

        public async Task<List<Starship>> GetAllAsync()
        {
            return await _context.Starships
                .OrderByDescending(s => s.Id) 
                .ToListAsync();
        }


        public async Task AddAsync(Starship starship)
        {
            _context.Starships.Add(starship);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Starship starship)
        {
            var existing = await _context.Starships.FindAsync(starship.Id);

            if (existing == null)
                throw new Exception("Starship not found.");

            _context.Entry(existing).CurrentValues.SetValues(starship);

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var starship = await _context.Starships.FindAsync(id);
            if (starship != null)
            {
                _context.Starships.Remove(starship);
                await _context.SaveChangesAsync();
            }
        }


    }
}
