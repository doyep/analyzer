using Doyep.Analyzer.Domain;

/// <summary>
/// Represents the result of a refresh token operation, containing the authenticated athlete and the new refresh token.
/// </summary>
/// <param name="Athlete">The authenticated athlete associated with the refresh token.</param>
/// <param name="RefreshToken">The newly issued refresh token.</param>
public sealed record RefreshResult(
    Athlete Athlete,
    string RefreshToken
);
