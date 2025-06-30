using Xunit;
using Moq;
using StarWarsAPI.Data.Models;
using StarWarsAPI.Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using StarWarsAPI.Data;

public class StarshipServiceTests
{
    private StarshipService GetServiceWithInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "StarshipsDb")
        .Options;

        var context = new ApplicationDbContext(options);
        return new StarshipService(context);
    }

    [Fact]
    public void HardcodedStarshipList_ShouldContainXWing()
    {
        // Arrange
        var starships = new List<Starship>
    {
        new Starship { Name = "X-Wing", Model = "T-65", Manufacturer = "Incom Corporation", CostInCredits = "149999" },
        new Starship { Name = "TIE Fighter", Model = "Twin Ion Engine", Manufacturer = "Sienar Fleet Systems", CostInCredits = "75000" }
    };

        // Act
        var xwing = starships.Find(s => s.Name == "X-Wing");

        // Assert
        Assert.NotNull(xwing);
        Assert.Equal("T-65", xwing.Model);
    }


    [Fact]
    public async Task GetAllStarships_ShouldReturnEmpty_WhenNoData()
    {
        // Arrange
        var service = GetServiceWithInMemoryDb();

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.Empty(result);
    }
}

