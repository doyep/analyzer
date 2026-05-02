namespace Doyep.Analyzer.Application.Security;

/// <summary>
/// Represents the payload contained within a state token used for authentication processes.
/// This record includes the device ID associated with the state token and the expiration time of the token.
/// The state token is typically used to maintain state between authentication requests and responses, ensuring that the authentication flow is secure and that tokens are valid only for a limited time.
/// </summary>
public record StatePayload
{
    /// <summary>
    /// The unique identifier of the device associated with the state token. This is used to link the state token to a specific device during the authentication process.
    /// </summary>
    public required Guid DeviceId { get; init; }

    /// <summary>
    /// The expiration time of the state token. This is used to determine the validity of the token and ensure that it is only used within a limited time frame.
    /// </summary>
    public required DateTimeOffset ExpiresAt { get; init; }
}
