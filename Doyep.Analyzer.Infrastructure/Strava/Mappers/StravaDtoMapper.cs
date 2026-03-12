using Doyep.Analyzer.Application.Strava;

namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Provides extensions methods to map Strava DTOs to application models
/// </summary>
public static class StravaDtoMapper
{

    public static StravaTokenResponse ToModel(this StravaTokenResponseDto dto) => new(
        dto.TokenType,
        dto.ExpiresIn,
        dto.ExpiresAt,
        dto.RefreshToken,
        dto.AccessToken,
        dto.Athlete?.ToModel()
    );

    public static SummaryAthlete ToModel(this SummaryAthleteDto dto) => new(
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
        dto.CreatedAt,
        dto.UpdatedAt
    );
}
