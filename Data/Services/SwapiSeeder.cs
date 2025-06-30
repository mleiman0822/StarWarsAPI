using StarWarsAPI.Data.Models;

namespace StarWarsAPI.Data.Services
{
    public class SwapiSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly StarshipService _starshipService;

        public SwapiSeeder(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _starshipService = new StarshipService(dbContext);
        }

        public async Task SeedAsync()
        {
            if (_dbContext.Starships.Any()) return;

            var starships = await _starshipService.QueryStarshipsFromSwapi();

            foreach (var ship in starships)
            {
                _dbContext.Starships.Add(new Starship
                {
                    Name = ship.Name,
                    Model = ship.Model,
                    Manufacturer = ship.Manufacturer,
                    CostInCredits = ship.CostInCredits,
                    Length = ship.Length,
                    MaxAtmospheringSpeed = ship.MaxAtmospheringSpeed,
                    Crew = ship.Crew,
                    Passengers = ship.Passengers,
                    CargoCapacity = ship.CargoCapacity,
                    HyperdriveRating = ship.HyperdriveRating,
                    StarshipClass = ship.StarshipClass
                });
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
