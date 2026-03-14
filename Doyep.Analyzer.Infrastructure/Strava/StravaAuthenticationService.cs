using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

using Doyep.Analyzer.Application.Strava;

namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Handles Strava OAuth 2.0 authentication workflows using <see cref="HttpClient"/>.
/// </summary>
public class StravaAuthenticationService : IStravaAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpClient _httpClient;
    private readonly StravaOptions _options;

    public StravaAuthenticationService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<StravaOptions> options)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _options = options.Value;
    }

    /// <inheritdoc/>
    public string GenerateLoginUrl()
    {
        var request = _httpContextAccessor.HttpContext.Request;
        var queries = new Dictionary<string, string?>
        {
            { "client_id", _options.ClientId },
            { "redirect_uri", $"{request.Scheme}://{request.Host}/auth/callback" },
            { "response_type", "code" },
            { "approval_prompt", "force" },
            { "scope", "read,read_all,profile:read_all,activity:read_all" },
        };

        var relative = QueryHelpers.AddQueryString("oauth/authorize", queries);

        var uri = new Uri(_httpClient.BaseAddress!, relative);

        return uri.ToString();
    }

    /// <inheritdoc/>
    public async Task<StravaTokenResponse?> ExchangeToken(string authorizationCode)
    {
        var body = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", _options.ClientId },
            { "client_secret", _options.ClientSecret },
            { "code", authorizationCode },
            { "grant_type", "authorization_code" }
        });

        var response = await _httpClient.PostAsync("/oauth/token", body);
        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadFromJsonAsync<StravaTokenResponseDto>();
        return dto!.ToModel();
    }

    /// <inheritdoc/>
    public async Task<StravaTokenResponse?> RefreshToken(string refreshToken)
    {
        var body = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", _options.ClientId },
            { "client_secret", _options.ClientSecret },
            { "code", refreshToken },
            { "grant_type", "refresh_token" }
        });

        var response = await _httpClient.PostAsync("/oauth/token", body);
        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadFromJsonAsync<StravaTokenResponseDto>();
        return dto!.ToModel();
    }

    /// <inheritdoc/>
    public async Task Deauthorize(string accessToken)
    {
        var queries = new Dictionary<string, string?>
        {
            { "access_token", accessToken }
        };

        var url = QueryHelpers.AddQueryString("oauth/deauthorize", queries);

        var response = await _httpClient.PostAsync(url, null);

        response.EnsureSuccessStatusCode();
    }
}
