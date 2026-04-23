using System.Net.Http.Json;
using Xunit;

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
}