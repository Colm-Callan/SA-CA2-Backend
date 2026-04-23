using System.Net.Http;
using Xunit;

public class ApiTestBase : IClassFixture<CustomWebApplicationFactory<Program>>
{
    protected readonly HttpClient Client;

    public ApiTestBase(CustomWebApplicationFactory<Program> factory)
    {
        Client = factory.CreateClient();
    }
}
// fresh db for each test