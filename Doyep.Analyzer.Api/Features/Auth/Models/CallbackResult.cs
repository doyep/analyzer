using Doyep.Analyzer.Application.Auth;

namespace Doyep.Analyzer.Api.Features.Auth;

/// <summary>
/// This class represents the result of the authentication callback process, encapsulating the success status, the generated JWT
/// token if authentication was successful, and any error information if authentication failed.
/// </summary>
public class CallbackResult
{
    /// <summary>
    /// Indicates whether the authentication process was successful. If true, the Token property will contain a valid JWT token.
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Contains the generated JWT token if authentication was successful.
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// Contains error information if the authentication process failed.
    /// </summary>
    public AuthError? Error { get; set; }
}
