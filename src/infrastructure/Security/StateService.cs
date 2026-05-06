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
        var payload = new StatePayload
        {
            DeviceId = deviceId,
            RedirectUri = redirectUri,
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(_options.ExpirationInMinutes)
        };

        var json = JsonSerializer.Serialize(payload);

        return _protector.Protect(json);
    }

    /// <inheritdoc/>
    public Result<StatePayload, Error> Consume(string state)
    {
        try
        {
            var json = _protector.Unprotect(state);
            var payload = JsonSerializer.Deserialize<StatePayload>(json);

            if (payload is null || payload.ExpiresAt < DateTimeOffset.UtcNow)
                return Result<StatePayload, Error>.Failure(LoginErrors.InvalidState);

            return Result<StatePayload, Error>.Success(payload);
        }
        catch
        {
            return Result<StatePayload, Error>.Failure(LoginErrors.InvalidState);
        }
    }
}
