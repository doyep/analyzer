namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Represents the authentication tokens issued to a user upon successful login.
/// This record contains the JWT token used for accessing protected resources and the refresh token used for obtaining new JWT tokens when the current one expires.
/// The JWT token is typically short-lived, while the refresh token has a longer lifespan and can be securely stored to maintain user sessions without requiring frequent re-authentication.
/// </summary>
public record AuthTokens
{
    /// <summary>
    /// The JWT token issued to the user, which is used for authenticating requests to protected resources within the application. This token contains claims about the user's identity and permissions.
    /// </summary>
    public required string JwtToken { get; init; }

    /// <summary>
    /// The refresh token issued to the user, which is used to obtain new JWT tokens when the current one expires.
    /// </summary>
    public required string RefreshToken { get; init; }
}
