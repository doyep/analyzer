namespace Doyep.Analyzer.Infrastructure.Auth;

/// <summary>
/// Configuration options for Refresh token management, such as token expiration time.
/// These values are loaded from the "RefreshToken" section of appsettings.json, from
/// environment variables or user secrets.
/// </summary>
public class RefreshTokenOptions
{
    /// <summary>
    /// Section name of appsettings.json
    /// </summary>
    public const string SectionName = "RefreshToken";

    /// <summary>
    /// The expiration time of the refresh token in days.
    /// This value determines how long the refresh token is valid before it expires and can no longer be used to obtain new JWT tokens.
    /// </summary>
    public required int ExpirationInDays { get; set; }
}
