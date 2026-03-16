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
    public static StravaTokenResponse ToModel(this StravaTokenResponseDto dto) => new(
        dto.TokenType,
        TimeSpan.FromSeconds(dto.ExpiresIn),
        DateTimeOffset.FromUnixTimeSeconds(dto.ExpiresAt).UtcDateTime,
        dto.RefreshToken,
        dto.AccessToken,
        dto.Athlete?.ToModel()
    );

    /// <summary>
    /// Maps a <see cref="SummaryAthleteDto"/> to a <see cref="Athlete"/>
    /// </summary>
    public static Athlete ToModel(this SummaryAthleteDto dto) => new(
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
