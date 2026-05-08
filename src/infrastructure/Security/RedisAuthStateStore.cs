using System.Text.Json;

using Doyep.Analyzer.Application;
using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Application.Security;

using Microsoft.Extensions.Caching.Distributed;

namespace Doyep.Analyzer.Infrastructure.Security;

/// <summary>
/// Implements the IAuthStateStore interface using a distributed cache (e.g., Redis) to manage authentication
/// state values during the OAuth authentication process.
/// </summary>
public class RedisAuthStateStore(IDistributedCache cache) : IAuthStateStore
{
    private static readonly TimeSpan Expiration = TimeSpan.FromMinutes(5);

    /// <inheritdoc/>
    public async Task SetAsync(string state, AuthState value, CancellationToken cancellationToken)
    {
        var key = BuildCacheKey(state);

        var json = JsonSerializer.Serialize(value);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = Expiration
        };

        await cache.SetStringAsync(key, json, options, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Result<AuthState, Error>> GetAsync(string state, CancellationToken cancellationToken)
    {
        var key = BuildCacheKey(state);

        var json = await cache.GetStringAsync(key, cancellationToken);

        if (json is null)
        {
            return Result<AuthState, Error>.Failure(AuthStateErrors.NotFound);
        }

        var value = JsonSerializer.Deserialize<AuthState>(json);

        if (value is null)
        {
            return Result<AuthState, Error>.Failure(AuthStateErrors.NotFound);
        }

        return Result<AuthState, Error>.Success(value);
    }

    /// <inheritdoc/>
    public Task RemoveAsync(string state, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Generates a cache key for storing the authentication state value based on the provided state token.
    /// The key is prefixed with "auth:state:" to create a unique namespace for authentication state values
    /// in the cache.
    /// </summary>
    /// <param name="state">The state token associated with the authentication request.</param>
    /// <returns>The cache key for the authentication state value.</returns>
    private static string BuildCacheKey(string state) => $"auth:state:{state}";
}
