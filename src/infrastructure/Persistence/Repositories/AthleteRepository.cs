using Doyep.Analyzer.Application.Athletes;
using Doyep.Analyzer.Domain;

using Microsoft.EntityFrameworkCore;

namespace Doyep.Analyzer.Infrastructure.Persistence;

/// <summary>
/// Implements the IAthleteRepository interface using Entity Framework Core to manage Athlete entities in the database.
/// </summary>
public class AthleteRepository(AnalyzerDbContext _context) : IAthleteRepository
{

    /// <inheritdoc/>
    public Task<Athlete?> FindByStravaAthleteIdAsync(long stravaAthleteId)
    {
        return _context.Athletes.FirstOrDefaultAsync(a => a.StravaAthleteId == stravaAthleteId);
    }

    /// <inheritdoc/>
    public async Task AddAsync(Athlete athlete)
    {
        try
        {
            _context.Athletes.Add(athlete);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new DuplicateAthleteException(athlete.StravaAthleteId);
        }
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Athlete athlete)
    {
        _context.Athletes.Update(athlete);
        await _context.SaveChangesAsync();
    }
}
