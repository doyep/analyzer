using System.Security.Cryptography;

using Doyep.Analyzer.Application;
using Doyep.Analyzer.Application.Athletes;
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
    AnalyzerDbContext context,
    IAthleteRepository athleteRepository,
    IOptions<RefreshTokenOptions> options,
    IRefreshTokenRepository refreshTokenRepository
) : IRefreshTokenService
{
    private readonly RefreshTokenOptions _options = options.Value;

    /// <inheritdoc/>
    public async Task<Result<string, Error>> IssueRefreshTokenAsync(long stravaAthleteId, Guid deviceId)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            using var tx = await context.Database.BeginTransactionAsync();

            var currentActiveToken = await refreshTokenRepository.FindActiveByStravaAthleteIdAndDeviceIdAsync(stravaAthleteId, deviceId);
            currentActiveToken?.Revoke();

            var token = Generate();
            var hashedToken = Hash(token);
            var refreshToken = RefreshToken.Create(
                stravaAthleteId,
                deviceId,
                hashedToken,
                DateTimeOffset.UtcNow.AddDays(_options.ExpirationInDays)
            );

            try
            {
                await refreshTokenRepository.AddAsync(refreshToken);
                await tx.CommitAsync();
            }
            catch (RefreshTokenPersistenceException)
            {
                return Result<string, Error>.Failure(RefreshTokenErrors.FailedToPersist);
            }

            return Result<string, Error>.Success(token);
        });
    }

    /// <inheritdoc/>
    public async Task<Result<RefreshResult, Error>> RefreshAsync(string token)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            using var tx = await context.Database.BeginTransactionAsync();

            var hashedToken = Hash(token);
            var currentToken = await refreshTokenRepository.FindByHashedTokenAsync(hashedToken);

            if (currentToken is null)
                return Result<RefreshResult, Error>.Failure(RefreshTokenErrors.InvalidToken);

            if (currentToken.RevokedAt is not null)
            {
                if (currentToken.ReplacedByTokenId is not null)
                {
                    await refreshTokenRepository.RevokeByStravaAthleteIdAndDeviceIdAsync(currentToken.StravaAthleteId, currentToken.DeviceId);
                    await tx.CommitAsync();

                    return Result<RefreshResult, Error>.Failure(RefreshTokenErrors.ReuseDetected);
                }

                return Result<RefreshResult, Error>.Failure(RefreshTokenErrors.RevokedToken);
            }

            if (currentToken.IsExpired)
                return Result<RefreshResult, Error>.Failure(RefreshTokenErrors.ExpiredToken);

            var newRawToken = Generate();
            var newHashedToken = Hash(newRawToken);
            var newRefreshToken = RefreshToken.Create(
                currentToken.StravaAthleteId,
                currentToken.DeviceId,
                newHashedToken,
                DateTimeOffset.UtcNow.AddDays(_options.ExpirationInDays)
            );
            currentToken.ReplaceWith(newRefreshToken.Id);

            await refreshTokenRepository.AddAsync(newRefreshToken);
            await tx.CommitAsync();

            // TODO: can we do better than fetching the athlete again here?
            var athlete = await athleteRepository.FindByStravaAthleteIdAsync(currentToken.StravaAthleteId);

            return Result<RefreshResult, Error>.Success(new RefreshResult(athlete!, newRawToken));
        });
    }

    /// <inheritdoc/>
    public async Task<Result<Error>> RevokeAsync(string refreshToken)
    {
        var hashedToken = Hash(refreshToken);
        await refreshTokenRepository.RevokeAsync(hashedToken);

        return Result<Error>.Success();
    }

    /// <summary>
    /// Generates a secure random refresh token string. The token is generated using a cryptographically secure random number generator and is encoded in Base64 to ensure it can be safely transmitted and stored.
    /// The length of the token can be adjusted by changing the number of bytes generated, but 64 bytes (resulting in an 88-character Base64 string) is a common choice for sufficient entropy.
    /// </summary>
    /// <returns>The generated refresh token.</returns>
    private string Generate()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);

        return Convert.ToBase64String(randomBytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", "");
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
