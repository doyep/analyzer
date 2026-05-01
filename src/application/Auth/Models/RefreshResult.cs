using Doyep.Analyzer.Domain;

/// <summary>
/// Represents the result of a refresh token operation, containing the new refresh token and the associated Strava athlete ID. This record is used to encapsulate the outcome of issuing or refreshing a token, providing both the token value and the athlete information in a single object.
/// </summary>
/// <param name="RefreshToken">The newly issued refresh token.</param>
/// <param name="Athlete">The Strava athlete associated with the refresh token.</param>
public sealed record RefreshResult(
    string RawRefreshToken,
    Athlete Athlete
);
