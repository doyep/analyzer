using System.Text.Json;

using Doyep.Analyzer.Application;
using Doyep.Analyzer.Application.Auth;
using Doyep.Analyzer.Application.Security;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure.Security;

/// <summary>
/// Implements the <see cref="IStateService"/> interface to manage state tokens used in authentication processes.
/// This service creates state tokens that encapsulate the device ID, redirect URI, and expiration time, and it
/// can consume these tokens to retrieve the associated device information.
/// </summary>
public class StateService(
    IDataProtectionProvider provider,
    IOptions<StateOptions> options
) : IStateService
{
    private readonly IDataProtector _protector = provider.CreateProtector("oauth-state");
    private readonly StateOptions _options = options.Value;

    /// <inheritdoc/>
    public string Create(Guid deviceId, Uri redirectUri)
    {
        var payload = new AuthState
        {
            DeviceId = deviceId,
            RedirectUri = redirectUri,
        };

        var json = JsonSerializer.Serialize(payload);

        return _protector.Protect(json);
    }

    /// <inheritdoc/>
    public Result<AuthState, Error> Consume(string state)
    {
        try
        {
            var json = _protector.Unprotect(state);
            var payload = JsonSerializer.Deserialize<AuthState>(json);

            if (payload is null || payload.ExpiresAt < DateTimeOffset.UtcNow)
                return Result<AuthState, Error>.Failure(LoginErrors.InvalidState);

            return Result<AuthState, Error>.Success(payload);
        }
        catch
        {
            return Result<AuthState, Error>.Failure(LoginErrors.InvalidState);
        }
    }
}
