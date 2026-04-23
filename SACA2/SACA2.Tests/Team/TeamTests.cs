using System.Net.Http.Json;
using Xunit;
using SACA2.Models;


public class TeamTests : ApiTestBase
{
    public TeamTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task Create_Team_Should_Succeed()
    {
        var response = await Client.PostAsJsonAsync("api/Team", new
        {
            name = "Arsenal",
            wins = 0,
            draws = 0,
            losses = 0,
            points = 0
        });

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Duplicate_Team_Should_Be_Rejected()
    {
        await Client.PostAsJsonAsync("api/Team", new
        {
            name = "Chelsea",
            wins = 0,
            draws = 0,
            losses = 0,
            points = 0
        });

        var duplicate = await Client.PostAsJsonAsync("api/Team", new
        {
            name = "Chelsea",
            wins = 0,
            draws = 0,
            losses = 0,
            points = 0
        });

        // should get 409 error not success
        Assert.False(duplicate.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Get_Teams_Should_Return_Data()
    {
        var response = await Client.GetAsync("api/Team");

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Update_Team()
    {
        var createResponse = await Client.PostAsJsonAsync("api/Team", new
        {
            name = "abc",
            wins = 1,
            draws = 2,
            losses = 3,
            points = 4
        });
        createResponse.EnsureSuccessStatusCode();

        var createdTeam = await createResponse.Content.ReadFromJsonAsync<Team>();
        Assert.NotNull(createdTeam);

        var updatedTeamData = new
        {
            name = "abcd",
            wins = 10,
            draws = 5,
            losses = 2,
            points = 35
        };

        var updateResponse = await Client.PutAsJsonAsync($"api/Team/{createdTeam.Id}", updatedTeamData);
        updateResponse.EnsureSuccessStatusCode();

        var updatedTeam = await updateResponse.Content.ReadFromJsonAsync<Team>();
        Assert.NotNull(updatedTeam);

        // check expected new val
        Assert.Equal("abcd", updatedTeam.Name);
        Assert.Equal(10, updatedTeam.Wins);
        Assert.Equal(5, updatedTeam.Draws);
        Assert.Equal(2, updatedTeam.Losses);
        Assert.Equal(35, updatedTeam.Points);

    }
}