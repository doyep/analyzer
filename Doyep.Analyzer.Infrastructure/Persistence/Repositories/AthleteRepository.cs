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
    public Task<Athlete?> FindByStravaIdAsync(long stravaId)
    {
        return _context.Athletes.FirstOrDefaultAsync(a => a.StravaId == stravaId);
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
            throw new DuplicateAthleteException(athlete.StravaId);
        }
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Athlete athlete)
    {
        _context.Athletes.Update(athlete);
        await _context.SaveChangesAsync();
    }
}
