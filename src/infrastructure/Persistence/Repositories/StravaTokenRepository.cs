using Doyep.Analyzer.Application.Strava;

using Microsoft.EntityFrameworkCore;

namespace Doyep.Analyzer.Infrastructure.Persistence;

/// <summary>
/// Implements the IStravaTokenRepository interface using Entity Framework Core to manage StravaToken entities in the database.
/// </summary>
public class StravaTokenRepository(AnalyzerDbContext _context) : IStravaTokenRepository
{
    /// <inheritdoc/>
    public async Task<StravaToken?> FindByStravaAthleteIdAsync(long stravaAthleteId)
    {
        return await _context.StravaTokens.FirstOrDefaultAsync(t => t.StravaAthleteId == stravaAthleteId);
    }

    /// <inheritdoc/>
    public async Task SaveAsync(StravaToken stravaToken)
    {
        var token = await FindByStravaAthleteIdAsync(stravaToken.StravaAthleteId);

        if (token is null)
        {
            _context.StravaTokens.Add(stravaToken);
        }
        else
        {
            token.AccessToken = stravaToken.AccessToken;
            token.RefreshToken = stravaToken.RefreshToken;
            token.ExpiresAt = stravaToken.ExpiresAt;
            _context.StravaTokens.Update(token);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new StravaTokenPersistenceException(stravaToken.StravaAthleteId);
        }
    }
}
