using Doyep.Analyzer.Application.Strava;

namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Provides extensions methods to map Strava DTOs to application models
/// </summary>
public static class StravaDtoMapper
{

    /// <summary>
    /// Maps a <see cref="StravaTokenResponseDto"/> to a <see cref="StravaTokenResponse"/>
    /// </summary>
    public static StravaAuthTokenResponse ToModel(this StravaAuthTokenResponseDto dto) => new StravaAuthTokenResponse
    {
        TokenType = dto.TokenType,
        ExpiresIn = TimeSpan.FromSeconds(dto.ExpiresIn),
        ExpiresAt = DateTimeOffset.FromUnixTimeSeconds(dto.ExpiresAt).UtcDateTime,
        RefreshToken = dto.RefreshToken,
        AccessToken = dto.AccessToken,
        Athlete = dto.Athlete.ToModel()
    };

    public static StravaRefreshTokenResponse ToModel(this StravaRefreshTokenResponseDto dto) => new StravaRefreshTokenResponse
    {
        TokenType = dto.TokenType,
        ExpiresIn = TimeSpan.FromSeconds(dto.ExpiresIn),
        ExpiresAt = DateTimeOffset.FromUnixTimeSeconds(dto.ExpiresAt).UtcDateTime,
        RefreshToken = dto.RefreshToken,
        AccessToken = dto.AccessToken
    };

    /// <summary>
    /// Maps a <see cref="SummaryAthleteDto"/> to a <see cref="StravaSummaryAthlete"/>
    /// </summary>
    public static StravaSummaryAthlete ToModel(this SummaryAthleteDto dto) => new(
        dto.Id,
        dto.ResourceState,
        dto.Firstname,
        dto.Lastname,
        dto.ProfileMedium,
        dto.Profile,
        dto.City,
        dto.State,
        dto.Country,
        dto.Sex,
        dto.Premium,
        dto.Summit,
        new DateTimeOffset(dto.CreatedAt),
        new DateTimeOffset(dto.UpdatedAt)
    );
}
