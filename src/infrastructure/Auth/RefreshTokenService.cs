using System.Security.Cryptography;

using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure.Auth;

/// <summary>
/// Implements the IRefreshTokenService interface to manage refresh tokens for authenticated athletes.
/// </summary>
public class RefreshTokenService(
    IRefreshTokenRepository repository,
    IOptions<RefreshTokenOptions> options
) : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _repository = repository;
    private readonly RefreshTokenOptions _options = options.Value;

    /// <inheritdoc/>
    public async Task<string> IssueRefreshTokenAsync(long stravaAthleteId)
    {
        await _repository.RevokeAllActiveByStravaAthleteIdAsync(stravaAthleteId);

        var refreshToken = Generate();

        try
        {
            var hashedToken = Hash(refreshToken);
            var entity = RefreshToken.Create(stravaAthleteId, hashedToken, DateTimeOffset.UtcNow.AddDays(_options.ExpirationInDays));
            await _repository.AddAsync(entity);
        }
        catch (DbUpdateException)
        {
            throw new RefreshTokenPersistenceException();
        }

        return refreshToken;
    }

    /// <summary>
    /// Generates a secure random refresh token string. The token is generated using a cryptographically secure random number generator and is encoded in Base64 to ensure it can be safely transmitted and stored.
    /// The length of the token can be adjusted by changing the number of bytes generated, but 64 bytes (resulting in an 88-character Base64 string) is a common choice for sufficient entropy.
    /// </summary>
    /// <returns>The generated refresh token.</returns>
    private string Generate()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(randomBytes);
    }

    /// <summary>
    /// Hashes the given refresh token using SHA-256 and returns the hashed value as a Base64 string. This method is used to securely store the refresh token in the database without exposing the original token value.
    /// Hashing the token ensures that even if the database is compromised, attackers cannot easily retrieve the original token, while still allowing for verification of the token during authentication processes.
    /// </summary>
    /// <param name="token">The refresh token to be hashed.</param>
    /// <returns>The hashed refresh token as a Base64 string.</returns>
    /// <remarks> The length of the hashed token will be 44 characters for a 256-bit hash encoded in Base64 (32 bytes -> 44 characters with padding). This is sufficient for securely storing the token hash. </remarks>
    private string Hash(string token)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(hashBytes);
    }
}
