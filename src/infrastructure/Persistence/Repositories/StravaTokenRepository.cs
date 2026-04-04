using Doyep.Analyzer.Application.Security;
using Doyep.Analyzer.Application.Strava;

using Microsoft.EntityFrameworkCore;

namespace Doyep.Analyzer.Infrastructure.Persistence;

/// <summary>
/// Implements the IStravaTokenRepository interface using Entity Framework Core to manage StravaToken entities in the database.
/// </summary>
public class StravaTokenRepository(
    AnalyzerDbContext _context,
    IEncryptionService _encryption)
    : IStravaTokenRepository
{
    /// <inheritdoc/>
    public async Task<StravaToken?> FindByStravaAthleteId(long stravaAthleteId)
    {
        var encryptedStravaToken = await _context.StravaTokens.FirstOrDefaultAsync(t => t.StravaAthleteId == stravaAthleteId);

        return encryptedStravaToken == null
            ? null
            : Decrypt(encryptedStravaToken);
    }

    /// <inheritdoc/>
    public async Task SaveAsync(StravaToken stravaToken)
    {
        var existingToken = await FindByStravaAthleteId(stravaToken.StravaAthleteId);

        if (existingToken == null)
        {
            _context.StravaTokens.Add(Encrypt(stravaToken));
        }
        else
        {
            existingToken.UpdateFrom(stravaToken);
            _context.StravaTokens.Update(Encrypt(stravaToken));
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

    /// <summary>
    /// Converts an EncryptedStravaToken entity from the database into a StravaToken domain model, decrypting the access and refresh tokens in the process.
    /// </summary>
    private StravaToken Decrypt(EncryptedStravaToken encryptedToken)
    {
        return new StravaToken
        {
            StravaAthleteId = encryptedToken.StravaAthleteId,
            AccessToken = _encryption.Decrypt(encryptedToken.EncryptedAccessToken),
            RefreshToken = _encryption.Decrypt(encryptedToken.EncryptedRefreshToken),
            ExpiresAt = encryptedToken.ExpiresAt
        };
    }

    /// <summary>
    /// Converts a StravaToken domain model into an EncryptedStravaToken entity for storage in the database, encrypting the access and refresh tokens in the process.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private EncryptedStravaToken Encrypt(StravaToken token)
    {
        return new EncryptedStravaToken
        {
            StravaAthleteId = token.StravaAthleteId,
            EncryptedAccessToken = _encryption.Encrypt(token.AccessToken),
            EncryptedRefreshToken = _encryption.Encrypt(token.RefreshToken),
            ExpiresAt = token.ExpiresAt
        };
    }
}
