using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Xunit;
using SACA2.Models;
using SACA2.Data;
using SACA2.Services;

public class FixtureGenerationServiceTests
{
    private AppDbContext GetInMemoryDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        return new AppDbContext(options);
    }

    private async Task SeedTeamsAndPitch(AppDbContext context) 
    {
        // sets 2 teams and a pitch for tests
        context.Teams.AddRange(
            new Team { Name = "team A" },
            new Team { Name = "team B" }
        );
        context.Pitches.Add(
            new Pitch { Name = "pitch1" }
        );
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task FixtureGenerationService_CreatesFixture_WithValidData()
    {
        var context = GetInMemoryDbContext(nameof(FixtureGenerationService_CreatesFixture_WithValidData));
        await SeedTeamsAndPitch(context);

        var service = new FixtureGenerationService(context);
        var matchDate = DateTime.UtcNow.Date.AddDays(1);

        var fixture = await service.GenerateMatchForDate(matchDate);

        // assert fixture on that date for teams not being the same
        Assert.NotNull(fixture);
        Assert.Equal(matchDate.Date, fixture.MatchDate.Date);
        Assert.NotEqual(fixture.HomeTeamId, fixture.AwayTeamId);
    }

    [Fact]
    public async Task GenerateMatchForDate_ReturnsNull_WhenNotEnoughTeams()
    {
        // var context = GetInMemoryDbContext(Guid.NewGuid().ToString());
        var context = GetInMemoryDbContext(nameof(GenerateMatchForDate_ReturnsNull_WhenNotEnoughTeams));

        // only add one team
        context.Teams.Add(new Team { Name = "team A" });
        context.Pitches.Add(new Pitch { Name = "Pitch" });
        await context.SaveChangesAsync();

        var service = new FixtureGenerationService(context);
        var date = DateTime.UtcNow.Date.AddDays(2);
        
        var fixture = await service.GenerateMatchForDate(date);
        // should null since 1 team
        Assert.Null(fixture);
    }

    [Fact]
    public async Task GenerateMatchForDate_ReturnsNull_WhenNoPitches()
    {
        var context = GetInMemoryDbContext(nameof(GenerateMatchForDate_ReturnsNull_WhenNoPitches));

        // two teams, but no pitches
        context.Teams.AddRange(
            new Team { Name = "team A" },
            new Team { Name = "team B" }
        );
        await context.SaveChangesAsync();

        var service = new FixtureGenerationService(context);
        var date = DateTime.UtcNow.Date.AddDays(3);

        var fixture = await service.GenerateMatchForDate(date);
        // should null no pitch added
        Assert.Null(fixture);
    }

    [Fact]
    public async Task GenerateMatchForDate_ReturnsNull_IfMatchAlreadyExists()
    {
        var context = GetInMemoryDbContext(nameof(GenerateMatchForDate_ReturnsNull_IfMatchAlreadyExists));

        await SeedTeamsAndPitch(context);
        var service = new FixtureGenerationService(context);

        var date = DateTime.UtcNow.Date.AddDays(5);
        var fixture1 = await service.GenerateMatchForDate(date);
        var fixture2 = await service.GenerateMatchForDate(date);

        Assert.NotNull(fixture1);
        Assert.Null(fixture2); // expect null since already made
    }

    [Fact]
    public async Task GenerateFixturesForDays_CreatesFixtures_ForMultipleDays()
    {
        var context = GetInMemoryDbContext(nameof(GenerateFixturesForDays_CreatesFixtures_ForMultipleDays));
        await SeedTeamsAndPitch(context);
        var service = new FixtureGenerationService(context);
        var fixtures = await service.GenerateFixturesForDays(4);

        Assert.NotNull(fixtures);
        Assert.Equal(4, fixtures.Count);
    }
}