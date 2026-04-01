using System.Net.Http.Json;

using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Application.Strava;

using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Handles Strava OAuth 2.0 authentication workflows using <see cref="HttpClient"/>.
/// </summary>
public class StravaAuthenticationService : IStravaAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly StravaOptions _options;

    public StravaAuthenticationService(HttpClient httpClient, IOptions<StravaOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    /// <inheritdoc/>
    public string GenerateLoginUrl(string state)
    {
        var queries = new Dictionary<string, string?>
        {
            { "client_id", _options.ClientId },
            { "redirect_uri", _options.RedirectUri },
            { "response_type", StravaResponseTypes.Code },
            { "approval_prompt", StravaApprovalPrompts.Force },
            { "scope", string.Join(",", ScopeValidator.RequiredScopes) },
            { "state", state }
        };

        var baseUrl = new Uri(StravaEndpoints.BaseUrl);
        var authorizeUrl = new Uri(baseUrl, StravaEndpoints.AuthorizeEndpoint);

        return QueryHelpers.AddQueryString(authorizeUrl.ToString(), queries);
    }

    /// <inheritdoc/>
    public async Task<StravaTokenResponse?> ExchangeToken(string authorizationCode)
    {
        var body = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", _options.ClientId },
            { "client_secret", _options.ClientSecret },
            { "grant_type", StravaGrantTypes.AuthorizationCode },
            { "code", authorizationCode }
        });

        var response = await _httpClient.PostAsync(StravaEndpoints.TokenEndpoint, body);
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
            { "grant_type", StravaGrantTypes.RefreshToken },
            { "refresh_token", refreshToken }
        });

        var response = await _httpClient.PostAsync(StravaEndpoints.TokenEndpoint, body);
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
