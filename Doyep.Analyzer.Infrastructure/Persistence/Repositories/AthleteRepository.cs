using Doyep.Analyzer.Application;
using Doyep.Analyzer.Domain;

namespace Doyep.Analyzer.Infrastructure.Persistence;

/// <summary>
/// Implements the IAthleteRepository interface using Entity Framework Core to manage Athlete entities in the database.
/// </summary>
public class AthleteRepository(AnalyzerDbContext _context) : IAthleteRepository
{
    /// <inheritdoc/>
    public async Task AddAthleteAsync(Athlete athlete)
    {
        await _context.Athletes.AddAsync(athlete);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<Athlete?> GetAthleteByStravaIdAsync(long stravaId)
    {
        return await _context.Athletes.FindAsync(stravaId);
    }
}