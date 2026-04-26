namespace Doyep.Analyzer.Application.Security;

/// <summary>
/// State service interface for managing state tokens used in authentication processes.
/// </summary>
public interface IStateService
{
    /// <summary>
    /// Creates a new state token associated with the specified device ID. This token can be used to maintain state between authentication requests and responses.
    /// </summary>
    /// <param name="deviceId">The unique identifier of the device for which the state token is being created.</param>
    /// <returns>The newly created state token.</returns>
    string Create(Guid deviceId);

    /// <summary>
    /// Consumes the specified state token and returning the associated device ID.
    /// </summary>
    /// <param name="state">The state token to consume.</param>
    /// <returns>The device ID associated with the consumed state token.</returns>
    StatePayload? Consume(string state);
}
