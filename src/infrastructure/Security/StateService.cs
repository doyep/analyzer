using System.Text.Json;

using Doyep.Analyzer.Application.Security;
using Doyep.Analyzer.Infrastructure.Auth;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure.Security;

/// <summary>
/// Implements the IStateService interface to create and consume state tokens used in the OAuth authentication flow. The state token contains a device ID and an expiration time, and is protected using ASP.NET Core's data protection API to ensure its integrity and confidentiality.
/// </summary>
/// <param name="provider">The data protection provider used to create a data protector for securing the state tokens.</param>
/// <param name="options">The options containing configuration settings for the state service, such as token expiration time.</param>
public class StateService(
    IDataProtectionProvider provider,
    IOptions<StateOptions> options
) : IStateService
{
    private readonly IDataProtector _protector = provider.CreateProtector("oauth-state");
    private readonly StateOptions _options = options.Value;

    /// <inheritdoc/>
    public string Create(Guid deviceId)
    {
        var payload = new StatePayload
        {
            DeviceId = deviceId,
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(_options.ExpirationInMinutes)
        };

        var json = JsonSerializer.Serialize(payload);

        return _protector.Protect(json);
    }

    /// <inheritdoc/>
    public StatePayload? Consume(string state)
    {
        try
        {
            var json = _protector.Unprotect(state);
            var payload = JsonSerializer.Deserialize<StatePayload>(json);


            if (payload is null || payload.ExpiresAt < DateTimeOffset.UtcNow)
                return null;

            return payload;
        }
        catch
        {
            return null;
        }
    }
}
