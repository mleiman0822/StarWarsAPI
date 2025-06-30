using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StarWarsAPI.Data.Models
{
    public class Starship
    {
        [Key]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("manufacturer")]
        public string? Manufacturer { get; set; }

        [JsonPropertyName("cost_in_credits")]
        public string? CostInCredits { get; set; }

        [JsonPropertyName("length")]
        public string? Length { get; set; }

        [JsonPropertyName("max_atmosphering_speed")]
        public string? MaxAtmospheringSpeed { get; set; }

        [JsonPropertyName("crew")]
        public string? Crew { get; set; }

        [JsonPropertyName("passengers")]
        public string? Passengers { get; set; }

        [JsonPropertyName("cargo_capacity")]
        public string? CargoCapacity { get; set; }

        [JsonPropertyName("hyperdrive_rating")]
        public string? HyperdriveRating { get; set; }

        [JsonPropertyName("starship_class")]
        public string? StarshipClass { get; set; }
    }
}
