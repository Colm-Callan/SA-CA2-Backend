using System.Net.Http.Json;
using Xunit;

public class AuthFlowTests : ApiTestBase
{
    public AuthFlowTests(CustomWebApplicationFactory<Program> factory)
        : base(factory)
    {
    }
    
    [Fact]
    public async Task Signup_Then_Login_Should_Succeed()
    {
        var signup = await Client.PostAsJsonAsync("api/Auth/signup", new
        {
            email = "test1@test.com",
            password = "1234"
        });

        Assert.True(signup.IsSuccessStatusCode);

        var login = await Client.PostAsJsonAsync("api/Auth/login", new
        {
            email = "test1@test.com",
            password = "1234"
        });

        Assert.True(login.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Login_With_Wrong_Password_Should_Fail()
    {
        var login = await Client.PostAsJsonAsync("api/Auth/login", new
        {
            email = "test1@test.com",
            password = "wrongpass"
        });

        Assert.False(login.IsSuccessStatusCode);
    }
}