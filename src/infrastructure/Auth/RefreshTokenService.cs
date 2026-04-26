using System.Security.Cryptography;

using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Domain;
using Doyep.Analyzer.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure.Auth;

/// <summary>
/// Implements the IRefreshTokenService interface to manage refresh tokens for authenticated athletes.
/// </summary>
public class RefreshTokenService(
    AnalyzerDbContext _context,
    IRefreshTokenRepository _repository,
    IOptions<RefreshTokenOptions> options
) : IRefreshTokenService
{
    private readonly RefreshTokenOptions _options = options.Value;

    /// <inheritdoc/>
    public async Task<string> IssueRefreshTokenAsync(long stravaAthleteId, Guid deviceId)
    {
        var strategy = _context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            var current = await _repository.FindActiveByStravaAthleteIdAndDeviceIdAsync(stravaAthleteId, deviceId);

            var rawToken = Generate();
            var hashedToken = Hash(rawToken);
            var refreshToken = RefreshToken.Create(stravaAthleteId, deviceId, hashedToken, DateTimeOffset.UtcNow.AddDays(_options.ExpirationInDays));

            try
            {
                if (current is not null)
                {
                    current.ReplaceWith(refreshToken.Id);
                }
                await _repository.AddAsync(refreshToken);

                await _context.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch (DbUpdateException)
            {
                throw new RefreshTokenPersistenceException();
            }

            return rawToken;
        });
    }

    /// <inheritdoc/>
    public async Task TryRevokeAsync(string refreshToken)
    {
        var hashedToken = Hash(refreshToken);
        await _repository.TryRevokeAsync(hashedToken);
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
