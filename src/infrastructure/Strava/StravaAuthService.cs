using System.Net.Http.Json;
using System.Text.Json;

using Doyep.Analyzer.Application;
using Doyep.Analyzer.Application.Strava;

using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Handles Strava OAuth 2.0 authentication workflows using <see cref="HttpClient"/>.
/// </summary>
public class StravaAuthService(
    HttpClient httpClient,
    IOptions<StravaOptions> options
) : IStravaAuthService
{
    private readonly StravaOptions _options = options.Value;

    /// <inheritdoc/>
    public Uri GenerateLoginUrl(string state, Uri callbackUri)
    {
        var queries = new Dictionary<string, string?>
        {
            { "client_id", _options.ClientId },
            { "redirect_uri", callbackUri.ToString() },
            { "response_type", StravaResponseTypes.Code },
            { "approval_prompt", StravaApprovalPrompts.Force },
            { "scope", StravaAuthorizationScopes.JoinRequiredScopes() },
            { "state", state }
        };

        var baseUri = new Uri(new Uri(StravaEndpoints.BaseUrl), StravaEndpoints.AuthorizeEndpoint);

        var urlWithQuery = QueryHelpers.AddQueryString(baseUri.ToString(), queries);

        return new Uri(urlWithQuery);
    }

    /// <inheritdoc/>
    public async Task<Result<StravaAuthTokenResponse, Error>> ExchangeTokenAsync(string authorizationCode)
    {
        var tokenResult = await PostTokenRequestAsync<StravaAuthTokenResponseDto>(new()
        {
            { "client_id", _options.ClientId },
            { "client_secret", _options.ClientSecret },
            { "grant_type", StravaGrantTypes.AuthorizationCode },
            { "code", authorizationCode }
        });

        return tokenResult.IsFailure
            ? Result<StravaAuthTokenResponse, Error>.Failure(tokenResult.Error)
            : Result<StravaAuthTokenResponse, Error>.Success(tokenResult.Value.ToModel());
    }

    /// <inheritdoc/>
    public async Task<Result<StravaRefreshTokenResponse, Error>> RefreshTokenAsync(string refreshToken)
    {
        var tokenResult = await PostTokenRequestAsync<StravaRefreshTokenResponseDto>(new()
        {
            { "client_id", _options.ClientId },
            { "client_secret", _options.ClientSecret },
            { "grant_type", StravaGrantTypes.RefreshToken },
            { "refresh_token", refreshToken }
        });

        return tokenResult.IsFailure
            ? Result<StravaRefreshTokenResponse, Error>.Failure(tokenResult.Error)
            : Result<StravaRefreshTokenResponse, Error>.Success(tokenResult.Value.ToModel());
    }

    /// <inheritdoc/>
    public async Task<Result<Error>> Deauthorize(string accessToken)
    {
        var queries = new Dictionary<string, string?>
        {
            { "access_token", accessToken }
        };
        var urlWithQuery = QueryHelpers.AddQueryString(StravaEndpoints.DeauthorizeEndpoint, queries);

        try
        {
            var response = await httpClient.PostAsync(urlWithQuery, null);

            if (!response.IsSuccessStatusCode)
            {
                // TODO: Logger
                // var errorContent = await response.Content.ReadAsStringAsync();
                return Result<Error>.Failure(StravaErrors.DeauthorizationFailed);
            }

            return Result<Error>.Success();
        }
        catch (HttpRequestException)
        {
            return Result<Error>.Failure(StravaErrors.DeauthorizationFailed);
        }

    }

    /// <summary>
    /// Sends a POST request to Strava's token endpoint with the specified body parameters to exchange an authorization code for tokens or refresh an access token.
    /// Handles the HTTP response, returning a Result containing either the deserialized token response or an error if the request fails or the response is invalid.
    /// </summary>
    /// <typeparam name="T">The type of the token response DTO.</typeparam>
    /// <param name="bodyParams">The body parameters for the token request.</param>
    /// <returns>A Result containing either the deserialized token response or an error.</returns>
    private async Task<Result<T, Error>> PostTokenRequestAsync<T>(Dictionary<string, string> bodyParams)
    {
        var body = new FormUrlEncodedContent(bodyParams);

        try
        {
            var response = await httpClient.PostAsync(StravaEndpoints.TokenEndpoint, body);

            if (!response.IsSuccessStatusCode)
            {
                // TODO: Logger
                // var errorContent = await response.Content.ReadAsStringAsync();
                return Result<T, Error>.Failure(StravaErrors.TokenRequestFailed);
            }

            var dto = await response.Content.ReadFromJsonAsync<T>();

            return dto is null
                ? Result<T, Error>.Failure(StravaErrors.TokenRequestFailed)
                : Result<T, Error>.Success(dto);
        }
        catch (HttpRequestException)
        {
            return Result<T, Error>.Failure(StravaErrors.TokenRequestFailed);
        }
        catch (JsonException)
        {
            return Result<T, Error>.Failure(StravaErrors.TokenRequestFailed);
        }
    }
}
