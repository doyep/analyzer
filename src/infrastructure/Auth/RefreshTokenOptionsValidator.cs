using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure.Auth;

/// <summary>
/// Validates the RefreshTokenOptions to ensure all required fields are set and properly formatted.
/// </summary>
public class RefreshTokenOptionsValidator : IValidateOptions<RefreshTokenOptions>
{
    /// <summary>
    /// Validates the RefreshTokenOptions instance. Checks for required fields and proper formatting.
    /// </summary>
    public ValidateOptionsResult Validate(string? name, RefreshTokenOptions options)
    {
        var errors = new List<string>();

        if (options.ExpirationInDays <= 0)
            errors.Add("ExpirationInDays must be greater than 0.");

        return errors.Count == 0
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(errors);
    }
}
