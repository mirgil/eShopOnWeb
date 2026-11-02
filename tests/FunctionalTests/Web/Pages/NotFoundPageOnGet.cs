using System.Net;
using Microsoft.eShopWeb.FunctionalTests.Web;
using Xunit;

namespace Microsoft.eShopWeb.FunctionalTests.WebRazorPages;

[Collection("Sequential")]
public class NotFoundPageOnGet : IClassFixture<TestApplication>
{
    private readonly HttpClient _client;

    public NotFoundPageOnGet(TestApplication factory)
    {
        _client = factory.CreateClient();
    }

    [Theory]
    [InlineData("/missing-page")]
    [InlineData("/missing-page/nested")]
    [InlineData("/missing-file.png")]
    public async Task ReturnsNotFoundWithOriginalPath(string path)
    {
        var response = await _client.GetAsync(path);
        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Contains("404 - Page not found", content);
        Assert.Contains($"<code>{path}</code>", content);
    }

    [Fact]
    public async Task AdminPageStillServesBlazorApp()
    {
        var response = await _client.GetAsync("/admin");
        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("<div id=\"admin\"></div>", content);
    }
}
