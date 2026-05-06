namespace Doyep.Analyzer.Application.Security;

/// <summary>
/// Defines the contract for a service responsible for managing state tokens used in authentication workflows,
/// particularly for maintaining state between authentication requests and responses in OAuth 2.0 flows.
/// This service allows for the creation of state tokens associated with specific device IDs and the consumption
/// of these tokens to retrieve the associated device information when needed.
/// </summary>
public interface IStateService
{
    /// <summary>
    /// Creates a new state token associated with the specified device ID and redirect URI.
    /// This token can be used to maintain state between authentication requests and responses.
    /// </summary>
    /// <param name="deviceId">The unique identifier of the device for which the state token is being created.</param>
    /// <param name="redirectUri">The URI to which the user will be redirected after authentication.</param>
    /// <returns>The newly created state token.</returns>
    string Create(Guid deviceId, Uri redirectUri);

    /// <summary>
    /// Consumes a state token and retrieves the associated device information. This method is typically called after
    /// the authentication response is received to validate the state and extract the relevant device context.
    /// </summary>
    /// <param name="state">The state token to consume.</param>
    /// <returns>The result of the state consumption, containing either the associated device information or an error.</returns>
    Result<StatePayload, Error> Consume(string state);
}
