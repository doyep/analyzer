using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure.Auth;

/// <summary>
/// Validates the JwtOptions to ensure all required fields are set and properly formatted.
/// </summary>
public class JwtOptionsValidator : IValidateOptions<JwtOptions>
{
    /// <summary>
    /// Validates the JwtOptions instance. Checks for required fields, proper formatting, and placeholder values.
    /// </summary>
    public ValidateOptionsResult Validate(string? name, JwtOptions options)
    {
        var errors = new List<string>();

        if (IsPlaceholder(options.Secret))
            errors.Add("Secret is not configured properly.");
        if (options.Secret.Length < 16)
            errors.Add("Secret should be at least 16 characters long for security reasons.");

        if (IsPlaceholder(options.Issuer))
            errors.Add("Issuer is not configured properly.");

        if (IsPlaceholder(options.Audience))
            errors.Add("Audience is not configured properly.");

        if (options.ExpirationInMinutes <= 0)
            errors.Add("ExpirationInMinutes must be a positive integer.");

        return errors.Count == 0
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(errors);
    }

    private static bool IsPlaceholder(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? true : value.Contains("<YOUR_");
    }
}
