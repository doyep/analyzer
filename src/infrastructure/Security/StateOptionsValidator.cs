using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure.Security;

/// <summary>
/// Validates the StateOptions to ensure all required fields are set and properly formatted.
/// </summary>
public class StateOptionsValidator : IValidateOptions<StateOptions>
{
    /// <summary>
    /// Validates the StateOptions instance to ensure that the ExpirationInMinutes property is set to a positive integer.
    /// This validation ensures that the state token has a valid expiration time configured, which is crucial for security and proper functioning of the authentication flow.
    /// </summary>
    public ValidateOptionsResult Validate(string? name, StateOptions options)
    {
        var errors = new List<string>();

        if (options.ExpirationInMinutes <= 0)
            errors.Add("ExpirationInMinutes must be a positive integer.");

        if (errors.Count > 0)
            return ValidateOptionsResult.Fail(errors);

        return ValidateOptionsResult.Success;
    }
}
