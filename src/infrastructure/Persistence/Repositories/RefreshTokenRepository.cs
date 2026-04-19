using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Domain;

using Microsoft.EntityFrameworkCore;

namespace Doyep.Analyzer.Infrastructure.Persistence;

/// <summary>
/// Implements the IRefreshTokenRepository interface using Entity Framework Core to manage RefreshToken entities in the database.
/// </summary>
public class RefreshTokenRepository(AnalyzerDbContext _context) : IRefreshTokenRepository
{
    /// <inheritdoc/>
    public async Task AddAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task TryRevokeAsync(string hashedRefreshToken)
    {
        var token = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.HashedToken == hashedRefreshToken && rt.RevokedAt == null && DateTimeOffset.UtcNow < rt.ExpiresAt);

        if (token is not null)
        {
            token.Revoke();
            await _context.SaveChangesAsync();
        }
    }

    /// <inheritdoc/>
    public async Task RevokeAllActiveByStravaAthleteIdAsync(long stravaAthleteId)
    {
        var tokens = await _context.RefreshTokens
            .Where(rt => rt.StravaAthleteId == stravaAthleteId && rt.RevokedAt == null && DateTimeOffset.UtcNow < rt.ExpiresAt)
            .ToListAsync();

        foreach (var token in tokens)
        {
            token.Revoke();
        }

        await _context.SaveChangesAsync();
    }
}
