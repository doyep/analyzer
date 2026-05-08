using Doyep.Analyzer.Application.Security;

namespace Doyep.Analyzer.Application.Auth;

/// <summary>
/// Defines the contract for a store that manages authentication state values during the authentication process.
/// This interface provides methods to set, retrieve, and remove authentication state values, which are used to
/// maintain state between the authentication request and callback phases in the OAuth flow.
/// </summary>
public interface IAuthStateStore
{
    /// <summary>
    /// Sets the authentication state value associated with a specific state token.
    /// This method is typically called during the authentication request phase to store the necessary
    /// state information that will be retrieved later during the callback phase.
    /// </summary>
    /// <param name="state">The state token associated with the authentication request.</param>
    /// <param name="value">The authentication state value to store.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SetAsync(string state, AuthState value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the authentication state value associated with a specific state token.
    /// This method is typically called during the callback phase to retrieve the state information
    /// stored during the authentication request phase.
    /// </summary>
    /// <param name="state">The state token associated with the authentication request.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>The authentication state value, or an error if not found.</returns>
    Task<Result<AuthState, Error>> GetAsync(string state, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the authentication state value associated with a specific state token.
    /// This method is typically called after the state information has been retrieved to clean up.
    /// </summary>
    /// <param name="state">The state token associated with the authentication request.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RemoveAsync(string state, CancellationToken cancellationToken = default);
}
