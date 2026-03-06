using Doyep.Analyzer.Application.Strava;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure.Strava;

public class StravaService : IStravaService
{
    private readonly HttpClient _httpClient;
    private readonly StravaOptions _options;

    public StravaService(HttpClient httpClient, IOptions<StravaOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<string> ExchangeToken(string authorizationCode)
    {
        var values = new Dictionary<string, string>
        {
            { "client_id", _options.ClientId },
            { "client_secret", _options.ClientSecret },
            { "code", authorizationCode },
            { "grant_type", "authorization_code" }
        };
        var content = new FormUrlEncodedContent(values);

        var response = await _httpClient.PostAsync("/oauth/token", content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}
