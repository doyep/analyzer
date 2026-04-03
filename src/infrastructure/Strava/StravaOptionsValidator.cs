using Microsoft.Extensions.Options;

namespace Doyep.Analyzer.Infrastructure.Strava;

/// <summary>
/// Validates the StravaOptions to ensure all required fields are set and properly formatted.
/// </summary>
public class StravaOptionsValidator : IValidateOptions<StravaOptions>
{
    /// <summary>
    /// Validates the StravaOptions instance. Checks for required fields, proper formatting, and placeholder values.
    /// </summary>
    public ValidateOptionsResult Validate(string? name, StravaOptions options)
    {
        var errors = new List<string>();

        if (IsPlaceholder(options.ClientId))
            errors.Add("ClientId is not configured properly.");
        if (!long.TryParse(options.ClientId, out _))
            errors.Add("ClientId must be a valid numeric value.");

        if (IsPlaceholder(options.ClientSecret))
            errors.Add("ClientSecret is not configured properly.");

        if (string.IsNullOrWhiteSpace(options.RedirectUri))
            errors.Add("RedirectUri is required.");
        if (!Uri.IsWellFormedUriString(options.RedirectUri, UriKind.Absolute))
            errors.Add("RedirectUri is not configured properly.");

        return errors.Count == 0
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(errors);
    }

    private static bool IsPlaceholder(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? true : value.Contains("<YOUR_");
    }
}
