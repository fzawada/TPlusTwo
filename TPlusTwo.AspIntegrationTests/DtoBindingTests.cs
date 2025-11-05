using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace TPlusTwo.AspIntegrationTests;

[TestFixture]
public class DtoBindingTests
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.UseUrls("http://localhost:7777"));
        _client = _factory.CreateClient();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task CreateRepoTrade_WithValidJson_Returns200()
    {
        var serializedPayload = """
        {
          "tradeDate": "2025-11-05",
          "settlementDate": "2025-11-07",
          "nominal": 100,
          "instrument": "string"
        }
        """;

        var resp = await PostCreateRepoTrade(serializedPayload);

        Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var responseContent = await resp.Content.ReadAsStringAsync();
        Assert.That(responseContent.Length, Is.EqualTo(0));
    }

    [Test]
    public async Task CreateRepoTrade_WithMissingRequiredMember_Returns400_AndErrorMessage()
    {
        var serializedPayloadWithMissingNominal = """
        {
          "tradeDate": "2025-11-05",
          "settlementDate": "2025-11-05",
          "instrument": "string"
        }
        """;
        var resp = await PostCreateRepoTrade(serializedPayloadWithMissingNominal);

        Assert.That(resp.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        var p = await resp.Content.ReadAsStringAsync();
        var problem = await resp.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        Assert.That(problem, Is.Not.Null);
        Assert.That(problem!.Errors["$"], Has.Some.Contains("nominal"));
        Assert.That(problem!.Errors["$"], Has.Some.Contains("missing required"));
    }

    [Test]
    public async Task CreateRepoTradet_WithNullForNonNullableRefType_Returns400()
    {
        Assert.Inconclusive();
    }

    [Test]
    public async Task CreateRepoTradet_WithNullForNonNullableValueType_Returns400()
    {
        Assert.Inconclusive();
    }

    //TODO: test that Vogen validation plays well with ASP.NET Core model binding

    private Task<HttpResponseMessage> PostCreateRepoTrade(
        string serializedPayloadWithMissingNominal) =>
        _client.PostAsync(
            "/repotrades/createrepotrade",
            new StringContent(
                serializedPayloadWithMissingNominal,
                System.Text.Encoding.UTF8,
                "application/json"));

}
