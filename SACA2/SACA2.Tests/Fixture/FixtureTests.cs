using System.Net.Http.Json;
using Xunit;

public class FixtureTests : ApiTestBase
{
    public FixtureTests(CustomWebApplicationFactory<Program> factory)
        : base(factory)
    {
    }
    [Fact]
    public async Task Create_Fixture_Should_Succeed()
    {
        var response = await Client.PostAsJsonAsync("api/Fixture", new
        {
            homeTeamId = 1,
            awayTeamId = 2,
            matchDate = System.DateTime.UtcNow
        });

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Get_Fixtures_Should_Return_List()
    {
        var response = await Client.GetAsync("api/Fixture");

        Assert.True(response.IsSuccessStatusCode);
    }
}