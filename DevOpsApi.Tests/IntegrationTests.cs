using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;

namespace DevOpsApi.Tests;

public class IntegrationTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public IntegrationTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_DevOps_Should_Return_OK()
    {
        var request = new
        {
            message = "This is a test",
            to = "Juan Perez",
            from = "Rita Asturia",
            timeToLifeSec = 45
        };

        _client.DefaultRequestHeaders.Add(
            "X-Parse-REST-API-Key",
            "2f5ae96c-b558-4c7b-a590-a501ae1c3f6c");

        var response =
            await _client.PostAsJsonAsync(
                "/DevOps",
                request);

        Assert.Equal(HttpStatusCode.OK,
            response.StatusCode);
    }

    [Fact]
    public async Task Post_Without_ApiKey_Should_Return_401()
    {
        var request = new
        {
            message = "test",
            to = "Juan",
            from = "Rita",
            timeToLifeSec = 45
        };

        var response =
            await _client.PostAsJsonAsync("/DevOps", request);

        Assert.Equal(HttpStatusCode.Unauthorized,
            response.StatusCode);
    }

    [Fact]
    public async Task Post_With_Invalid_ApiKey_Should_Return_401()
    {
        var request = new
        {
            message = "test",
            to = "Juan",
            from = "Rita",
            timeToLifeSec = 45
        };

        _client.DefaultRequestHeaders.Add(
            "X-Parse-REST-API-Key",
            "wrong-key");

        var response =
            await _client.PostAsJsonAsync("/DevOps", request);

        Assert.Equal(HttpStatusCode.Unauthorized,
            response.StatusCode);
    }

    [Fact]
    public async Task Get_Should_Return_Error()
    {
        _client.DefaultRequestHeaders.Add(
            "X-Parse-REST-API-Key",
            "2f5ae96c-b558-4c7b-a590-a501ae1c3f6c");

        var response =
            await _client.GetAsync("/DevOps");

        Assert.Equal(HttpStatusCode.BadRequest,
            response.StatusCode);
    }
}