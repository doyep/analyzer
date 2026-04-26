using System.Net.Http.Json;

using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Application.Security;
using Doyep.Analyzer.Application.Strava;

using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Handles Strava OAuth 2.0 authentication workflows using <see cref="HttpClient"/>.
/// </summary>
public class StravaAuthenticationService(
    HttpClient httpClient,
    IStateService stateService,
    IOptions<StravaOptions> options
) : IStravaAuthenticationService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IStateService _stateService = stateService;
    private readonly StravaOptions _options = options.Value;

    /// <inheritdoc/>
    public string GenerateLoginUrl(Guid deviceId)
    {
        var state = _stateService.Create(deviceId);

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
    public async Task<StravaAuthTokenResponse> ExchangeTokenAsync(string authorizationCode)
    {
        var dto = await SendTokenRequestAsync<StravaAuthTokenResponseDto>(new()
        {
            { "client_id", _options.ClientId },
            { "client_secret", _options.ClientSecret },
            { "grant_type", StravaGrantTypes.AuthorizationCode },
            { "code", authorizationCode }
        });

        return dto.ToModel();
    }

    /// <inheritdoc/>
    public async Task<StravaRefreshTokenResponse> RefreshTokenAsync(string refreshToken)
    {
        var dto = await SendTokenRequestAsync<StravaRefreshTokenResponseDto>(new()
        {
            { "client_id", _options.ClientId },
            { "client_secret", _options.ClientSecret },
            { "grant_type", StravaGrantTypes.RefreshToken },
            { "refresh_token", refreshToken }
        });

        return dto.ToModel();
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

    /// <summary>
    /// Sends a POST request to the Strava token endpoint with the specified body parameters and returns the deserialized response.
    /// </summary>
    private async Task<T> SendTokenRequestAsync<T>(Dictionary<string, string> bodyParams)
    {
        var body = new FormUrlEncodedContent(bodyParams);

        var response = await _httpClient.PostAsync(StravaEndpoints.TokenEndpoint, body);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<T>() ?? throw new StravaAuthenticationException("Failed to parse Strava token response.");
    }
}
