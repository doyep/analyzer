namespace Doyep.Analyzer.Infrastructure.Auth;

/// <summary>
/// Configuration options for JWT token generation and validation.
/// These values are loaded from the "Jwt" section of appsettings.json, from
/// environment variables or user secrets.
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Section name of appsettings.json
    /// </summary>
    public const string SectionName = "Auth:Jwt";

    /// <summary>
    /// The secret used for signing JWT tokens. This value should be stored securely and should be of sufficient length and complexity to prevent brute-force attacks.
    /// </summary>
    public required string Secret { get; set; }

    /// <summary>
    /// The issuer of the JWT token, typically the name of the application or service that generates the token. This value is used in token validation to ensure that the token was issued by a trusted source.
    /// </summary>
    public required string Issuer { get; set; }

    /// <summary>
    /// The intended audience of the JWT token, typically a string that identifies the recipients that the token is intended for. This value is used in token validation to ensure that the token is being used by an authorized recipient.
    /// </summary>
    public required string Audience { get; set; }

    /// <summary>
    /// The expiration time of the JWT token in minutes. This value determines how long the token is valid before it expires and can no longer be used for authentication.
    /// </summary>
    public required int ExpirationInMinutes { get; set; }
}
