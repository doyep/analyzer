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
        var existingToken = await FindByStravaAthleteIdAsync(stravaToken.StravaAthleteId);

        if (existingToken is null)
        {
            _context.StravaTokens.Add(stravaToken);
        }
        else
        {
            existingToken.AccessToken = stravaToken.AccessToken;
            existingToken.RefreshToken = stravaToken.RefreshToken;
            existingToken.ExpiresAt = stravaToken.ExpiresAt;
            _context.StravaTokens.Update(existingToken);
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
